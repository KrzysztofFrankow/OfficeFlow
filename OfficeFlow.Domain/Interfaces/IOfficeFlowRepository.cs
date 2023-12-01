using OfficeFlow.Domain.Entities;

namespace OfficeFlow.Domain.Interfaces
{
    public interface IOfficeFlowRepository
    {
        Task Create(Users users);
        Task Create(EFiles eFiles);
        Task Create(EFileDocuments eFileDocuments);
        Task<IEnumerable<Users>> GetAllUsers();
        Task<IEnumerable<EFiles>> GetAllEFiles();
        Task<Users> GetUserByPublicId(Guid publicId);
        Task<EFiles> GetEFileByPublicId(Guid publicId);
        Task<EFileDocuments> GetEFileDocumentById(int id);
        Task<EFileDocuments> GetDocumentById(int id);
        Task Commit();
    }
}
