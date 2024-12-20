using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class User
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public int RoleId { get; set; }

    public string PersonId { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;

    public virtual LookupConstant Role { get; set; } = null!;

    public virtual ICollection<ScheduleHearing> ScheduleHearings { get; set; } = new List<ScheduleHearing>();
}
