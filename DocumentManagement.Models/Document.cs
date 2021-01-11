using System;

namespace DocumentManagement.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Location { get; set; }
        public long FileSize { get; set; }
    }
}
