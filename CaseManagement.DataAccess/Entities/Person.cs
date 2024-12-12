using System;
using System.Collections.Generic;

namespace CaseManagement.DataAccess.Entities;

public partial class Person
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long Contact { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
