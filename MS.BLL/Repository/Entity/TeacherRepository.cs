using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class TeacherRepository : BaseRepository<Teacher>
    {
        public int Update(Teacher newEntity)
        {
            if (newEntity.Id == 0)
                return 0;

            Teacher oldEntity = table.Find(newEntity.Id);

            if (oldEntity == null)
                return 0;

            newEntity.AddedDate = oldEntity.AddedDate;
            newEntity.isActive = oldEntity.isActive;
            db.Entry(oldEntity).CurrentValues.SetValues(newEntity);

            return Save();
        }
    }
}
