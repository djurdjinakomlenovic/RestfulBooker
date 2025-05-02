using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test_RestfulBooker_Djurdjina_Komlenovic
{
    public class NegativeTests
    {
        private readonly RestClient _client = new RestClient("https://restful-booker.herokuapp.com");

        [Fact]
        public async Task CreateBooking_MissingRequiredFields_ShouldReturn400()
        {
            var request = new RestRequest("/booking", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            var invalidBooking = new { totalprice = 50 };

            request.AddJsonBody(invalidBooking);
            var response = await _client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBooking_WithInvalidToken_ShouldReturn403()
        {
            var bookingId = "1";

            var updateRequest = new RestRequest($"/booking/{bookingId}", Method.Put);
            updateRequest.AddHeader("Content-Type", "application/json");
            updateRequest.AddHeader("Cookie", "token=invalid_token");

            var updateData = new
            {
                firstname = "Hacker",
                lastname = "Test",
                totalprice = 999,
                depositpaid = false,
                bookingdates = new { checkin = "2024-09-01", checkout = "2024-09-10" },
                additionalneeds = "None"
            };

            updateRequest.AddJsonBody(updateData);
            var response = await _client.ExecuteAsync(updateRequest);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBooking_WithNoToken_ShouldReturn403()
        {
            var bookingId = "1";
            var deleteRequest = new RestRequest($"/booking/{bookingId}", Method.Delete);

            var response = await _client.ExecuteAsync(deleteRequest);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetNonExistingBooking_ShouldReturn404()
        {
            var request = new RestRequest("/booking/999999", Method.Get);
            var response = await _client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
