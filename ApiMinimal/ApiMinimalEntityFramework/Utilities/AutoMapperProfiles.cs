using ApiMinimalEntityFramework.DTOs;
using ApiMinimalEntityFramework.Entities;
using AutoMapper;

namespace ApiMinimalEntityFramework.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<CreateGenreDTO, Genre>();
        }
    }
}
