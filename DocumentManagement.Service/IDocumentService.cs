using DocumentManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IDocumentService
    {
        Task<Document> AddDocumentRecord(string name, Guid location, long length);
        Task<Guid> AddDocument(IFormFile file);
    }
}
