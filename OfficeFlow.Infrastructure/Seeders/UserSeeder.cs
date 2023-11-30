using OfficeFlow.Domain.Entities;
using OfficeFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Infrastructure.Seeders
{
    public class UserSeeder
    {
        private readonly OfficeFlowDbContext _dbContext;

        public UserSeeder(OfficeFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                if(!_dbContext.Users.Any())
                {
                    var admin = new Users()
                    {
                        FirstName = "Administrator",
                        LastName = "Aplikacji",
                        Email = "Administrator@admin.pl",
                        PhoneNumber = "123456789",
                        DateOfBirth = DateTime.Now,
                        CreatedBy = 0,
                        Address = new UsersAddress()
                        {
                            Country = "Polska",
                            City = "Zabrze",
                            PostalCode = "31-803",
                            Street = "Ulica Administratora",
                            HouseNumber = "1",
                            ApartmentNumber = "10",
                        }
                    };
                    _dbContext.Users.Add(admin);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
