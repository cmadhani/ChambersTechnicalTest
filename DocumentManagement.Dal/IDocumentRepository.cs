using DocumentManagement.Entities;
using System;
using System.Threading.Tasks;

namespace DocumentManagement.Dal
{
    public interface IDocumentRepository : IDisposable
    {
        Task<Guid> CreateDocumentRecordAsync(DocumentRecordEntity document);
        Task<Guid> CreateDocumentAsync(DocumentEntity document);
    }
}
