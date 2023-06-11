using MovieModel.Config;
using MovieModel.Generic;
using MovieModel.Interfaces;
using Wed_Movie.Entities;

namespace MovieModel.Repository
{
    public class PhanPhimRepository : GenericRepository<PhanPhim>, IPhanPhimRepository
    {
        public PhanPhimRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        // Triển khai các phương thức đặcifc cho PhanPhimRepository
        public IEnumerable<PhanPhim> GetPhanPhim()
        {
            return GetAll();
        }

        public IEnumerable<PhanPhim> GetPhanPhimById(string? phanphimId)
        {
            return GetList(filter: x => x.Id == phanphimId);
        }

        public void AddPhanPhim(PhanPhim phanPhim)
        {
            Add(phanPhim);
        }

        public void DeletePhanPhim(string phanPhimId)
        {
            Delete(e => e.Id == phanPhimId);
        }

        public void UpdatePhanPhim(PhanPhim phanPhim)
        {
            Update(phanPhim);
        }

        public IEnumerable<PhanPhim> GetListPhanPhim()
        {
            return GetList(orderBy: e=>e.OrderBy(e=>e.TimeUpdate));
        }


    }
}
