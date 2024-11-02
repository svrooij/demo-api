namespace Svrooij.Users.Api.Service;

public class UserRepository
{
    private readonly List<Models.User> _users =
    [
        new Models.User { Id = "cb573fc6-1a0b-4fdf-a706-fb2bbd153bdf", Email = "fake@svrooij.io", Name = "Stephan van Rooij", FavoriteAnimal = "Dog" },
        new Models.User { Id = Guid.NewGuid().ToString(), Email = "second@svrooij.io", Name = "Other user", FavoriteAnimal = "Ant" }
    ];

    public Task<IEnumerable<Models.User>> GetUsersAsync() => Task.FromResult(_users.AsEnumerable());
    public Task<Models.User?> GetUserAsync(string id) => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    public Task<Models.User> AddUserAsync(Models.User user)
    {
        user.Id = Guid.NewGuid().ToString();
        _users.Add(user);
        return Task.FromResult(user);
    }
    public Task<Models.User?> UpdateUserAsync(string id, Models.User user)
    {
        var existing = _users.FirstOrDefault(u => u.Id == id);
        if (existing is null) return Task.FromResult<Models.User?>(null);

        existing.Email = user.Email;
        existing.Name = user.Name;
        existing.FavoriteAnimal = user.FavoriteAnimal;

        return Task.FromResult(existing);
    }

    public Task<bool> DeleteUserAsync(string id)
    {
        var existing = _users.FirstOrDefault(u => u.Id == id);
        if (existing is null) return Task.FromResult(false);

        _users.Remove(existing);
        return Task.FromResult(true);
    }
}
