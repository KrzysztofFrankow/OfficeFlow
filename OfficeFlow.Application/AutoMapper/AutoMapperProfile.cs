using AutoMapper;
using OfficeFlow.Application.Users.Commands.EditUsers;
using OfficeFlow.Application.EFiles.Commands.EditEFiles;
using OfficeFlow.Application.EFilesDocuments.Commands.EditEFilesDocuments;
using OfficeFlow.Application.Enums;
using OfficeFlow.Domain.Entities;
using OfficeFlow.Application.Absences.Commands.EditAbsences;
using OfficeFlow.Application.Absences.Commands.EditLimit;
using OfficeFlow.Application.Dictionaries.Commands.EditDictionaries;

namespace OfficeFlow.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Users

            CreateMap<Users.Models.CreateModel, Domain.Entities.Users>()
                .ForMember(e => e.PublicId, o => o.Ignore())
                .ForMember(e => e.PasswordHash, o => o.Ignore())
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
                .ForMember(e => e.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth.ToString("dd-MM-yyyy")))
                .ForMember(e => e.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.Address.PostalCode))
                .ForMember(e => e.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(e => e.HouseNumber, o => o.MapFrom(s => s.Address.HouseNumber))
                .ForMember(e => e.ApartmentNumber, o => o.MapFrom(s => s.Address.ApartmentNumber));

            CreateMap<Domain.Entities.Users, Users.Models.DetailsModel>()
                .ForMember(e => e.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth.ToString("dd-MM-yyyy")))
                .ForMember(e => e.Role, o => o.Ignore())
                .ForMember(e => e.Password, o => o.Ignore())
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
                .ForMember(e => e.DateFrom, o => o.MapFrom(s => string.IsNullOrEmpty(s.DateFrom) ? (DateTime?)null : DateTime.Parse(s.DateFrom)))
                .ForMember(e => e.DateTo, o => o.MapFrom(s => string.IsNullOrEmpty(s.DateTo) ? (DateTime?)null : DateTime.Parse(s.DateTo)));

            CreateMap<Domain.Entities.EFileDocuments, EFilesDocuments.Models.ListModel>()
                .ForMember(e => e.CategoryName, o => o.Ignore())
                .ForMember(e => e.TypeName, o => o.Ignore())
                .ForMember(e => e.Date, o => o.MapFrom(s => s.Date.ToString("dd-MM-yyyy")))
                .ForMember(e => e.DateFrom, o => o.MapFrom(s => s.DateFrom.HasValue ? s.DateFrom.Value.ToString("dd-MM-yyyy") : string.Empty))
                .ForMember(e => e.DateTo, o => o.MapFrom(s => s.DateTo.HasValue ? s.DateTo.Value.ToString("dd-MM-yyyy") : string.Empty));

            CreateMap<Domain.Entities.EFileDocuments, EFilesDocuments.Models.DetailsModel>()
                .ForMember(e => e.CategoryName, o => o.Ignore())
                .ForMember(e => e.TypeName, o => o.Ignore())
                .ForMember(e => e.PublicId, o => o.MapFrom(s => s.EFile != null ? s.EFile.PublicId : Guid.Empty))
                .ForMember(e => e.UserFirstName, o => o.MapFrom(s => s.EFile != null && s.EFile.User != null ? s.EFile.User.FirstName : null))
                .ForMember(e => e.UserLastName, o => o.MapFrom(s => s.EFile != null && s.EFile.User != null ? s.EFile.User.LastName : null))
                .ForMember(e => e.Date, o => o.MapFrom(s => s.Date.ToString("dd-MM-yyyy")))
                .ForMember(e => e.DateFrom, o => o.MapFrom(s => s.DateFrom.HasValue ? s.DateFrom.Value.ToString("dd-MM-yyyy") : string.Empty))
                .ForMember(e => e.DateTo, o => o.MapFrom(s => s.DateTo.HasValue ? s.DateTo.Value.ToString("dd-MM-yyyy") : string.Empty));

            CreateMap<Domain.Entities.EFileDocuments, EFilesDocuments.Models.DocumentModel>();

            //Absences

            CreateMap<Absences.Models.CreateModel, Domain.Entities.Absences>();

            CreateMap<Domain.Entities.Absences, Absences.Models.ListModel>()
                .ForMember(e => e.StatusName, o => o.MapFrom(s => ((AbsenceStatus)s.Status).GetDescription()))
                .ForMember(e => e.From, o => o.MapFrom(s => s.From.ToString("dd-MM-yyyy")))
                .ForMember(e => e.To, o => o.MapFrom(s => s.To.ToString("dd-MM-yyyy")))
                .ForMember(e => e.DateCreated, o => o.MapFrom(s => s.DateCreated.ToString("dd-MM-yyyy")))
                .ForMember(e => e.UserName, o => o.MapFrom(s => s.User != null ? s.User.FirstName + " " + s.User.LastName : ""));

            CreateMap<Domain.Entities.Absences, Absences.Models.DetailsModel>()
                .ForMember(e => e.TypeName, o => o.Ignore())
                .ForMember(e => e.UserName, o => o.MapFrom(s => s.User != null ? s.User.FirstName + " " + s.User.LastName : ""))
                .ForMember(e => e.DateCreated, o => o.MapFrom(s => s.DateCreated.ToString("dd-MM-yyyy")))
                .ForMember(e => e.From, o => o.MapFrom(s => s.From.ToString("dd-MM-yyyy")))
                .ForMember(e => e.To, o => o.MapFrom(s => s.To.ToString("dd-MM-yyyy")))
                .ForMember(e => e.StatusName, o => o.MapFrom(s => ((AbsenceStatus)s.Status).GetDescription()));

            CreateMap<Absences.Models.DetailsModel, EditAbsencesCommand>();

            CreateMap<Domain.Entities.Users, Absences.Models.UserLimitsModel>()
                .ForMember(e => e.UserName, o => o.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(e => e.Limits, o => o.MapFrom(s => s.Limits));

            CreateMap<Domain.Entities.Limits, Absences.Models.LimitsListModel>();

            CreateMap<Domain.Entities.Limits, Absences.Models.DetailsLimitModel>();

            CreateMap<Absences.Models.CreateLimitModel, Limits>();

            CreateMap<Absences.Models.DetailsLimitModel, EditLimitCommand>();

            //Dictionaries

            CreateMap<Dictionaries.Models.CreateModel, Domain.Entities.Dictionaries>();

            CreateMap<Domain.Entities.Dictionaries, Dictionaries.Models.ListModel>()
                .ForMember(e => e.Type, o => o.MapFrom(s => ((DictionaryType)s.Type).GetDescription()));

            CreateMap<Domain.Entities.Dictionaries, EditDictionariesCommand>();
        }
    }
}
