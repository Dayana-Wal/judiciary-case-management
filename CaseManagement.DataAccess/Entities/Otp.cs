using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class Otp
{
    public string Id { get; set; } = null!;

    public string OtpHash { get; set; } = null!;

    public bool IsVerified { get; set; }

    public string RequestedBy { get; set; } = null!;

    public DateTime GeneratedAt { get; set; }

    public int UsedForId { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public virtual Person RequestedByNavigation { get; set; } = null!;

    public virtual LookupConstant UsedFor { get; set; } = null!;
}
