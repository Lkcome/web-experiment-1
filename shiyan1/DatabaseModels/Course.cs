using System;
using System.Collections.Generic;

namespace shiyan1.DatabaseModels;

public partial class Course
{
    public string Cid { get; set; } = null!;
    public string? Cname { get; set; }
    public string? Cscore { get; set; }
    public string? Cteacher { get; set; }
    public string? Csem { get; set; }
    public string? Ctime { get; set; }
    public string? Cclassroom { get; set; }

    public virtual ICollection<SelectedCourse> SelectedCourses { get; set; } = new List<SelectedCourse>();
}
