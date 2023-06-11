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
    public class TheLoaiService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITheLoaiRepository _theloaiRepository;

        public TheLoaiService(ApplicationDbContext dbContext)
        {
            //dbContext.TheLoais.Include(e => e.CT_TheLoais).ThenInclude(e=>e.TheLoai).Load();
            _dbContext = dbContext;
            _theloaiRepository = new TheLoaiRepository(_dbContext);
        }

        public IEnumerable<TheLoai> GetTheLoaiId(string id)
        {
            return _theloaiRepository.GetTheLoaiById(id);
        }
        public IEnumerable<TheLoai> GetAllTheLoais()
        {
            return _theloaiRepository.GetTheLoai();
        }

        public IEnumerable<TheLoai> GetListTheLoais()
        {
            return _theloaiRepository.GetListTheLoai();
        }

        public void AddTheLoai(TheLoai theloai)
        {
            _theloaiRepository.AddTheLoai(theloai);
        }

        public void UpdateTheLoai(TheLoai theloai)
        {
            _theloaiRepository.Update(theloai);
        }

        public void DeleteTheLoai(string id)
        {
            _theloaiRepository.DeleteTheLoai(id);
        }
        public IEnumerable<TheLoai> SearchNameTheLoais(string name)
        {
            return _theloaiRepository.SearchTheLoaiByName(name);
        }
    }
}
