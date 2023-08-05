namespace Contracts.Requests;

public record CreateBookRequest(
    string Title, 
    string Author, 
    double Price);
