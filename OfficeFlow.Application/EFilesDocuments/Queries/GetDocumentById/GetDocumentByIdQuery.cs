using MediatR;
using OfficeFlow.Application.EFilesDocuments.Models;

namespace OfficeFlow.Application.EFilesDocuments.Queries.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<DocumentModel>
    {
        public int Id { get; set; }

        public GetDocumentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
