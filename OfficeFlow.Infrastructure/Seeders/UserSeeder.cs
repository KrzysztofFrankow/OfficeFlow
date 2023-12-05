using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPasswordHasher<Domain.Entities.Users> _passwordHasher;

        public UserSeeder(OfficeFlowDbContext dbContext, IPasswordHasher<Domain.Entities.Users> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                if(!_dbContext.Roles.Any())
                {
                    var roles = new List<Role>()
                    {
                        new Role()
                        {
                            Name = "Admin"
                        },
                        new Role()
                        {
                            Name = "Manager"
                        },
                        new Role()
                        {
                            Name = "User"
                        }
                    };

                    _dbContext.Roles.AddRange(roles);
                    await _dbContext.SaveChangesAsync();
                }

                if(!_dbContext.Users.Any())
                {
                    var admin = new Users()
                    {
                        FirstName = "Administrator",
                        LastName = "Aplikacji",
                        Email = "Administrator@admin.pl",
                        PasswordHash = "1",
                        PhoneNumber = "123456789",
                        DateOfBirth = DateTime.Now,
                        Address = new UsersAddress()
                        {
                            Country = "Polska",
                            City = "Zabrze",
                            PostalCode = "31-803",
                            Street = "Ulica Administratora",
                            HouseNumber = "1",
                            ApartmentNumber = "10",
                        },
                        Role = _dbContext.Roles.First(w => w.Name == "Admin")
                    };
                    admin.PasswordHash = _passwordHasher.HashPassword(admin, "admin");
                    _dbContext.Users.Add(admin);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
