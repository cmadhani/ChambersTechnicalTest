using DocumentManagement.Dal;
using DocumentManagement.Entities;
using DocumentManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _dbRepository;

        public DocumentService(IDocumentRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task<Document> AddDocumentRecord(string fileName, Guid location, long fileSize)
        {
            var documentRecordEntity = new DocumentRecordEntity { DocumentId = location, FileName = fileName, FileSize = fileSize };
            var recordGuid = await _dbRepository.CreateDocumentRecordAsync(documentRecordEntity);
            return new Document { FileName = fileName, FileSize = fileSize, Location = location, Id = recordGuid };
        }


        public async Task<Guid> AddDocument(IFormFile file)
        {
            DocumentEntity documentEntity = null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                documentEntity = new DocumentEntity { File = memoryStream.ToArray() };
            }

            return await _dbRepository.CreateDocumentAsync(documentEntity);

        }
    }
}
