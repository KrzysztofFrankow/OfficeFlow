using MediatR;
using OfficeFlow.Application.EFilesDocuments.Models;

namespace OfficeFlow.Application.EFilesDocuments.Queries.GetEFileDocumentById
{
    public class GetEFileDocumentByIdQuery : IRequest<DetailsModel>
    {
        public int Id { get; set; }

        public GetEFileDocumentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
