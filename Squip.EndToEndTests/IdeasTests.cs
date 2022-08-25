using System;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using Squip.Rest.Dtos;
using Xunit;

namespace Squip.EndToEndTests;

[Trait("Category", "End-to-end")]
public class IdeasTests
{
    [Fact]
    public async void CreateReadUpdateDelete()
    {
        var restClient = new RestClient("https://localhost:8080/api");
        var postRequest = new RestRequest("ideas");
        postRequest.AddJsonBody(new
            { title = "Title goes here", content = "Content goes here", tags = new[] { "Tag1" } });
        var postResponse = await restClient.PostAsync(postRequest);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = JsonConvert.DeserializeObject<IdeaDto>(postResponse.Content);
        responseContent.Should().NotBe(null);
        var id = responseContent.Id;
        responseContent.Title.Should().Be("Title goes here");
        responseContent.Content.Should().Be("Content goes here");

        var getRequest = new RestRequest($"ideas/{id}");
        var getResponse = await restClient.GetAsync(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getResponseContent = JsonConvert.DeserializeObject<IdeaDto>(getResponse.Content);
        getResponseContent.Id.Should().Be(id);
        getResponseContent.Title.Should().Be("Title goes here");
        getResponseContent.Content.Should().Be("Content goes here");
        getResponseContent.Tags.Length.Should().Be(1);
        getResponseContent.Tags[0].Should().Be("Tag1");

        var putRequest = new RestRequest($"ideas/{id}");
        putRequest.AddJsonBody(new
            { id, title = "Updated title", content = "Updated content", tags = Array.Empty<object>() });
        var putResponse = await restClient.PutAsync(putRequest);
        putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        getResponse = await restClient.GetAsync(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponseContent = JsonConvert.DeserializeObject<IdeaDto>(getResponse.Content);
        getResponseContent.Id.Should().Be(id);
        getResponseContent.Title.Should().Be("Updated title");
        getResponseContent.Content.Should().Be("Updated content");

        var deleteRequest = new RestRequest($"ideas/{id}");
        var deleteResponse = await restClient.DeleteAsync(deleteRequest);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        getResponse = await restClient.GetAsync(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
