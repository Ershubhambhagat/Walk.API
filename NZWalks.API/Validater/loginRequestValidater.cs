using FluentValidation;

namespace NZWalks.API.Validater
{
    public class loginRequestValidater:AbstractValidator<Models.DTO.loginRequest>

    {
        public loginRequestValidater()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();    

        }
    }
}
