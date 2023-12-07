using MediatR;

namespace OfficeFlow.Application.Dictionaries.Queries.GetDictionariesById
{
    public class GetDictionariesByIdQuery : IRequest<Domain.Entities.Dictionaries>
    {
        public int Id { get; set; }

        public GetDictionariesByIdQuery(int id)
        {
            Id = id;
        }
    }
}
