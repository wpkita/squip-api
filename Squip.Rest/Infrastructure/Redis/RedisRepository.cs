using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Squip.Rest.Common.Domain;
using Squip.Rest.Infrastructure.Common;
using StackExchange.Redis;

namespace Squip.Rest.Infrastructure.Redis;

public abstract class RedisRepository<T> : IRepository<T> where T : IChangeable
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;
    protected readonly IDatabase RedisDb;

    protected RedisRepository(IConfiguration config)
    {
        var redis = ConnectionMultiplexer.Connect(config["RedisConnectionString"]);
        RedisDb = redis.GetDatabase();

        _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    }

    protected abstract string EntityName { get; }

    private string ArchivedEntityIdsSetName => $"{EntityName}IdsArchived";

    protected string ActiveEntityIdsSetName => $"{EntityName}Ids";

    public async Task<bool> DoesExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doesExist = await RedisDb.KeyExistsAsync(EntityRedisKey(id));

        return doesExist;
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = default(T);

        try
        {
            var entityJson = await RedisDb.StringGetAsync(EntityRedisKey(id));
            entity = JsonConvert.DeserializeObject<T>(entityJson, _jsonSerializerSettings);
        }
        catch
        {
            // This is technically business logic? Hmm
            await ArchiveAsync(id, cancellationToken);
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        ICollection<T> entities = new Collection<T>();

        var entityIds = await RedisDb.SetMembersAsync(ActiveEntityIdsSetName);
        foreach (var entityId in entityIds)
        {
            var entity = await GetByIdAsync(Guid.Parse((string)entityId!), cancellationToken);
            entities.Add(entity);
        }

        return entities;
    }

    public Task<IEnumerable<T>> GetByTagAsync(string tagName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        entity.PreCreate();
        var entityJson = JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

        await RedisDb.StringSetAsync(EntityRedisKey(entity.Id), entityJson);

        // Cache Id for random selection later
        await RedisDb.SetAddAsync(ActiveEntityIdsSetName, entity.Id.ToString());

        return true;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        entity.PreUpdate();
        var entityJson = JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

        await RedisDb.StringSetAsync(EntityRedisKey(entity.Id), entityJson);

        return true;
    }

    public async Task<bool> ArchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        var didSucceed = await RedisDb.SetMoveAsync(
            ActiveEntityIdsSetName,
            ArchivedEntityIdsSetName,
            id.ToString()
        );

        return didSucceed;
    }

    public Task<IEnumerable<string>> GetAllTagsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private string EntityRedisKey(Guid id)
    {
        return $"{EntityName}:{id}";
    }
}
