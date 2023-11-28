using Ynov.Business.Models;

namespace Ynov.Business.IRespositories;

public interface IUserRepository
{
    public List<User> Get();
    public User? Get(long id);
    public User Add(User user);
    public User? ModifyUserName(User user);
    public User? ChangePassword(User user);
    public void Delete(User user);
}