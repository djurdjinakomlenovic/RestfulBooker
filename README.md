# RestfulBooker
Repository created with the purpose to push an assignment solution.

This project contains automated smoke, positive, negative, and ping tests for the Restful Booker API using C#, RestSharp, and xUnit.

🔧 Technologies Used
C# (.NET Core)

RestSharp

xUnit (test framework)

Newtonsoft.Json (JSON parsing)

Visual Studio 2022

Git / GitHub

🚀 How to Run the Tests
1. Clone the Repository
git clone https://github.com/djurdjinakomlenovic/RestfulBooker.git
cd RestfulBooker
Or use GitHub Desktop to clone the repository.

2. Open in Visual Studio
Open the .sln file (Test_RestfulBooker_Djurdjina_Komlenovic.sln)

Make sure NuGet packages are restored.

3. Install Required Packages
If not already installed, run:

Install-Package RestSharp
Install-Package xunit
Install-Package xunit.runner.visualstudio
Install-Package Microsoft.NET.Test.Sdk
Install-Package Newtonsoft.Json
You can install these using NuGet Package Manager in Visual Studio.

4. Run the Tests
Open Test Explorer in Visual Studio.

Click Run All Tests or right-click a specific test to run it individually.

✅ Smoke Tests Included
Create Booking

Update Booking

Delete Booking

These represent the basic working functionality and are used to verify that the system is testable.

❌ Negative Test Cases
Test cases are also included for:

Missing required fields

Invalid dates

Incorrect auth tokens

## 🐞 Bug Report: 
Bug 1: Create Booking Returns 500 Instead of 4xx on Missing Fields

### Steps to Reproduce
1. Send a `POST` request to `https://restful-booker.herokuapp.com/booking`
2. Use a JSON body with missing required fields, such as omitting `firstname` and `lastname`:

```json
{
  "totalprice": 100,
  "depositpaid": true,
  "bookingdates": {
    "checkin": "2024-06-01",
    "checkout": "2024-06-10"
  },
  "additionalneeds": "Breakfast"
}
Expected Behavior
The API should return an appropriate 4xx client error, such as:

400 Bad Request – if validation fails due to missing required fields.

Or 403 Forbidden – if the input violates expected schema rules.

Actual Behavior
The API returns 500 Internal Server Error, which usually indicates a server-side 
failure or unhandled exception.

Request/Response Details
Request:

POST /booking HTTP/1.1
Host: restful-booker.herokuapp.com
Content-Type: application/json

{
  "totalprice": 100,
  "depositpaid": true,
  "bookingdates": {
    "checkin": "2024-06-01",
    "checkout": "2024-06-10"
  },
  "additionalneeds": "Breakfast"
}
Response:

HTTP/1.1 500 Internal Server Error
Content-Type: text/plain; charset=utf-8

Internal Server Error
Summary
This seems to be a server-side issue where invalid input causes the system 
to fail instead of returning a structured validation error. 
Bug 2:
Ping Endpoint Returns 201 Instead of 200

### Steps to Reproduce
1. Send a `GET` request to `https://restful-booker.herokuapp.com/ping`
2. Observe the status code returned.

---

### Expected Behavior
- The API should return a **200 OK** status code, indicating that 
the service is up and responding successfully.

---

### Actual Behavior
- The API returns a **201 Created** status code, 
which is typically used to indicate that a resource has been successfully created. 
This is not appropriate for a simple health-check endpoint.

---

### Request/Response Details

**Request:**
GET /ping HTTP/1.1
Host: restful-booker.herokuapp.com


**Response:**
HTTP/1.1 201 Created


### Summary
The `/ping` endpoint should return a **200 OK** status code instead of 
**201 Created** as it is a health-check endpoint and not creating any resources. 
Returning `200 OK` would better align with the expected behavior for such endpoints.
###


👩‍💻 Author
Djurdjina Komlenovic


