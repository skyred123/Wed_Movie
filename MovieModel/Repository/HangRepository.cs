using MovieModel.Config;
using MovieModel.Generic;
using MovieModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Repository
{
    public class HangRepository : GenericRepository<Hang>, IHangRepository
    {
        public HangRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddHang(Hang hang)
        {
            Add(hang);
        }

        public void DeleteHang(string hangId)
        {
            Delete(e => e.Id == hangId);
        }

        public IEnumerable<Hang> GetHang()
        {
            return GetAll();
        }

        public IEnumerable<Hang> GetHangById(string hangId)
        {
            return GetList(filter: x => x.Id == hangId);
        }

        public IEnumerable<Hang> GetListHang()
        {
            return GetList();
        }

        public void UpdateHang(Hang hang)
        {
            Update(hang);
        }

        public IEnumerable<Hang> SearchHangByName(string hangName)
        {
            return GetAll().Where(e => UTF8_Convert.UTF8Convert(e.Name.ToLower()).Contains(UTF8_Convert.UTF8Convert(hangName.ToLower())));
        }
    }
}
