using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.UI.Models
{
    public class UserShiftViewModel
    {
        public IList<Shift> AllShifts { get; set; }
        public IList<Shift> AssignedShifts { get; set; }
        public User User { get; set; }
    }

    public partial class Shift
    {
        public bool Include { get; set; }
    }
}