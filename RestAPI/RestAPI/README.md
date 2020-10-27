# README #

This is the rest API for tax calculations.

## Generating swagger definitions ##

The service can automatically generate a swagger definitions, thanks to the inclusion of Swashbuckle.
To generate the swagger definitions

URL https://localhost:5001/swagger/index.html

The url will present you with the Swagger dashboard:

## Options: ##

CalculateTax
FetchTaxCalculation
---
CalculateTax properties:
income : float
postalCode : string

FetchTaxCalculation properties:
postalCode : string

When the dashboard loads, please expand call, insert property values and Execute call.

###### Serilog
Serilog implementation is dane and can be managed to write to required databases (SEQ, Application Insights etc)


###### Notes
Notes are made throughout the solution with possible recommendations and refactoring options.


###### Testing
Nunit Implementation is done, but did not complete tests due to time constraints

##### Solution Structure
###### RestAPI
Application :   Contains Handlers and Requests
Constants:      Class for types of calculations
Contracts:      Models and Responses
Controllers:    Both controllers with endpoints to CalculateTax and FetchTaxCalculationType 
Options:        Swagger options
Services:       Contains Interfaces and Classes for the three responsibilities
appsettings.json
Program.cs
README.md
Startup.cs


###### RestAPI.DataAccess
DataServices:           SQL query designer and base query
DataTransferObjects:    DTO's for the responses
QueryProvider:          Interface and class for the QueryProviders
                        three available : - to receive an object
                                          - to purely execute a void
                                          - to receive an array back (would be used to return the whole list of types)
 
###### RestAPITests
FetchCalculationTypeTests:  A mock test.


                        
