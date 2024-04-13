# Clean Architecture: Summary

## Summary
Clean Architecture is a form of architecture/software design approach based on a set of SOLID principles to separate applications into different layers. It was formerly known as Onion Architecture/Hexagonal Architecture/Ports and Adapters. Clean Architecture is a domain-centric approach to organizing dependencies. It follows the concept of having an organized set of project files in a single project/Solution space in well-labelled project folders (e.g., src, Models, Data, Controllers, etc.). Clean Architecture has a steep learning curve as project files may become overwhelming but makes it easier to test, deploy, and maintain applications while minimizing dependence on Infrastructure concerns and keeping the focus on domain logic.

## Pros of Clean Architecture
- Leverages .NET developers to efficiently focus on each logic and curate it accurately.
- Helps maintain coding practices, leading to improved application stability and security.
- Aids in writing effective automated test cases, focusing on core business logic.
- Aids in quickly adding new features, APIs, and third-party components.

## Cons of Clean Architecture
- Steep learning curve.
- Performance overhead and might be an overkill for small projects.
- Higher complexity.

## Rules of Clean Architecture
- Model all business rules and entities into the core project.
- All dependencies flow into the project.
- Inner projects define interfaces; outer projects implement them.

## What belongs in the Core Project?
- Interfaces
- Aggregates
- Entities, etc.

## What belongs in the Infrastructure Project?
- Repos
- API clients
- System clock, etc.

## What belongs in the Web Project?
- API Endpoints
- Controllers
- Views, etc.

## What belongs in the Shared Kernel Project?
- Base Entity
- Common Authentication
- Common Interfaces, etc.

**NOTE:** There are no Infrastructure dependencies in the shared kernel!
