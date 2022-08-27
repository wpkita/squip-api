using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using RestSharp;
using Squip.Rest.Dtos;
using Xunit;
using System.Text.Json;
using NodaTime.Serialization.SystemTextJson;
using RestSharp.Serializers.Json;

namespace Squip.EndToEndTests;

[Trait("Category", "End-to-end")]
public class HibitsTests
{
    private JsonSerializerOptions serializerSettings = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

    [Fact]
    public async void CreateReadUpdateDelete()
    {
        var restClient = new RestClient("https://localhost:8080/api");
        var postHabitRequest = new RestRequest("habits");
        postHabitRequest.AddJsonBody(new
            { name = "Name goes here" });
        var postHabitResponse = await restClient.PostAsync(postHabitRequest);
        postHabitResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var postHabitResponseContent = JsonSerializer.Deserialize<HabitDto>(postHabitResponse.Content,
            serializerSettings);
        postHabitResponseContent.Should().NotBe(null);
        var habitId = postHabitResponseContent.Id;
        postHabitResponseContent.Name.Should().Be("Name goes here");

        var postHibitRequest = new RestRequest($"habits/{habitId}/hibits");
        postHibitRequest.AddJsonBody(new { habitId = habitId });
        var postHibitResponse = await restClient.PostAsync(postHibitRequest);
        postHibitResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var postHibitResponseContent = JsonSerializer.Deserialize<HibitDto>(postHibitResponse.Content,
            serializerSettings);
        postHibitResponseContent.Should().NotBeNull();
        var id = postHibitResponseContent.Id;
        postHibitResponseContent.HabitId.Should().Be(habitId);
        postHibitResponseContent.InstantOccurredAt.Should().NotBeNull();

        var getHibitRequest = new RestRequest($"habits/{habitId}/hibits/{id}");
        var getHibitResponse = await restClient.GetAsync(getHibitRequest);
        getHibitResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getHibitResponseContent = JsonSerializer.Deserialize<HibitDto>(getHibitResponse.Content, serializerSettings);
        getHibitResponseContent.Id.Should().Be(id);

        var getAllHibitsRequest = new RestRequest($"habits/{habitId}/hibits");
        var getAllHibitsResponse = await restClient.GetAsync(getAllHibitsRequest);
        getHibitResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getAllHibitsResponseContent =
            JsonSerializer.Deserialize<HibitDto[]>(getAllHibitsResponse.Content, serializerSettings);
        var hibit = getAllHibitsResponseContent.FirstOrDefault(h => h.Id == id);
        hibit.Should().NotBeNull();

        var deleteHibitRequest = new RestRequest($"habits/{habitId}/hibits/{id}");
        var deleteHibitResponse = await restClient.DeleteAsync(deleteHibitRequest);
        deleteHibitResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var deleteHabitRequest = new RestRequest($"habits/{habitId}");
        var deleteHabitResponse = await restClient.DeleteAsync(deleteHabitRequest);
        deleteHabitResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
