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
    public class TapPhimService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TapPhimRepository _tapphimRepository;
        private readonly PhanPhimRepository _phanPhimRepository;
        public TapPhimService(ApplicationDbContext dbContext)
        {
            dbContext.PhanPhims
                .Include(e => e.Phim)
                .Include(e => e.CT_DienVien)
                    .ThenInclude(e => e.DienVien)
                .Include(e => e.CT_TheLoais)
                    .ThenInclude(e => e.TheLoai)
                .Include(e => e.CT_Hangs)
                    .ThenInclude(e => e.Hang)
                .Include(e => e.TapPhim).Load();
            dbContext.TapPhims
                .Include(e => e.PhanPhim).Load();
            _dbContext = dbContext;
            _tapphimRepository = new TapPhimRepository(_dbContext);
            _phanPhimRepository = new PhanPhimRepository(_dbContext);
        }
        public IEnumerable<TapPhim> GetAllTapPhimId(string? id)
        {
            return _tapphimRepository.GetTapPhimById(id);
        }

        public IEnumerable<TapPhim> GetAllTapPhims()
        {
            return _tapphimRepository.GetTapPhim();
        }

        public IEnumerable<TapPhim> GetListTapPhims()
        {
            return _tapphimRepository.GetListTapPhim();
        }

        public void AddTapPhim(TapPhim tapphim)
        {
            _tapphimRepository.AddTapPhim(tapphim);
        }

        public void UpdateTapPhim(TapPhim tapphim)
        {
            _tapphimRepository.Update(tapphim);
        }

        public void DeleteTapPhim(string tapphimId)
        {
            _tapphimRepository.DeleteTapPhim(tapphimId);
        }

        public IEnumerable<TapPhim> GetTapPhimByPhanPhimId(string id)
        {
            return _phanPhimRepository.GetPhanPhimById(id).FirstOrDefault().TapPhim;
        }

        public IEnumerable<TapPhim> SearchNameTapPhims(string name)
        {
            return _tapphimRepository.SearchTapPhimByName(name);
        }

    }
}
