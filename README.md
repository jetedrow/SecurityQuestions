# Security Questions Applicaiton
An *over-engineered* console app that stores security questions for a specified person.  It utilizes
several design patterns and libraries to separate concerns, and allow for a more usable and testable
application that can be built upon and expanded.

This software utilizes patterns that would be more suitable for enterprise software, but they are
being illustrated here as an example.  Normally, a problem of this size would not warrant such a
complex communication layer with 

## Design Patterns
- **Dependency Injection**
	- The application utilizes a simple DI stack to inject libraries and other common services that are used
	througout the application.
- **Mediator Pattern (CQRS)**
	- The application uses the mediator pattern (via the MediatR library) to separate concerns of
	business logic (back-end work) and presentation.
	- This pattern allows for each "feature" to be independently tested and verified.

## Libraries
- **[MediatR](https://github.com/jbogard/MediatR)**
	- As discussed above, this library enables the use of the mediator pattern (CQRS) to separate concerns.
- **[Spectre.Console](https://github.com/spectreconsole/spectre.console)**
	- This helper library allows for a great user experience while interacting with a console
	application, allowing for more interactivity for user choices, which expands on the original
	reqirements.
- **EntityFrameworkCore**
	- Using EF Core 7 for data persistance.
	- Utilizing code-first migrations to deploy schema changes to the database.


## Improvements
There are several places this code could be improved in the future.
- A repository pattern could also be established and injected so the logic of the data is further
abstracted from the features layer, if data layer communication becomes more complex.
- This current code uses the SQLite provider for ease of build and portability.  If this were to be
a full-fledged application, a more persitant data store (such as SQL server) would allow for things such
as encryption and/or data masking to ensure security concerns are addressed for the confidentail
information shared in the application.
- An end state should be entered into the flows as well, to allow for a clean exit of the application
in the future.
- Instead of re-doing the answers as required, it may be a better flow in the future to allow the user
to add/change their selections, and update selections they have already answered vs. completely
re-doing the store flow.