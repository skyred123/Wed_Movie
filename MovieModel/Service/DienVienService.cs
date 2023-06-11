using Microsoft.EntityFrameworkCore;
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
    public class DienVienService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDienVienRepository _dienVienRepository;

        public DienVienService(ApplicationDbContext dbContext)
        {
            //dbContext.DienViens.Include(e => e.CT_DienViens).ThenInclude(e=>e.DienVien).Load();
            _dbContext = dbContext;
            _dienVienRepository = new DienVienRepository(_dbContext);
        }

        public IEnumerable<DienVien> GetAllDienVienId(string? id)
        {
            return _dienVienRepository.GetDienVienById(id);
        }
        public IEnumerable<DienVien> GetAllDienVienName(string name)
        {
            return _dienVienRepository.GetDienVienByName(name);
        }

        public IEnumerable<DienVien> GetAllDienViens()
        {
            return _dienVienRepository.GetDienVien();
        }

        public IEnumerable<DienVien> GetListDienViens()
        {
            return _dienVienRepository.GetListDienVien();
        }

        public void AddDienVien(DienVien dienvien)
        {
            _dienVienRepository.AddDienVien(dienvien);
        }

        public void UpdateDienVien(DienVien dienvien)
        {
            _dienVienRepository.Update(dienvien);
        }

        public void DeleteDienVien(string id)
        {
            _dienVienRepository.DeleteDienVien(id);
        }

        public IEnumerable<DienVien> SearchNameDienViens(string name)
        {
            return _dienVienRepository.SearchDienVienByName(name);
        }
    }
}
