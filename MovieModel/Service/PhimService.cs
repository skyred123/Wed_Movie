using MovieModel.Config;
using MovieModel.Interfaces;
using MovieModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Service
{
    public class PhimService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPhimRepository _phimRepository;

        public PhimService(ApplicationDbContext dbContext)
        {
            //dbContext.Hangs.Include(e => e.CT_Hangs).ThenInclude(e=>e.Hang).Load();
            _dbContext = dbContext;
            _phimRepository = new PhimRepository(_dbContext);
        }

        public IEnumerable<Phim> GetPhimId(string id)
        {
            return _phimRepository.GetPhimById(id);
        }
        public IEnumerable<Phim> GetAllPhims()
        {
            return _phimRepository.GetPhim();
        }

        public IEnumerable<Phim> GetListPhims()
        {
            return _phimRepository.GetListPhim();
        }

        public void AddPhim(Phim Phim)
        {
            _phimRepository.AddPhim(Phim);
        }

        public void UpdatePhim(Phim Phim)
        {
            _phimRepository.Update(Phim);
        }

        public void DeletePhim(string id)
        {
            _phimRepository.DeletePhim(id);
        }
        public IEnumerable<Phim> SearchNamePhims(string name)
        {
            return _phimRepository.SearchPhimByName(name);
        }
    }
}
