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
    public class HangService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHangRepository _hangRepository;

        public HangService(ApplicationDbContext dbContext)
        {
            //dbContext.Hangs.Include(e => e.CT_Hangs).ThenInclude(e=>e.Hang).Load();
            _dbContext = dbContext;
            _hangRepository = new HangRepository(_dbContext);
        }

        public IEnumerable<Hang> GetHangId(string id)
        {
            return _hangRepository.GetHangById(id);
        }
        public IEnumerable<Hang> GetAllHangs()
        {
            return _hangRepository.GetHang();
        }

        public IEnumerable<Hang> GetListHangs()
        {
            return _hangRepository.GetListHang();
        }

        public void AddHang(Hang hang)
        {
            _hangRepository.AddHang(hang);
        }

        public void UpdateHang(Hang hang)
        {
            _hangRepository.Update(hang);
        }

        public void DeleteHang(string id)
        {
            _hangRepository.DeleteHang(id);
        }
        public IEnumerable<Hang> SearchNameHangs(string name)
        {
            return _hangRepository.SearchHangByName(name);
        }
    }
}
