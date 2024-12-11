namespace BookStore.Api.Dtos;

public record Book
(
    string Title,
    string Culture,
    Author[] Authors
);