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
    public class PhanPhimTheLoaiRepository : GenericRepository<CT_TheLoai>, IPhanPhimTheLoaiRepository
    {
        public PhanPhimTheLoaiRepository(ApplicationDbContext context) : base(context)
        {
        }

        void IPhanPhimTheLoaiRepository.AddPhanPhimTheLoai(CT_TheLoai cT_TheLoai)
        {
            Add(cT_TheLoai);
        }

        void IPhanPhimTheLoaiRepository.DeletePhanPhimTheLoai(CT_TheLoai cT_TheLoai)
        {
            Delete(cT_TheLoai);
        }

        IEnumerable<string?> IPhanPhimTheLoaiRepository.GetAllPhanPhimIdByTheLoaiId(string? theloaiId)
        {
            return GetMany(e => e.IdTheLoai == theloaiId).Select(x => x.IdTheLoai);
        }

        IEnumerable<string?> IPhanPhimTheLoaiRepository.GetAllTheLoaiIdByPhanPhimId(string? phanphimId)
        {
            return GetMany(e => e.IdPhanPhim == phanphimId).Select(x => x.IdTheLoai);
        }
    }
}
