using Ynov.Business.DTOitem;
using Ynov.Business.Exceptions;
using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.Business.Services;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordServices _passwordServices;

    public UserServices(IUserRepository userRepository, IPasswordServices passwordServices)
    {
        _userRepository = userRepository;
        _passwordServices = passwordServices;
    }


    public BusinessResult<List<User>> Get()
    {
        List<User> users = _userRepository.Get();

        return BusinessResult<List<User>>.FromSuccess(users);
    }

    public BusinessResult<User> Get(long id)
    {
        User? user = _userRepository.Get(id);

        if (user is null)
        {
            return BusinessResult<User>.FromError($"The user {id} do not exist", BusinessErrorReason.NotFound);
        }

        return BusinessResult<User>.FromSuccess(user);
    }
    
    public BusinessResult<User> Add(User uUser)
    {
        uUser.PasswordHash = _passwordServices.HashPassword(uUser.PasswordHash);
        
        try
        {
            User? user = _userRepository.Add(uUser);
            
            if (user is null)
            {
                return BusinessResult<User>.FromError("Error while adding user", BusinessErrorReason.BusinessRule);
            }
            return BusinessResult<User>.FromSuccess(user);
        }
        catch (UserAlreadyExistsException ex)
        {
            return BusinessResult<User>.FromError(ex.Message, BusinessErrorReason.DbConflict);
        }
    }


    public BusinessResult<User> ModifyUserName(long id, User uUser)
    {
        User? user = _userRepository.Get(id);
        
        if (user is null)
        {
            return BusinessResult<User>.FromError($"The user {id} do not exist", BusinessErrorReason.NotFound);
        }
        user.Name = uUser.Name;
        _userRepository.ModifyUserName(user);
        return BusinessResult<User>.FromSuccess(user);
    }
    
    public BusinessResult<User> ChangePassword(long id, User uUser, string oldPassword)
    {
        User? user = _userRepository.Get(id);
        
        if (user is null)
        {
            return BusinessResult<User>.FromError($"The user {id} do not exist", BusinessErrorReason.NotFound);
        }

        bool passwordVerification = _passwordServices.VerifyPassword(oldPassword, user.PasswordHash);

        if (!passwordVerification)
        {
            return BusinessResult<User>.FromError($"The user {id} do not exist", BusinessErrorReason.Forbidden);
        }
        
        user.PasswordHash = _passwordServices.HashPassword(uUser.PasswordHash);
        
        _userRepository.ChangePassword(user);
        return BusinessResult<User>.FromSuccess(user);
    }
    

    public BusinessResult Delete(long id)
    {
        User? user = _userRepository.Get(id);

        if (user is null)
        {
            return BusinessResult<User>.FromError($"The user {id} do not exist", BusinessErrorReason.NotFound);
        }
        _userRepository.Delete(user);
        return BusinessResult.FromSuccess();
    }
}