using System;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Entities
{
    public class DocumentEntity
    {
        [Key]
        public Guid DocumentId { get; set; }
        public byte[] File { get; set; }
    }
}
