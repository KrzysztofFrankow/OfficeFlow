using Microsoft.EntityFrameworkCore;
using OfficeFlow.Application.Enums;
using OfficeFlow.Domain.Entities;
using OfficeFlow.Domain.Interfaces;
using OfficeFlow.Infrastructure.Persistence;

namespace OfficeFlow.Infrastructure.Repositories
{
    internal class OfficeFlowRepository : IOfficeFlowRepository
    {
        private readonly OfficeFlowDbContext _dbContext;

        public OfficeFlowRepository(OfficeFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(object obj)
        {
            _dbContext.Add(obj);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Remove(object obj)
        {
            _dbContext.Remove(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dictionaries>> GetAllDictionaries()
        {
            return await _dbContext.Dictionaries.ToListAsync();
        }

        public async Task<IEnumerable<Dictionaries>> GetEFileCategoryDictionaries()
        {
            return await _dbContext.Dictionaries.Where(w => w.Type == (int)DictionaryType.EFileCategory).ToListAsync();
        }

        public async Task<IEnumerable<Dictionaries>> GetEFileTypeDictionaries()
        {
            return await _dbContext.Dictionaries.Where(w => w.Type == (int)DictionaryType.EFileType).ToListAsync();
        }

        public async Task<IEnumerable<Dictionaries>> GetAbsenceTypeDictionaries()
        {
            return await _dbContext.Dictionaries.Where(w => w.Type == (int)DictionaryType.AbsenceType).ToListAsync();
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _dbContext.Users.OrderByDescending(a => a.DateCreated).ToListAsync();
        }

        public async Task<IEnumerable<Absences>> GetAbsencesToAccept()
        {
            return await _dbContext.Absences.Include(i => i.User).Where(w => w.Status == (int)AbsenceStatus.InProgress).OrderByDescending(a => a.DateModified).ToListAsync();
        }

        public async Task<IEnumerable<Absences>> GetAbsencesAccepted()
        {
            return await _dbContext.Absences.Include(i => i.User).Where(w => w.Status == (int)AbsenceStatus.Accepted).OrderByDescending(a => a.DateModified).ToListAsync();
        }

        public async Task<IEnumerable<Absences>> GetAbsencesRejected()
        {
            return await _dbContext.Absences.Include(i => i.User).Where(w => w.Status == (int)AbsenceStatus.Rejected).OrderByDescending(a => a.DateModified).ToListAsync();
        }

        public async Task<IEnumerable<Absences>> GetAbsencesMyList(int userId)
        {
            return await _dbContext.Absences.Include(i => i.User).Where(w => w.UserId == userId).OrderByDescending(a => a.DateModified).ToListAsync();
        }

        public async Task<IEnumerable<EFiles>> GetAllEFiles()
        {
            return await _dbContext.EFiles.Include(i => i.User).OrderByDescending(a => a.DateModified).ToListAsync();
        }

        public async Task<Users> GetUserByPublicId(Guid publicId)
        {
            return await _dbContext.Users.Include(i => i.Role).FirstAsync(w => w.PublicId == publicId);
        }

        public async Task<Limits> GetLimitByPublicId(Guid publicId)
        {
            return await _dbContext.Limits.FirstAsync(w => w.PublicId == publicId);
        }

        public async Task<EFiles> GetEFileByPublicId(Guid publicId)
        {
            return await _dbContext.EFiles.Include(i => i.User).Include(i => i.EFileDocuments!.OrderByDescending(o => o.DateModified)).FirstAsync(w => w.PublicId == publicId);
        }

        public async Task<EFileDocuments> GetEFileDocumentById(int id)
        {
            return await _dbContext.EFileDocuments.Include(i => i.EFile).ThenInclude(i => i.User).FirstAsync(w => w.Id == id);
        }

        public async Task<EFileDocuments> GetDocumentById(int id)
        {
            return await _dbContext.EFileDocuments.FirstAsync(w => w.Id == id);
        }

        public bool CheckEmail(string email)
        {
            return _dbContext.Users.Any(u => u.Email == email);
        }

        public async Task<Users?> GetUserForLogin(string email)
        {
            return await _dbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(w => w.Email == email);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Dictionaries> GetDictionaryById(int id)
        {
            return await _dbContext.Dictionaries.FirstAsync(w => w.Id == id);
        }

        public async Task<Absences> GetAbsenceByPublicId(Guid publicId)
        {
            return await _dbContext.Absences.Include(i => i.User).FirstAsync(w => w.PublicId == publicId);
        }

        public async Task<Users> GetUserLimitsByPublicId(Guid publicId)
        {
            return await _dbContext.Users.Include(i => i.Limits).FirstAsync(w => w.PublicId == publicId);
        }

        public async Task<IEnumerable<Absences>> GetAbsencesByUserIdAndType(int userId, int type)
        {
            return await _dbContext.Absences.Where(w => w.UserId == userId && w.Type == type && w.Status == (int)AbsenceStatus.Accepted).ToListAsync();
        }

        public async Task<Limits> GetLimitByUserIdAndType(int userId, int type)
        {
            return await _dbContext.Limits.FirstAsync(w => w.UserId == userId && w.Type == type);
        }
    }
}
