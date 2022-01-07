# RizSoft.CleanArchitecture
Template Project for implementing Clean Architecture in several ASP.NET CORE UI

CORE contains the Domain MODELS and other common Entities
APPLICATION contains only the ABSTRACTIONS (Interfaces)
INFRASTRUCTURE contains the IMPLEMENTATIONS of the above abstractions
PRESENTATION contains several UI layers

Based on Northwind database, the purpose is to use in various Frontends always the same Service call (eg. employeeSvc.ListByCountryAsync("UK") that calls different Repositories
Change of repository is made only changing the dependency in program.cs

- WebUI uses a Sqlite db
- WebAPI uses a SqlServer Ef Core Db Context
- Blazor Server uses a  SqlServer Ef Core DbContextFactory (to avoid concurrency issues)
- Blazor WASM uses directly the API
- Console App uses directly the implementations like in the old days... :-)

See picture below
![Alt text](Ausil/CleanArchitecture.jpg?raw=true "Clean Architecture Schema")
