using MovieModel.Config;
using MovieModel.Generic;
using MovieModel.Interfaces;
using Wed_Movie.Entities;

namespace MovieModel.Repository
{
    public class PhanPhimDienVienRepository : GenericRepository<CT_DienVien>, IPhanPhimDienVienRepository
    {
        public PhanPhimDienVienRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<string?> GetAllDienVienIdByPhanPhimId(string? phanphimId)
        {
            return GetMany(e => e.IdPhanPhim == phanphimId).Select(x => x.IdPhanPhim);
        }

        public IEnumerable<string?> GetAllPhanPhimIdByDienVienId(string? dienvienId)
        {
            return GetMany(e => e.IdDienVien == dienvienId).Select(x => x.IdDienVien);
        }

        public void AddPhanPhimDienVien(CT_DienVien cT_DienVien)
        {
            Add(cT_DienVien);
        }

        public void DeletePhanPhimDienVien(CT_DienVien cT_DienVien)
        {
            Delete(cT_DienVien);
        }
    }
}
