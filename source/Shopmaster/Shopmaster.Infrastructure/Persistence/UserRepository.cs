using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(User user)
    {
        _applicationDbContext.Users.Add(user);
        _applicationDbContext.SaveChanges();
    }

    public User? GetByEmail(string email)
    {
        return _applicationDbContext.Users
            .FirstOrDefault(user => user.Email == email);
    }

    public User? GetById(Guid id)
    {
        return _applicationDbContext.Users
            .FirstOrDefault(user => user.Id == id);
    }

    public void Remove(User user)
    {
        _applicationDbContext.Users.Remove(user);
        _applicationDbContext.SaveChanges();
    }

    public void Update(User user)
    {
        _applicationDbContext.SaveChanges();
    }
}
