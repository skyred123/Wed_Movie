using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IHangRepository : IGenericRepository<Hang>
    {
        IEnumerable<Hang> GetHang();
        IEnumerable<Hang> GetHangById(string hangId);
        void AddHang(Hang hang);
        void DeleteHang(string hangId);
        void UpdateHang(Hang hang);

        IEnumerable<Hang> GetListHang();

        IEnumerable<Hang> SearchHangByName(string hangName);
    }
}
