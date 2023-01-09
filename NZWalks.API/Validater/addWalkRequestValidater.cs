using FluentValidation;

namespace NZWalks.API.Validater
{
    public class addWalkRequestValidater:AbstractValidator<Models.DTO.AddWalkRequest>

    {
        public addWalkRequestValidater()
        {
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x => x.Length).NotEmpty();



        }

    }
}
