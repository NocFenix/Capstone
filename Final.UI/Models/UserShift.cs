//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final.UI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserShift
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public bool Active { get; set; }
    
        public virtual Shift Shift { get; set; }
        public virtual User User { get; set; }
    }
}
