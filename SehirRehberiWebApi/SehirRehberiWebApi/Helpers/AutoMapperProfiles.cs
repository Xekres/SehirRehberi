using AutoMapper;
using SehirRehberiWebApi.Dtos;
using SehirRehberiWebApi.Models;

namespace SehirRehberiWebApi.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                });

            //yani bu PhotoUrl i kaynakta ki (city) fotolorundan IsMain olanından Map et dedim.
            CreateMap<City, CityForDetailDto>();
        }
    }
}
