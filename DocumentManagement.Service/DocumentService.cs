using DocumentManagement.Models;
using System;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IDocumentService
    {
        Task<Document> AddPdf(Document document);
    }
    public class DocumentService : IDocumentService
    {
        public async Task<Document> AddPdf(Document document)
        {
            return document;
        }
    }
}
