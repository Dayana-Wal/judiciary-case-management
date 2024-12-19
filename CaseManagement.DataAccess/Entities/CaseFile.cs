using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class CaseFile
{
    public string Id { get; set; } = null!;

    public string CaseId { get; set; } = null!;

    public string FileId { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual File File { get; set; } = null!;
}
