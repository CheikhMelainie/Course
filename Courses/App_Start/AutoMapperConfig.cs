using AutoMapper;
using Courses.Data;
using Courses.Models;

namespace Courses
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryModel>()
                .ForMember(dst => dst.Id, src => src.MapFrom(e => e.ID))
                .ForMember(dst => dst.ParentId, src => src.MapFrom(e => e.Category2.Parent_id))
                .ForMember(dst => dst.ParentName, src => src.MapFrom(e => e.Category2.Name))
                .ReverseMap();
            });

            Mapper = config.CreateMapper();
        }
    }
}