using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IPhanPhimHangRepository : IGenericRepository<CT_Hang>
    {
        IEnumerable<string?> GetAllHangIdByPhanPhimId(string? hangId);

        IEnumerable<string?> GetAllHangIdByDienVienId(string? phanphimId);

        void AddHangDienVien(CT_Hang cT_Hang);

        void DeleteHangDienVien(CT_Hang cT_Hang);
    }
}
