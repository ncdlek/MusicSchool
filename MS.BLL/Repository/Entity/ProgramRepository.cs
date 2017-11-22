﻿using MS.BLL.Helper;
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

            List<Room> roomList = db.Rooms
                .Where(x => x.isActive == true)
                .ToList();

            int[] hours = { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };

            List<PivotTable> pivotTable = new List<PivotTable>();

            foreach (var hour in hours) // her saat için bir satır oluşturuyoruz
            {
                PivotTable pivotRow = new PivotTable() { Hour = hour };

                foreach (var room in roomList) // her oda için tek tek bakıyoruz
                {
                    bool added = false;

                    foreach (var item in modelBase) // programdaki her dersi oda ve saat ile karşılaştırıyoruz
                    {
                        if (item.Hour == hour && room.Id == item.RoomId) // eğer uyuyorsa pivota ekliyoruz
                        {
                            pivotRow.Rooms.Add(item);
                            added = true;
                            break;
                        }
                    }
                    if (!added) // hiç biri uymadı ise pivota boş oda ekliyoruz
                    {
                        EmptyRoom emptyRoom = new EmptyRoom(room.Id, hour, day);
                        pivotRow.Rooms.Add(emptyRoom);
                        added = false;
                    }
                }

                pivotTable.Add(pivotRow);
            }

            return pivotTable;
        }
    }
}
