using Microsoft.AspNetCore.Identity;

namespace pustokApp;

public class CustomIdentityErrorDescriber:IdentityErrorDescriber
{
    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "Password must have at least one upper and one lower case letter"
        };
    }
}