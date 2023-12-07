using OfficeFlow.Domain.Entities;

namespace OfficeFlow.Domain.Interfaces
{
    public interface IOfficeFlowRepository
    {
        Task Commit();
        Task Create(object obj);
        Task Remove(object obj);

        Task<IEnumerable<Users>> GetAllUsers();
        Task<IEnumerable<Dictionaries>> GetAllDictionaries();
        Task<IEnumerable<Dictionaries>> GetEFileCategoryDictionaries();
        Task<IEnumerable<Dictionaries>> GetEFileTypeDictionaries();
        Task<IEnumerable<Dictionaries>> GetAbsenceTypeDictionaries();
        Task<IEnumerable<Absences>> GetAbsencesToAccept();
        Task<IEnumerable<Absences>> GetAbsencesAccepted();
        Task<IEnumerable<Absences>> GetAbsencesRejected();
        Task<IEnumerable<Absences>> GetAbsencesMyList(int userId);
        Task<IEnumerable<Role>> GetAllRoles();
        bool CheckEmail(string email);
        Task<IEnumerable<EFiles>> GetAllEFiles();
        Task<Users> GetUserByPublicId(Guid publicId);
        Task<Absences> GetAbsenceByPublicId(Guid publicId);
        Task<IEnumerable<Absences>> GetAbsencesByUserIdAndType(int userId, int type);
        Task<Limits> GetLimitByPublicId(Guid publicId);
        Task<Limits> GetLimitByUserIdAndType(int userId, int type);
        Task<Users> GetUserLimitsByPublicId(Guid publicId);
        Task<Role?> GetRoleById(int id);
        Task<Dictionaries> GetDictionaryById(int id);
        Task<Users?> GetUserForLogin(string email);
        Task<EFiles> GetEFileByPublicId(Guid publicId);
        Task<EFileDocuments> GetEFileDocumentById(int id);
        Task<EFileDocuments> GetDocumentById(int id);
        
        
    }
}
