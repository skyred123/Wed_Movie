using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IPhanPhimTheLoaiRepository : IGenericRepository<CT_TheLoai>
    {
        IEnumerable<string?> GetAllTheLoaiIdByPhanPhimId(string? phanphimId);

        IEnumerable<string?> GetAllPhanPhimIdByTheLoaiId(string? theloaiId);

        void AddPhanPhimTheLoai(CT_TheLoai cT_TheLoai);

        void DeletePhanPhimTheLoai(CT_TheLoai cT_TheLoai);
    }
}
