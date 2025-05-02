using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

public class SmokeTests
{
    private readonly RestClient _client = new RestClient("https://restful-booker.herokuapp.com");

    [Fact]
    public async Task Auth_Should_Return_Token()
    {
        var request = new RestRequest("/auth", Method.Post);
        request.AddJsonBody(new
        {
            username = "admin",
            password = "password123"
        });

        var response = await _client.ExecuteAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = JObject.Parse(response.Content);
        Assert.NotNull(content["token"]);
    }

    [Fact]
    public async Task CreateBooking_ShouldReturnSuccessAndBookingId()
    {
        var client = new RestClient("https://restful-booker.herokuapp.com");
        var request = new RestRequest("/booking", Method.Post);

        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");

        var rawJson = JsonConvert.SerializeObject(new
        {
            firstname = "John",
            lastname = "Doe",
            totalprice = 100,
            depositpaid = true,
            bookingdates = new
            {
                checkin = "2024-06-01",
                checkout = "2024-06-10"
            },
            additionalneeds = "Breakfast"
        });

        request.AddStringBody(rawJson, DataFormat.Json);

        var response = await client.ExecuteAsync(request);

        Console.WriteLine("Status Code: " + response.StatusCode);
        Console.WriteLine("Content: " + response.Content);

        Assert.Contains(response.StatusCode, new[] { HttpStatusCode.OK, HttpStatusCode.Created });
        Assert.False(string.IsNullOrEmpty(response.Content), "Response content is empty.");

        try
        {
            var json = JObject.Parse(response.Content);
            Assert.NotNull(json["bookingid"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("JSON Parse Error: " + ex.Message);
            Assert.True(false, "Invalid JSON response.");
        }
    }

    [Fact]
    public async Task UpdateBooking_ShouldReturnSuccess_WhenAuthorized()
    {
        var client = new RestClient("https://restful-booker.herokuapp.com");

        var authRequest = new RestRequest("/auth", Method.Post);
        authRequest.AddHeader("Content-Type", "application/json");
        authRequest.AddStringBody(JsonConvert.SerializeObject(new
        {
            username = "admin",
            password = "password123"
        }), DataFormat.Json);

        var authResponse = await client.ExecuteAsync(authRequest);
        var token = JObject.Parse(authResponse.Content)["token"]?.ToString();
        Assert.False(string.IsNullOrEmpty(token), "Token was not returned.");

        var createRequest = new RestRequest("/booking", Method.Post);
        createRequest.AddHeader("Content-Type", "application/json");
        createRequest.AddHeader("Accept", "application/json");
        var bookingBody = new
        {
            firstname = "Jane",
            lastname = "Doe",
            totalprice = 150,
            depositpaid = true,
            bookingdates = new { checkin = "2024-07-01", checkout = "2024-07-05" },
            additionalneeds = "Lunch"
        };
        createRequest.AddStringBody(JsonConvert.SerializeObject(bookingBody), DataFormat.Json);
        var createResponse = await client.ExecuteAsync(createRequest);
        var bookingId = JObject.Parse(createResponse.Content)["bookingid"]?.ToString();
        Assert.False(string.IsNullOrEmpty(bookingId), "Booking ID was not returned.");

        var updateRequest = new RestRequest($"/booking/{bookingId}", Method.Put);
        updateRequest.AddHeader("Content-Type", "application/json");
        updateRequest.AddHeader("Accept", "application/json");
        updateRequest.AddHeader("Cookie", $"token={token}");
        var updatedBooking = new
        {
            firstname = "Updated",
            lastname = "User",
            totalprice = 200,
            depositpaid = false,
            bookingdates = new { checkin = "2024-08-01", checkout = "2024-08-10" },
            additionalneeds = "Dinner"
        };
        updateRequest.AddStringBody(JsonConvert.SerializeObject(updatedBooking), DataFormat.Json);

        var updateResponse = await client.ExecuteAsync(updateRequest);
        Console.WriteLine("Update Response: " + updateResponse.Content);

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
    }


    [Fact]
    public async Task DeleteBooking_ShouldReturn201_WhenAuthorized()
    {
        var client = new RestClient("https://restful-booker.herokuapp.com");

        var authRequest = new RestRequest("/auth", Method.Post);
        authRequest.AddHeader("Content-Type", "application/json");
        authRequest.AddStringBody(JsonConvert.SerializeObject(new
        {
            username = "admin",
            password = "password123"
        }), DataFormat.Json);
        var authResponse = await client.ExecuteAsync(authRequest);
        var token = JObject.Parse(authResponse.Content)["token"]?.ToString();
        Assert.False(string.IsNullOrEmpty(token), "Token was not returned.");

        var createRequest = new RestRequest("/booking", Method.Post);
        createRequest.AddHeader("Content-Type", "application/json");
        createRequest.AddHeader("Accept", "application/json");
        var booking = new
        {
            firstname = "ToDelete",
            lastname = "User",
            totalprice = 123,
            depositpaid = true,
            bookingdates = new { checkin = "2024-09-01", checkout = "2024-09-10" },
            additionalneeds = "Snacks"
        };
        createRequest.AddStringBody(JsonConvert.SerializeObject(booking), DataFormat.Json);
        var createResponse = await client.ExecuteAsync(createRequest);
        var bookingId = JObject.Parse(createResponse.Content)["bookingid"]?.ToString();
        Assert.False(string.IsNullOrEmpty(bookingId), "Booking ID was not returned.");

        var deleteRequest = new RestRequest($"/booking/{bookingId}", Method.Delete);
        deleteRequest.AddHeader("Cookie", $"token={token}");
        var deleteResponse = await client.ExecuteAsync(deleteRequest);
        Console.WriteLine("Delete Response: " + deleteResponse.StatusCode);

        Assert.Equal(HttpStatusCode.Created, deleteResponse.StatusCode);
    }



}








