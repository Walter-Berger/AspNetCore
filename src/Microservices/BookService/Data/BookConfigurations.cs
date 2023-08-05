namespace BookService.Data;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books").HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
    }
}
