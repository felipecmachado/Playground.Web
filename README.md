# Playground.Web

Playground.Web - This is a full stack web application created for fun and trying new things.

- Architecture: NET Core 3.1 / VueJS / MySQL;
- Design Patterns and Practices: SOLID principles, Code-First, EF Core, NET Core Native DI, Unit Tests;
- Used Vue Paper Dashboard as a base template (Lots of thanks to Creative Tim);

## Architecture
![image](https://user-images.githubusercontent.com/2963750/77981171-93226600-72df-11ea-8023-827e751c5a40.png)
```
Solution:
- Playground.Web.Domain
	- Entity models
- Playground.Web.Shared
	- DTOs
- Playground.Web.Business
	- Services with all the business logic; Services interfaces;
- Playground.Web.Infrastructure
	- Data access Layer
	- Configuration, Initializers and Infrastructure.
	- (Skipped Repository pattern for now since DbContext implements most of it)
- Playground.Web.API
	- API controllers;
- Playground.Web.BackgroundServices
	- Background services using IHostedService interface
- Playground.Web.Tests
	- Unit tests and smoke tests
	- Used NUnit, FakeItEasy, FluentAssertions
```

## What next
- Finish all features;
- Add Heartbeat using SignalR for all Background services;
- Isolate API from Entity models (create DTOs for everything and maybe use AutoMapper to make it easier);
- Apply Repository pattern;
- Review if more abstraction is needed (maybe not since the project isn't that big);
- Write more unit tests (it should cover most of the web app);

## API
![image](https://user-images.githubusercontent.com/2963750/77974222-dc1cef00-72cc-11ea-8afd-7bcc3f571668.png)

## Layout
![image](https://user-images.githubusercontent.com/2963750/78040956-86852880-7346-11ea-9b34-634d42caade6.png)

## How to test it:
**Frontend**:  
Clone this repository and enter the frontend directory  
Install the dependencies by running npm install  
Start the project by running npm run-script dev (remember to start the backend first)  

**Backend**:  
Clone this repository and enter the backend directory  
Go to the Startup.cs and change the AddDbContext options from MySql to InMemoryDatabase  
Enter the Web.Api directory  
Start the project by running dotnet run  
Navigate to the http://localhost:5000/swagger  
You can use the login admin/admin to get a token and add to Swagger

## Unit tests
![image](https://user-images.githubusercontent.com/2963750/77977031-ee9b2680-72d4-11ea-8518-90cf241f50c1.png)

## References:
> Built using: https://dotnet.microsoft.com/download/dotnet-core/3.1

> Creative Tim templates: https://www.creative-tim.com/templates/vuejs-free

License
This project is licensed under the MIT license. Copyright (c) 2020 Felipe Machado.
