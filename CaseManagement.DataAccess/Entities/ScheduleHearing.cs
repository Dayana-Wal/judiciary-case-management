using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class ScheduleHearing
{
    public string Id { get; set; } = null!;

    public string CaseId { get; set; } = null!;

    public string JudgeId { get; set; } = null!;

    public DateTime ScheduledAt { get; set; }

    public string Judgement { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual User Judge { get; set; } = null!;
}
