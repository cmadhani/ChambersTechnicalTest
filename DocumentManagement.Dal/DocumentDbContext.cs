using DocumentManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Dal
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {

        }

        public DbSet<DocumentRecordEntity> DocumentRecord { get; set; }
        public DbSet<DocumentEntity> Document { get; set; }

    }
}
