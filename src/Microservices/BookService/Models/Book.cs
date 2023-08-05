namespace BookService.Models;

public class Book
{
    public Guid Id { get; init; }
    public string Title { get; private set; } = default!;
    public string Author { get; private set; } = default!;
    public double Price { get; private set; }
    public bool IsLoaned { get; private set; }
    public bool IsBought { get; private set; }
    public long CreationTimestampUnix { get; init; }
    public long EditedTimestampUnix { get; private set; }

    private Book() { }

    public Book(Guid id, string title, string author, double price)
    {
        Id = id;
        Title = title;
        Author = author;
        Price = price;
        IsLoaned = false;
        IsBought = false;
        CreationTimestampUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        EditedTimestampUnix = CreationTimestampUnix;
    }

    public void Update(Book book, long editedTimestampUnix)
    {
        Title = book.Title;
        Author = book.Author;
        Price = book.Price;
        EditedTimestampUnix = editedTimestampUnix;
    }

    public void Buy() => IsBought = true;

    public void Loan() => IsLoaned = true;

    public void Return() => IsLoaned = false;
}
