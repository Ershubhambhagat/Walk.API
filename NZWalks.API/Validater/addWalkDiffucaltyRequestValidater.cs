using FluentValidation;

namespace NZWalks.API.Validater
{
    public class addWalkDiffucaltyRequestValidater : AbstractValidator<Models.DTO.AddWalkDiffucaltyRequest>
    {
        public addWalkDiffucaltyRequestValidater()
        {
            RuleFor(x => x.Code).NotEmpty();

        }
    }

}
