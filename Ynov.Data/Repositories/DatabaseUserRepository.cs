using Ynov.Business.DTOitem;
using Ynov.Business.Exceptions;
using Ynov.Business.IRespositories;
using Ynov.Business.Models;
using Ynov.Data.Contexts;

namespace Ynov.Data.Repositories;

public class DatabaseUserRepository : IUserRepository
{
    private readonly BoardDbContext _context;

    public DatabaseUserRepository(BoardDbContext context)
    {
        _context = context;
    }

    public List<User> Get()
    {
        return _context.Users.ToList();
    }

    public User? Get(long id)
    {
        return _context.Users.FirstOrDefault(card => card.Id == id);
    }

    public User Add(User user)
    {
        var unique = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        if (unique != null)
        {
            throw new UserAlreadyExistsException("A user with this email already exists.");
        }
        
        // verification of the password requirement in the front.
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? ModifyUserName(User uUser)
    {
        var user = _context.Users.Find(uUser.Id);
        if (user != null)
        {
            user.Name = uUser.Name;
            _context.SaveChanges();
        }
        return user;
    }

    public User? ChangePassword(User uUser)
    {
        var user = _context.Users.Find(uUser.Id);
        if (user != null)
        {
            user.PasswordHash = uUser.PasswordHash;
            _context.SaveChanges();
        }

        return user;
    }
    
    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}