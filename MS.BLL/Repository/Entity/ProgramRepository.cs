using MS.BLL.Helper;
using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class ProgramRepository : BaseRepository<WeeklyProgram>
    {
        public List<PivotTable> GetDayProgram(int day, DateTime? date)
        {
            if (!date.HasValue)
                date = DateTime.Now;

            List<WeeklyProgram> modelBase = table
                .Where(b => b.StartDate <= date && (b.EndDate >= date || b.EndDate == null))
                .Where(b => b.Day == day)
                .ToList();

            int[] hours = { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };

            List<PivotTable> pivotTable = new List<PivotTable>();

            foreach (var hour in hours)
            {
                PivotTable pivotRow = new PivotTable() { Hour = hour };

                foreach (var item in modelBase)
                    if (item.Hour == hour)
                        switch (item.RoomId)
                        {
                            case 1:
                                pivotRow.Room1 = item.Id;
                                break;
                            case 2:
                                pivotRow.Room2 = item.Id;
                                break;
                            case 3:
                                pivotRow.Room3 = item.Id;
                                break;
                            case 4:
                                pivotRow.Room4 = item.Id;
                                break;
                            case 5:
                                pivotRow.Room5 = item.Id;
                                break;
                        }
                pivotTable.Add(pivotRow);
            }

            return pivotTable;
        }
    }
}
