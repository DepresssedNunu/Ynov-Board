namespace Ynov.Business.Dtos;

public record UserDto(
    string Name,
    string Email,
    string Password
);

public record ModifyUserNameDto(
    string Name
);

public record ModifyPasswordDto(
    string OldPassword, 
    string NewPassword
);