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
    public class PhimRepository : GenericRepository<Phim>, IPhimRepository
    {
        public PhimRepository(ApplicationDbContext context) : base(context)
        {
        }

        void IPhimRepository.AddPhim(Phim phim)
        {
            Add(phim);
        }

        void IPhimRepository.DeletePhim(string phimId)
        {
            Delete(e => e.Id == phimId);
        }

        IEnumerable<Phim> IPhimRepository.GetListPhim()
        {
            return GetList();
        }

        IEnumerable<Phim> IPhimRepository.GetPhim()
        {
            return GetAll();
        }

        IEnumerable<Phim> IPhimRepository.GetPhimById(string phimId)
        {
            return GetList(filter: x => x.Id == phimId);
        }

        IEnumerable<Phim> IPhimRepository.GetPhimByName(string phimName)
        {
            return GetList(filter: x => x.Name == phimName);
        }

        IEnumerable<Phim> IPhimRepository.SearchPhimByName(string phimName)
        {
            return GetAll().Where(e => UTF8_Convert.UTF8Convert(e.Name.ToLower()).Contains(UTF8_Convert.UTF8Convert(phimName.ToLower())));
        }

        void IPhimRepository.UpdatePhim(Phim phim)
        {
            Update(phim);
        }
    }
}
