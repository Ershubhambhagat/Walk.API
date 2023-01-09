using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfiles:Profile
    {
        public RegionsProfiles()
        {
            CreateMap<Models.Domain.Region,Models.DTO.Region>()
                //If source and destination name is Change the use this 
            .ForMember(dest=>dest.Code2,options=>options.MapFrom(src=>src.Code));

        }
    }
}
