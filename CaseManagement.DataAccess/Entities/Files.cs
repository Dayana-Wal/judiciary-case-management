using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class Files
{
    public string Id { get; set; } = null!;

    public int FileTypeId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string UploadedBy { get; set; } = null!;

    public virtual ICollection<CaseFile> CaseFiles { get; set; } = new List<CaseFile>();

    public virtual LookupConstant FileType { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual User UploadedByNavigation { get; set; } = null!;
}
