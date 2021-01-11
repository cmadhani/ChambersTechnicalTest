using DocumentManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Dal
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {

        }

        public DbSet<DocumentRecordEntity> Document { get; set; }
    }
}
