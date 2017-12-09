using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class RoomRepository : BaseRepository<Room>
    {
        public int Update(Room newEntity)
        {
            if (newEntity.Id == 0)
                return 0;

            Room oldEntity = table.Find(newEntity.Id);

            if (oldEntity == null)
                return 0;

            newEntity.AddedDate = oldEntity.AddedDate;
            newEntity.isActive = oldEntity.isActive;
            db.Entry(oldEntity).CurrentValues.SetValues(newEntity);

            return Save();
        }
    }
}
