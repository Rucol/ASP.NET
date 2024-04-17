using AutoMapper;
using ContosoJourney.Models;
using StudentJourney.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<JourneyViewModel, Journey>();
        CreateMap<Journey, JourneyViewModel>();
        CreateMap<Enrollment, EnrollmentViewModel>();
        CreateMap<EnrollmentViewModel, Enrollment>();
    }
}
