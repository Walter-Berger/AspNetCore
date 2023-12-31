﻿namespace AccountService.Models;

public class User
{
    public Guid Id { get; init; }
    public string Email { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public long CreationTimestampUnix { get; init; }
    public long EditedTimestampUnix { get; private set; }

    private User() { }

    public User(Guid id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CreationTimestampUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        EditedTimestampUnix = CreationTimestampUnix;
    }

    public void Update(User user, long editedTimestampUnix)
    {
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        EditedTimestampUnix = editedTimestampUnix;
    }
}
