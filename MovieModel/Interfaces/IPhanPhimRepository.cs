using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IPhanPhimRepository : IGenericRepository<PhanPhim>
    {
        IEnumerable<PhanPhim> GetPhanPhim();
        IEnumerable<PhanPhim> GetPhanPhimById(string? phanphimId);
        void AddPhanPhim(PhanPhim phanphim);
        void DeletePhanPhim(string phanphimId);
        void UpdatePhanPhim(PhanPhim phanphim);
        IEnumerable<PhanPhim> GetListPhanPhim();
    }
}
