using MediatR;

namespace OfficeFlow.Application.Absences.Queries.CheckLimitForValidator
{
    public class CheckLimitForValidatorQuery : IRequest<bool>
    {
        public int UserId { get; set; }
        public int Type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool ForEdit { get; set; }

        public CheckLimitForValidatorQuery(int userId, int type, DateTime from, DateTime to, bool forEdit)
        {
            UserId = userId;
            Type = type;
            From = from;
            To = to;
            ForEdit = forEdit;
        }
    }
}
