using FluentValidation;

namespace NZWalks.API.Validater
{
    public class updateWalkRequestValidater:AbstractValidator<Models.DTO.UpdateWalkRequest>
    {
        public updateWalkRequestValidater()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).NotEmpty();
        }
    }

}
