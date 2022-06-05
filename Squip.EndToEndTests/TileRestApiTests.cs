using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace Squip.EndToEndTests
{
    [Trait("Category", "End-to-end")]
    public class TileRestApiTests
    {
        [Fact]
        public async void CreateReadUpdateDelete()
        {
            var client = new RestClient("https://localhost:32776/");

            const string name = "Tile NAME goes here.";
            const string type = "Tile TYPE goes here.";
            const string updatedName = "Updated Tile NAME goes here.";
            const string updatedType = "Updated Tile TYPE goes here.";

            // Create
            var request = new RestRequest("tiles");
            request.AddJsonBody(new { name, type });
            var response = await client.PostAsync(request);
            var deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);
            var id = (string)deserializedJson.id.Value;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            ((string)deserializedJson.name.Value).Should().Be(name);
            ((string)deserializedJson.type.Value).Should().Be(type);
            id.Should().NotBeNullOrWhiteSpace();

            // Read
            request = new RestRequest($"tiles/{id}");
            response = await client.GetAsync(request);
            deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);
            id = deserializedJson.id.Value;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ((string)deserializedJson.name.Value).Should().Be(name);
            ((string)deserializedJson.type.Value).Should().Be(type);
            ((string)deserializedJson.id.Value).Should().Be(id);

            // Update
            request = new RestRequest("tiles");
            request.AddJsonBody(new { id, name = updatedName, type = updatedType });
            response = await client.PutAsync(request);
            deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ((string)deserializedJson.name.Value).Should().Be(updatedName);
            ((string)deserializedJson.type.Value).Should().Be(updatedType);
            ((string)deserializedJson.id.Value).Should().Be(id);

            // Delete
            request = new RestRequest($"tiles/{id}");
            response = await client.DeleteAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            request = new RestRequest($"tiles/{id}");
            response = await client.GetAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
