using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<EFiles>> GetAllEFiles()
        {
            return await _dbContext.EFiles.Include(i => i.User).ToListAsync();
        }

        public async Task<Users> GetUserByPublicId(Guid publicId)
        {
            return await _dbContext.Users.FirstAsync(w => w.PublicId == publicId);
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
    }
}
