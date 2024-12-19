using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class LookupConstant
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Text { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
