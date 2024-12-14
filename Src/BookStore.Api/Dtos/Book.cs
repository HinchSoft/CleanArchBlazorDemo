namespace BookStore.Api.Dtos;

public record Book
(
    string Title,
    string Culture,
    string Edition,
    Author[] Authors,
    string Publisher,
    Release Release
);
