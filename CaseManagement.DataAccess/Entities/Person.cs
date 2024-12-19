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

    public virtual ICollection<Case> CaseAccuseds { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseAdvocates { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseVictims { get; set; } = new List<Case>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
