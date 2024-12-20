# Clean Architecture - Blazor Demo

This solution uses an idea and some code from [Zoran Horvats](https://www.youtube.com/@zoran-horvat/featured) video on
[EF Core Configuration](https://www.youtube.com/watch?v=xpm2nRpvvA8).

The clean architecture approach in this example can be extended for further 
separation of concerns, for example the business logic and domain objects could be separated.

## Core / Domain
This layer is at the centre the base layer that all other layers reference,
it doesn't have any dependencies on any other layer.

This layer contains the domain entities and business logic, including
service interface definitions for other layers for example data repositories.

## Infrastructure
This layer contains connections to external services and databases,
this is where Entity Framework and clients to other external
services should reside.