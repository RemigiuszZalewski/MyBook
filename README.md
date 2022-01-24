# MyBook
MyBook is a .NET Core 5 application that allows users to trade books. App has been built based on CQRS (Queries and Commands), Domain models, Entity Framework Core as an ORM and Mediator design pattern. Authentication/Authorization is done using IdentityServer4 as the separated layer to grant JWT tokens. The whole flow of application has been unit tested and integration tested using XUnit.

Test results:

![image](https://user-images.githubusercontent.com/79413343/150845702-de4bf051-38bd-494c-b92b-a2c2d0830844.png)

Application is integrated with Azure Pipelines:

![Build Status](https://dev.azure.com/remikzalewski/MyBook/_apis/build/status/MyBook-ASP.NET%20Core-CI?branchName=main)
