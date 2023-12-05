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

        public async Task Create(Users users)
        {
            _dbContext.Add(users);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(EFiles eFiles)
        {
            _dbContext.Add(eFiles);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(Absences absences)
        {
            _dbContext.Add(absences);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<Absences>> GetAbsencesToAccept()
        {
            return await _dbContext.Absences.Include(i => i.User).Where(w => w.Status == (int)AbsenceStatus.InProgress).ToListAsync();
        }

        public async Task<IEnumerable<EFiles>> GetAllEFiles()
        {
            return await _dbContext.EFiles.Include(i => i.User).ToListAsync();
        }

        public async Task<Users> GetUserByPublicId(Guid publicId)
        {
            return await _dbContext.Users.Include(i => i.Role).FirstAsync(w => w.PublicId == publicId);
        }

        public async Task<EFiles> GetEFileByPublicId(Guid publicId)
        {
            return await _dbContext.EFiles.Include(i => i.User).Include(i => i.EFileDocuments).FirstAsync(w => w.PublicId == publicId);
        }

        public async Task Create(EFileDocuments eFileDocuments)
        {
            _dbContext.Add(eFileDocuments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EFileDocuments> GetEFileDocumentById(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await _dbContext.EFileDocuments.Include(i => i.EFile).ThenInclude(i => i.User).FirstAsync(w => w.Id == id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public async Task<EFileDocuments> GetDocumentById(int id)
        {
            return await _dbContext.EFileDocuments.FirstAsync(w => w.Id == id);
        }

        public bool CheckEmail(string email)
        {
            return _dbContext.Users.Any(u => u.Email == email);
        }

        public async Task<Users> GetUserForLogin(string email)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Users.Include(i => i.Role).FirstOrDefaultAsync(w => w.Email == email);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }
    }
}
