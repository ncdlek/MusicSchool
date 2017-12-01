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
        public List<Lecture> UnmatchedLectures(Room room)
        {
            List<Lecture> lectures = table
                .Where(x => x.isActive == true)
                .ToList();
            List<Lecture> lectureList = table
                .Where(x => x.isActive == true)
                .ToList();

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

        public List<Lecture> UnmatchedLectures(Teacher teacher)
        {
            List<Lecture> lectures = table
                .Where(x => x.isActive == true)
                .ToList();

            List<Lecture> lectureList = table
                .Where(x => x.isActive == true)
                .ToList();

            foreach (var item in teacher.TeacherLectures)
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
