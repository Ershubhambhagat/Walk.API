using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validater
{
    public class addRegionrequestValidater: AbstractValidator<Models.DTO.AddRegionrequest>
    {

        public addRegionrequestValidater()
        {
            RuleFor(x => x.Code2).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
