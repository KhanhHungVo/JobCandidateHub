
# JobCandidateHubApi
## Technologies
* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [AutoMapper](https://automapper.org/)
* [CsvHelper](https://joshclose.github.io/CsvHelper/getting-started/)

## Getting Started
* Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
* `dotnet build`
* `dotnet run --project src/JobCandidateHub.Api`
*  Access local swagger page to test api http://localhost:5274/swagger/index.html
## Improvement notes
* Improve performance: replace line when updating record in csv file instead override all csv files 
* If plan to use DB then should replace CSV Storage services by DBContext and integrate with any Db (MSSQL, MySql, Postgre,...) in infrastructure layer

