using FluentValidation;

namespace NZWalks.API.Validater
{
    public class updateWalkDiffucaltyRequestValidater : AbstractValidator<Models.DTO.UpdateWalkDiffucaltyRequest>


    {

        public updateWalkDiffucaltyRequestValidater()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }


}
