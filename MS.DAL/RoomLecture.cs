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
    
    public partial class RoomLecture
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> LectureId { get; set; }
    
        public virtual Room Room { get; set; }
        public virtual Lecture Lecture { get; set; }
    }
}
