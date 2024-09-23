using System;
using System.Collections.Generic;

namespace shiyan1.DatabaseModels;

public partial class Student
{
    public string Sid { get; set; } = null!;

    public string? Sname { get; set; }

    public string? Sclass { get; set; }

    public string? Spassword { get; set; }

    public virtual ICollection<SelectedCourse> SelectedCourses { get; set; } = new List<SelectedCourse>();
}
