using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class Request
{
    public string Id { get; set; } = null!;

    public int RequestStatusId { get; set; }

    public int RequestTypeId { get; set; }

    public string Description { get; set; } = null!;

    public string FileId { get; set; } = null!;

    public string RaisedBy { get; set; } = null!;

    public string CaseId { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual File File { get; set; } = null!;

    public virtual Person RaisedByNavigation { get; set; } = null!;

    public virtual LookupConstant RequestStatus { get; set; } = null!;

    public virtual LookupConstant RequestType { get; set; } = null!;
}
