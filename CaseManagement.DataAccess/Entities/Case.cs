using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class Case
{
    public string Id { get; set; } = null!;

    public int CaseTypeId { get; set; }

    public string Description { get; set; } = null!;

    public string CaseNumber { get; set; } = null!;

    public string AccusedId { get; set; } = null!;

    public string VictimId { get; set; } = null!;

    public string? AdvocateId { get; set; }

    public int CaseStatusId { get; set; }

    public virtual Person Accused { get; set; } = null!;

    public virtual Person? Advocate { get; set; }

    public virtual ICollection<CaseFile> CaseFiles { get; set; } = new List<CaseFile>();

    public virtual LookupConstant CaseStatus { get; set; } = null!;

    public virtual LookupConstant CaseType { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<ScheduleHearing> ScheduleHearings { get; set; } = new List<ScheduleHearing>();

    public virtual Person Victim { get; set; } = null!;
}
