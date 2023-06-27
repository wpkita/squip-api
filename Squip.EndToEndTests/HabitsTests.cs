using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using Squip.Rest.Habits.Dtos;
using Xunit;

namespace Squip.EndToEndTests;

[Trait("Category", "End-to-end")]
public class HabitsTests
{
    [Fact]
    public async void CreateReadUpdateDelete()
    {
        var restClient = new RestClient("https://localhost:8080/api");
        var postRequest = new RestRequest("habits");
        postRequest.AddJsonBody(new
            { name = "Name goes here" });
        var postResponse = await restClient.PostAsync(postRequest);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = JsonConvert.DeserializeObject<HabitDto>(postResponse.Content);
        responseContent.Should().NotBe(null);
        var id = responseContent.Id;
        responseContent.Name.Should().Be("Name goes here");

        var getAllRequest = new RestRequest("habits");
        var getAllResponse = await restClient.GetAsync(getAllRequest);
        getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getAllResponseContent = JsonConvert.DeserializeObject<HabitDto[]>(getAllResponse.Content);
        var habit = getAllResponseContent.FirstOrDefault(i => i.Id == id);
        habit.Should().NotBeNull();

        var getRequest = new RestRequest($"habits/{id}");
        var getResponse = await restClient.GetAsync(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getResponseContent = JsonConvert.DeserializeObject<HabitDto>(getResponse.Content);
        getResponseContent.Id.Should().Be(id);
        getResponseContent.Name.Should().Be("Name goes here");

        var putRequest = new RestRequest($"habits/{id}");
        putRequest.AddJsonBody(new
            { id, name = "Updated name" });
        var putResponse = await restClient.PutAsync(putRequest);
        putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        getResponse = await restClient.GetAsync(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponseContent = JsonConvert.DeserializeObject<HabitDto>(getResponse.Content);
        getResponseContent.Id.Should().Be(id);
        getResponseContent.Name.Should().Be("Updated name");

        var deleteRequest = new RestRequest($"habits/{id}");
        var deleteResponse = await restClient.DeleteAsync(deleteRequest);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        getResponse = await restClient.GetAsync(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
