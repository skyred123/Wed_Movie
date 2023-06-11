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
    public class TapPhimRepository : GenericRepository<TapPhim>, ITapPhimRepository
    {
        public TapPhimRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddTapPhim(TapPhim tapphim)
        {
            Add(tapphim);
        }

        public void DeleteTapPhim(string tapphimId)
        {
            Delete(e => e.Id == tapphimId);
        }

        public IEnumerable<TapPhim> GetListTapPhim()
        {
            return GetList(orderBy: e => e.OrderBy(e => e.Count));
        }

        public IEnumerable<TapPhim> GetTapPhim()
        {
            return GetAll();
        }

        public IEnumerable<TapPhim> GetTapPhimById(string? tapphimId)
        {
            return GetList(filter: x => x.Id == tapphimId);
        }

        public void UpdateTapPhim(TapPhim tapphim)
        {
            Update(tapphim);
        }

        public IEnumerable<TapPhim> SearchTapPhimByName(string phimName)
        {
            return GetAll().Where(e => UTF8_Convert.UTF8Convert(e.Name.ToLower()).Contains(UTF8_Convert.UTF8Convert(phimName.ToLower())));
        }
    }
}
