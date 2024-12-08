# Broker Application Description

This application show case .net 6 api build following clean Architecture.<br>

# Rquirements.

.net sdk 8
visual studio /visual studio code
SQL server

# Setps to run.

1. Clone the repository
2. Build the application using dotnet build or can be build using Visual studio.
3. Since this app uses code first approach using entity framework use following command in package manager console to create db and schemas. Note: This is optional as app uses auto migration
   a. add-migration <Name>
   b. update-database
4. dotnet run
   Application should be up and running and details of api documentation should be should in swagger.

As keeping comments in code is a bad practise and creates code smells. Here is the short descriptions of different layers in clean architecture.

Domain Layer:
It is the most inner most layer of architecture. It holds the main domain information. All the other layers are build on top of domain layer. This layes holds entities, enums for schemas, data objects. All these components are the core definition of the application.

Application Layer:
It lies on top of Domain layer and hold all the logics for domain. It defines contracts for different external services as well as persistance services.

Infrastructure Layer:
It is the outer layer and is plugable from application layer. It implements the contracts of application layer to provide different services. As contract is closely related to domain, this layer implements those contract which gives the flexibilty to unplug this layer if different implementation for the contract is needed.

WebAPI:
It is the client facing layer. This layer can be api, mvc or someother client facing layer. This layer forwards the request to Application layer. Here Mediatr pattern is used to forware request from controller to Commands in application.



Note: This is done so that project can be completed with in given time frame. A lot of functionalities are self assumed and can be cleared during interview.
