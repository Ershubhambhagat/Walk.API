using FluentValidation;

namespace NZWalks.API.Validater
{
    public class updateRegionRequestregionValidater : AbstractValidator<Models.DTO.UpdateRegionRequest>
    {
        public updateRegionRequestregionValidater()
        {
            RuleFor(x => x.Lat).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Long).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);


        }
    }
}
