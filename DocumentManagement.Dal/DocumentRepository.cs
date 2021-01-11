using DocumentManagement.Entities;
using System;
using System.Threading.Tasks;

namespace DocumentManagement.Dal
{
    public class DocumentRepository : IDocumentRepository
    {
        private bool _disposedValue;
        private DocumentDbContext _context;

        public DocumentRepository(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateDocumentAsync(DocumentRecordEntity document)
        {
            _context.Document.Add(document);
            await _context.SaveChangesAsync();
            return document.DocumentRecordId;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
