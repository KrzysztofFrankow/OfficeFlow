using System.ComponentModel;

namespace OfficeFlow.Application.Enums
{
    public enum AbsenceStatus
    {
        [Description("W trakcie")]
        InProgress = 1,
        [Description("Zaakceptowane")]
        Accepted = 2,
        [Description("Odrzucone")]
        Rejected = 3
    }
}
