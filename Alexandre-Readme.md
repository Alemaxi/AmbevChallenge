# Ambev Test

Este repositório contém a solução desenvolvida para o teste. A seguir, são apresentadas as principais escolhas e decisões técnicas adotadas durante o desenvolvimento.

## Principais Escolhas e Decisões

# Ambev Test

This repository contains the solution developed for the test. Below are the main choices and technical decisions made during development.

## Main Choices and Decisions

The CQRS (Command Query Responsibility Segregation) pattern was adopted as specified in the instructions. This approach allows the separation of read and write operations, contributing to greater scalability and easier maintenance of the system. I observed the initial example that had a request class which was later mapped to a command, and understanding that the Command by itself already served to abstract the backend structure of the application and to simplify the architecture and development, I opted to place the command directly in the controller to receive the requests.

Note: Another reason for choosing not to use the request classes that were used in previous examples was redundancy... The Command already performed validation and there was a similar validation in the request, so it did not make sense to maintain this redundancy. It would have made the code harder to maintain and slowed down development without providing any added benefit.

Furthermore, I took advantage of the fact that all commands had a validator and automated the process to apply validations automatically without needing to call the validate method every time. This allowed the code to focus solely on what was essential for the feature to work, making it standardized and less prone to error. This implementation was carried out through the abstract class GenericHandler. I also opted to change the call for validation from FluentValidation's validate to the ValidateAndThrowAsync call, which already throws the validation error to be handled by the middleware already set up for exception handling.

To handle the events necessary for sending a new sale insertion, deletion, cancellation, or modification, I chose to override the CommitAsync directly in the context, tracking the changes there to send them to a queue if needed. However, as requested, I configured it to print the event to the console window. This choice is extremely scalable, as it does not directly affect the main code, and every record that undergoes an insert or update will mandatorily pass through it.

## Notes

The database used were postgres as asked in the instructions. The database has been used through docker since is simpler to run and also contributes to keep a clean environment.

were performed unit tests for Controller, Handlers and repositories.
