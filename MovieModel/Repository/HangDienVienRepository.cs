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
    public class HangDienVienRepository : GenericRepository<CT_Hang>, IPhanPhimHangRepository
    {
        public HangDienVienRepository(ApplicationDbContext context) : base(context)
        {
        }

        void IPhanPhimHangRepository.AddHangDienVien(CT_Hang cT_Hang)
        {
            Add(cT_Hang);
        }

        void IPhanPhimHangRepository.DeleteHangDienVien(CT_Hang cT_Hang)
        {
            Delete(cT_Hang);
        }

        IEnumerable<string?> IPhanPhimHangRepository.GetAllHangIdByDienVienId(string? phanphimId)
        {
            return GetMany(e => e.IdPhanPhim == phanphimId).Select(x => x.IdHang);
        }

        IEnumerable<string?> IPhanPhimHangRepository.GetAllHangIdByPhanPhimId(string? hangId)
        {
            return GetMany(e => e.IdHang == hangId).Select(x => x.IdHang);
        }
    }
}
