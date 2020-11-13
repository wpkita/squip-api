using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace Squip.EndToEndTests
{
    public class TileRestApiTests
    {
        [Fact]
        public void CreateReadUpdateDelete()
        {
            var client = new RestClient("https://localhost:32770/");

            const string name = "Tile NAME goes here.";
            const string type = "Tile TYPE goes here.";
            const string updatedName = "Updated Tile NAME goes here.";
            const string updatedType = "Updated Tile TYPE goes here.";

            // Create
            var request = new RestRequest("tiles");
            request.AddJsonBody(new
            {
                name, type
            });
            var response = client.Post(request);
            var deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);
            var id = (string)deserializedJson.id.Value;

            ((string)deserializedJson.name.Value).Should().Be(name);
            ((string)deserializedJson.type.Value).Should().Be(type);
            id.Should().NotBeNullOrWhiteSpace();

            // Read
            request = new RestRequest($"tiles/{id}");
            response = client.Get(request);
            deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);
            id = deserializedJson.id.Value;

            ((string)deserializedJson.name.Value).Should().Be(name);
            ((string)deserializedJson.type.Value).Should().Be(type);
            id.Should().Be(id);

            // Update
            request = new RestRequest("tiles");
            request.AddJsonBody(new
            {
                id,
                name = updatedName,
                type = updatedType
            });
            response = client.Put(request);
            deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);

            ((string)deserializedJson.name.Value).Should().Be(updatedName);
            ((string)deserializedJson.type.Value).Should().Be(updatedType);
            ((string)deserializedJson.id.Value).Should().Be(id);

            // Delete
            request = new RestRequest($"tiles/{id}");
            response = client.Delete(request);
            response.StatusCode.Should().Be(204);

            request = new RestRequest($"tiles/{id}");
            response = client.Get(request);
            response.StatusCode.Should().Be(404);
        }
    }
}
