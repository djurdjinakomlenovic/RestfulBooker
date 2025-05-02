using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test_RestfulBooker_Djurdjina_Komlenovic
{
    public class PingTest
    {
        [Fact]
        public async Task Ping_ShouldReturn201Created()
        {
            // Arrange
            var client = new RestClient("https://restful-booker.herokuapp.com");
            var request = new RestRequest("/ping", Method.Get);

            // Act
            var response = await client.ExecuteAsync(request);

            // Assert
            Console.WriteLine("Ping Response Status Code: " + response.StatusCode);
            Console.WriteLine("Ping Response Content: " + response.Content);

            // Check if the status code is 200 OK
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Optionally, you can check if the content matches expected output
            Assert.Equal("Created", response.Content);  // Based on the current behavior of the API
        }
    }
}
