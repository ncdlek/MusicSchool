using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class LectureRepository : BaseRepository<Lecture>
    {
        public List<Lecture> UnmatchedRoomLectures(Room room)
        {
            List<Lecture> lectures = table.ToList();
            List<Lecture> lectureList = table.ToList();

            foreach (var item in room.RoomLectures)
            {
                foreach (var lecture in lectures)
                {
                    if (lecture.Id == item.LectureId)
                    {
                        lectureList.Remove(lecture);
                    }
                }
            }

            return lectureList;
        }
    }
}
