using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public int RoleId { get; set; }

    public Guid PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual LookupConstant Role { get; set; } = null!;
}
