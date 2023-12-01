using AutoMapper;
using OfficeFlow.Application.Users.Commands.EditUsers;
using OfficeFlow.Application.EFiles.Commands.EditEFiles;
using OfficeFlow.Application.EFilesDocuments.Commands.EditEFilesDocuments;

namespace OfficeFlow.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Users

            CreateMap<Users.Models.CreateModel, Domain.Entities.Users>()
                .ForMember(e => e.PublicId, o => o.Ignore())
                .ForMember(e => e.Address, o => o.MapFrom(s => new Domain.Entities.UsersAddress()
                {
                    Country = s.Country,
                    City = s.City,
                    PostalCode = s.PostalCode,
                    Street = s.Street,
                    HouseNumber = s.HouseNumber,
                    ApartmentNumber = s.ApartmentNumber,
                }));

            CreateMap<Domain.Entities.Users, Users.Models.ListModel>()
                .ForMember(e => e.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.Address.PostalCode))
                .ForMember(e => e.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(e => e.HouseNumber, o => o.MapFrom(s => s.Address.HouseNumber))
                .ForMember(e => e.ApartmentNumber, o => o.MapFrom(s => s.Address.ApartmentNumber));

            CreateMap<Domain.Entities.Users, Users.Models.DetailsModel>()
                .ForMember(e => e.Country, o => o.MapFrom(s => s.Address.Country))
                .ForMember(e => e.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.Address.PostalCode))
                .ForMember(e => e.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(e => e.HouseNumber, o => o.MapFrom(s => s.Address.HouseNumber))
                .ForMember(e => e.ApartmentNumber, o => o.MapFrom(s => s.Address.ApartmentNumber));

            CreateMap<Users.Models.DetailsModel, EditUsersCommand>();

            //EFiles

            CreateMap<EFiles.Models.CreateModel, Domain.Entities.EFiles>()
                .ForMember(e => e.PublicId, o => o.Ignore());

            CreateMap<Domain.Entities.EFiles, EFiles.Models.ListModel>()
                .ForMember(e => e.UserFirstName, o => o.MapFrom(s => s.User!.FirstName))
                .ForMember(e => e.UserLastName, o => o.MapFrom(s => s.User!.LastName));

            CreateMap<Domain.Entities.EFiles, EFiles.Models.DetailsModel>()
                .ForMember(e => e.UserFirstName, o => o.MapFrom(s => s.User!.FirstName))
                .ForMember(e => e.UserLastName, o => o.MapFrom(s => s.User!.LastName))
                .ForMember(e => e.Documents, o => o.MapFrom(s => s.EFileDocuments));

            CreateMap<EFiles.Models.DetailsModel, EditEFilesCommand>();

            //EFileDocuments

            CreateMap<EFilesDocuments.Models.CreateModel, Domain.Entities.EFileDocuments>()
                .ForMember(e => e.DocumentContentType, o => o.MapFrom(s => s.DocumentContent != null ? s.DocumentContent.ContentType : null))
                .ForMember(e => e.DocumentName, o => o.MapFrom(s => s.DocumentContent != null ? s.DocumentContent.FileName : null))
                .ForMember(e => e.DocumentContent, o => o.Ignore());

            CreateMap<EFilesDocuments.Models.EditModel, Domain.Entities.EFileDocuments>()
                .ForMember(e => e.DocumentContentType, o => o.Ignore())
                .ForMember(e => e.DocumentName, o => o.Ignore())
                .ForMember(e => e.DocumentContent, o => o.Ignore());

            CreateMap<EFilesDocuments.Models.DetailsModel, EditEFilesDocumentsCommand>()
                .ForMember(e => e.Date, o => o.MapFrom(s => Convert.ToDateTime(s.Date)))
                .ForMember(e => e.DateFrom, o => o.MapFrom(s => Convert.ToDateTime(s.DateFrom)))
                .ForMember(e => e.DateTo, o => o.MapFrom(s => Convert.ToDateTime(s.DateTo)));

            CreateMap<Domain.Entities.EFileDocuments, EFilesDocuments.Models.ListModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom.HasValue ? src.DateFrom.Value.ToString("dd-MM-yyyy") : string.Empty))
                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo.HasValue ? src.DateTo.Value.ToString("dd-MM-yyyy") : string.Empty));

            CreateMap<Domain.Entities.EFileDocuments, EFilesDocuments.Models.DetailsModel>()
                .ForMember(e => e.PublicId, o => o.MapFrom(s => s.EFile != null ? s.EFile.PublicId : Guid.Empty))
                .ForMember(e => e.UserFirstName, o => o.MapFrom(s => s.EFile != null && s.EFile.User != null ? s.EFile.User.FirstName : null))
                .ForMember(e => e.UserLastName, o => o.MapFrom(s => s.EFile != null && s.EFile.User != null ? s.EFile.User.LastName : null))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom.HasValue ? src.DateFrom.Value.ToString("dd-MM-yyyy") : string.Empty))
                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo.HasValue ? src.DateTo.Value.ToString("dd-MM-yyyy") : string.Empty));

            CreateMap<Domain.Entities.EFileDocuments, EFilesDocuments.Models.DocumentModel>();
        }
    }
}
