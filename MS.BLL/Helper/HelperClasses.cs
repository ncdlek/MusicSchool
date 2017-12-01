using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Helper
{
    public class PivotTable
    {
        public PivotTable()
        {
            this.Rooms = new List<WeeklyProgram>();
        }

        public int Hour { get; set; }
        public List<WeeklyProgram> Rooms { get; set; }
    }

    public class EmptyRoom : WeeklyProgram
    {
        public EmptyRoom(int id, int hour, int day)
        {
            Id = id;
            Hour = hour;
            Day = day;
        }
    }
}
