using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Entities
{
    public class DocumentRecordEntity
    {
        [Key]
        public Guid DocumentRecordId { get; set; }
        public string FileName { get; set; }

        [ForeignKey("DocumentEntity")]
        public Guid DocumentId { get; set; }
        public long FileSize { get; set; }
    }
}
