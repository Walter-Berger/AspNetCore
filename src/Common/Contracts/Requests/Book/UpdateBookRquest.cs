namespace Contracts.Requests;

public record UpdateBookRquest(
    string Author,
    string Title,
    double Price);
