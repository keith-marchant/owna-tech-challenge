# owna-tech-challenge

- [OWNA Tech Challenge](#owna-tech-challenge)
  - [Summary](#summary)
    - [Design Decisions & Issue Log](#design-decisions--issue-log)
  - [Specification](#specification)
    - [Specification Tooling](#specification-tooling)
    - [Specification Mocking](#specification-mocking)
  - [Testing](#testing)
  - [Running the solution](#running-the-solution)
    - [CREATE ORDER](#create-order)
    - [GET ORDER](#get-order)
    - [UPDATE ORDER](#update-order)

## Summary

This tech challenge is implemented using the [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) framework with [Mediatr](https://github.com/jbogard/MediatR) handling communication between layers.

### Design Decisions & Issue Log

1. This is an API only as I didn't have time to add anything further.
2. I used a memory cache for persistence (see code in the `Infrastructure` project) and thus records will be lost on restarts.
3. Validation is implemented using FluentValidator and triggered by the mediator pipeline. This keeps it out of the controllers and allows the code to be portable to a new runtime should that be required.
4. I ran out of time to implement a mapper so the mapping between `Entities` and `Dtos` is manually done.
5. I implemented the spec as described in the Word doc but if I were building a site for real I would create different resources for Products, Customers, etc. and then reference them by `Id`.
6. I ran out of time to add a full test suite but given time I would make sure every business operatrion is covered.
7. The error messages are not formatting properly to RFC7807 standard

## Specification

The specification is written using [OpenAPI](https://swagger.io/specification/) and may be accessed [here](./spec/ecommerce-api.v1.yaml).

### Specification Tooling

I use VS Code to edit the OpenAPI YAML along with the following extensions to help navigate and render it:
- [OpenAPI Swagger Editor](https://github.com/42Crunch/vscode-openapi)
- [redoc-cli](https://github.com/Redocly/redoc)

Builds can be done by running: `redoc-cli bundle .\ecommerce-api.v1.yaml` which will generate a redoc static html file. 

### Specification Mocking

When building an API it can be helpful to mock the spec for the purposes of front end development, test suite generation, and demonstration purposes. To mock this spec I recommend [Prism](https://meta.stoplight.io/docs/prism/ZG9jOjky-installation)

To mock this API execute `prism mock .\ecommerce-api.v1.yaml` and a mock server will launch at http://localhost:4010 so that you may run cURL (or other tooling of your choice) requests.

```
curl --request POST \
  --url http://localhost:4010/orders
```

Prism will display a log of all requests and will pull responses from the examples loaded into the specification.

## Testing

Some basic tests are available as unit tests to verify the functionality of the handlers. More integration tests should be added in future to further verify functionality.

## Running the solution

Please run the solution using: `dotnet run` from the `.\src\OWNA.ECommerce.Api` directory.

You may use the included swagger file to execute requests, or the following cURL statements:

### CREATE ORDER

```
curl --request POST \
  --url http://localhost:5261/Orders \
  --header 'Content-Type: application/json' \
  --data '{
  "customer": {
    "name": "string",
    "address": "string",
    "email": "string",
    "phone": "string"
  },
  "product": {
    "name": "string",
    "description": "string",
    "price": 0
  },
  "status": "Pending"
}'
```

### GET ORDER

```
curl --request GET \
  --url http://localhost:5261/Orders/c06c3f3c-0770-4956-b7a2-22ddda68cb01
```

### UPDATE ORDER

```
curl --request PUT \
  --url http://localhost:5261/Orders/c06c3f3c-0770-4956-b7a2-22ddda68cb01 \
  --header 'Content-Type: application/json' \
  --data '{
  "customer": {
    "name": "string2",
    "address": "string",
    "email": "string",
    "phone": "string"
  },
  "product": {
    "name": "string",
    "description": "string",
    "price": 1.0
  },
  "status": "Pending"
}'
```