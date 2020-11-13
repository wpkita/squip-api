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
            var client = new RestClient("https://localhost:44312/");

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
            var responseContent = client.Post(request).Content;
            var deserializedJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
            var id = deserializedJson.id.Value as string;

            (deserializedJson.name.Value as string).Should().Be(name);
            (deserializedJson.type.Value as string).Should().Be(type);
            id.Should().NotBeNullOrWhiteSpace();

            // Read
            request = new RestRequest("tiles");
            request.AddQueryParameter("id", id);
            responseContent = client.Get(request).Content;
            deserializedJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
            id = deserializedJson.id.Value as string;

            (deserializedJson.name.Value as string).Should().Be(name);
            (deserializedJson.type.Value as string).Should().Be(type);
            id.Should().Be(id);

            // Update
            request = new RestRequest("tiles");
            request.AddJsonBody(new
            {
                id,
                name = updatedName,
                type = updatedType
            });
            responseContent = client.Put(request).Content;
            deserializedJson = JsonConvert.DeserializeObject<dynamic>(responseContent);

            (deserializedJson.name.Value as string).Should().Be(updatedName);
            (deserializedJson.type.Value as string).Should().Be(updatedType);
            (deserializedJson.id.Value as string).Should().Be(id);

            // Delete
            request = new RestRequest("tiles");
            request.AddQueryParameter("id", id);
            var response = client.Delete(request);
            response.StatusCode.Should().Be(204);

            request = new RestRequest("tiles");
            response = client.Get(request);
            response.StatusCode.Should().Be(404);
        }
    }
}
