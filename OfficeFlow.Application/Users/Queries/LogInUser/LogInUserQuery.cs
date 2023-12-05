using MediatR;
using OfficeFlow.Application.Users.Models;

namespace OfficeFlow.Application.Users.Queries.LogInUser
{
    public class LogInUserQuery : IRequest<DetailsModel>
    {
        public LoginModel Login { get; set; }
        
        public LogInUserQuery(LoginModel login)
        {
            Login = login;
        }
    }
}
