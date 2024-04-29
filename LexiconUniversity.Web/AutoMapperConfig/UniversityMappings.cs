using AutoMapper;
using LexiconUniversity.Core.Entities;
using LexiconUniversity.Web.Models;

namespace LexiconUniversity.Web.AutoMapperConfig;

public class UniversityMappings : Profile
{
    public UniversityMappings()
    {
        CreateMap<Student, StudentSummaryViewModel>()
            .ForMember(
                dest => dest.FullName,
                opts => opts.MapFrom<string>(s => $"{s.FirstName} {s.LastName}"));
    }
}
