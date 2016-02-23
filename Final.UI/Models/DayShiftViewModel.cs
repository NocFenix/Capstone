using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.UI.Models
{
    public class DayShiftViewModel
    {
        public IList<Shift> Shifts { get; set; }
        public IList<Shift> Users { get; set; }
        public Day Day { get; set; }
    }
}