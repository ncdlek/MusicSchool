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
    
    public partial class ReceivedPayment
    {
        public int Id { get; set; }
        public System.DateTime AddedDate { get; set; }
        public int LessonId { get; set; }
        public decimal Payment { get; set; }
        public string UserId { get; set; }
        public bool isActive { get; set; }
        public string Name { get; set; }
    
        public virtual WeeklyProgram WeeklyProgram { get; set; }
        public virtual User User { get; set; }
    }
}
