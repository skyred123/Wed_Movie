using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IPhanPhimDienVienRepository : IGenericRepository<CT_DienVien>
    {
        IEnumerable<string?> GetAllDienVienIdByPhanPhimId(string? phanphimId);

        IEnumerable<string?> GetAllPhanPhimIdByDienVienId(string? dienvienId);

        void AddPhanPhimDienVien(CT_DienVien cT_DienVien);

        void DeletePhanPhimDienVien(CT_DienVien cT_DienVien);
    }
}
