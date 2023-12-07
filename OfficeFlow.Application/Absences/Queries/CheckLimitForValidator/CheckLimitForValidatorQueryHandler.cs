using AutoMapper;
using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Queries.CheckLimitForValidator
{
    public class CheckLimitForValidatorQueryHandler : IRequestHandler<CheckLimitForValidatorQuery, bool>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public CheckLimitForValidatorQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<bool> Handle(CheckLimitForValidatorQuery request, CancellationToken cancellationToken)
        {
            var currentYear = DateTime.UtcNow.Year;
            var absences = await _officeFlowRepository.GetAbsencesByUserIdAndType(request.UserId, request.Type);
            var limit = await _officeFlowRepository.GetLimitByUserIdAndType(request.UserId, request.Type);

            var totalUsedDays = absences
                .Where(a => a.From.Year == currentYear || a.To.Year == currentYear)
                .Sum(a => CountDays(a.From, a.To, currentYear));

            var newAbsenceDays = CountDays(request.From, request.To, currentYear);

            return totalUsedDays + newAbsenceDays <= limit.DaysLimit;
        }

        private int CountDays(DateTime from, DateTime to, int year)
        {
            var startDate = from.Year == year ? from : new DateTime(year, 1, 1);
            var endDate = to.Year == year ? to : new DateTime(year, 12, 31);

            return (endDate - startDate).Days + 1;
        }
    }
}
