//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PostponedLesson
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> OriginalLessonId { get; set; }
        public int RescheduledLessonId { get; set; }
        public string Name { get; set; }
    
        public virtual WeeklyProgram WeeklyProgram { get; set; }
        public virtual WeeklyProgram WeeklyProgram1 { get; set; }
    }
}
