using Ynov.Business.Models;

namespace Ynov.Business.IServices;

public interface IUserServices
{
    public BusinessResult<List<User>> Get();

    public BusinessResult<User> Get(long id);

    public BusinessResult<User> Add(User user);
    
    public BusinessResult<User> ModifyUserName(long id, User user);
    
    public BusinessResult<User> ChangePassword(long id, User user, string oldPassword);

    public BusinessResult Delete(long id);
}