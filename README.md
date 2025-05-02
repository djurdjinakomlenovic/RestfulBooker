# RestfulBooker
Repository created with the purpose to push an assignment solution.

This project contains automated smoke, positive, negative, and boundary tests for the Restful Booker API using C#, RestSharp, and xUnit.

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


