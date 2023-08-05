namespace AccountService.Data;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users").HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
    }
}
