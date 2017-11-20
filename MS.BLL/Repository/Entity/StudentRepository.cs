using MS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.BLL.Repository.Entity
{
    public class StudentRepository : BaseRepository<Student>
    {
        public List<Student> SelectOnePage(int? page, out int pageCount)
        {
            int count = 10;
            int pageNumber = page ?? 1;

            pageCount = (int)Math.Ceiling((double)table
                .Count() / count);

            return table
                .OrderBy(x => x.Name)
                .Skip(count * (pageNumber - 1))
                .Take(count)
                .ToList();
        }

        public List<Student> SelectOnePage(int? page, string name, out int pageCount)
        {
            int count = 10;
            int pageNumber = page ?? 0;

            pageCount = (int)Math.Ceiling((double)table
                .Where(c => c.Name.Contains(name))
                .Count() / count);

            return table
                .Where(c => c.Name.Contains(name))
                .OrderBy(x => x.Name)
                .Skip(count * pageNumber)
                .Take(count)
                .ToList();
        }
    }
}
