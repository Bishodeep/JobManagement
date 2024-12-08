# Broker Application Description

This application show case .net 6 api build following clean Architecture. I used clean architecture because this will handle the scenario mentioned in assesment. As the application layer here defines the repository contracts and infrastructure layer is implementing it. We can in later stages of application cleanly unplug the repository implementation and replace it with other database options.<br>

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
It is the client facing layer. This layer can be api, mvc or some other client facing layer. This layer forwards the request to Application layer. Here Mediatr pattern is used to forware request from controller to Commands in application.

# List of Improvements:
1. Endpoints can be authorized using some jwt token created using Identity
2. Data validations can be optimized using fluent validator. This will give more leverage on validating each properties uniquely.
3. More global exceptions can be added in filter for fine tuned exception handling.
4. Further more if application grows it can use CQRS pattern for seperating command and queries. (Mediatr is a good package for implementing this pattern) 

# List of Assumptions:
1. One get endpoint has been added so that added or updated data can be verified.

# Time utilized
4 hr ( 3 hr development 1 hr unit test)
