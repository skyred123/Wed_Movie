using MovieModel.Config;
using MovieModel.Interfaces;
using MovieModel.Repository;
using Microsoft.EntityFrameworkCore;
using Wed_Movie.Entities;

namespace MovieModel.Service
{
    public class PhanPhimService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPhanPhimRepository _phanphimRepository;
        private readonly IDienVienRepository _dienVienRepository;
        private readonly IHangRepository _hangRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;
        private readonly IPhimRepository _phimRepository;

        private readonly IPhanPhimDienVienRepository _phanPhimDienVienRepository;
        private readonly IPhanPhimHangRepository _phanPhimHangRepository;
        private readonly IPhanPhimTheLoaiRepository _phimTheLoaiRepository;
        public PhanPhimService(ApplicationDbContext dbContext)
        {
            dbContext.PhanPhims
                .Include(e => e.Phim)
                .Include(e => e.CT_DienVien)
                .ThenInclude(e => e.DienVien)
                .Include(e => e.CT_TheLoais)
                .ThenInclude(e => e.TheLoai)
                .Include(e => e.CT_Hangs)
                .ThenInclude(e => e.Hang)
                .Include(e => e.TapPhim)
                .Load();
            _dbContext = dbContext;

            _phanphimRepository = new PhanPhimRepository(_dbContext);
            _dienVienRepository = new DienVienRepository(_dbContext);
            _hangRepository = new HangRepository(_dbContext);
            _theLoaiRepository = new TheLoaiRepository(_dbContext);
            _phimRepository = new PhimRepository(_dbContext);
            _phanPhimDienVienRepository = new PhanPhimDienVienRepository(_dbContext);
        }

        public IEnumerable<PhanPhim> GetAllPhanPhimId(string? id)
        {
            return _phanphimRepository.GetPhanPhimById(id);
        }

        public IEnumerable<PhanPhim> GetAllPhanPhims()
        {
            return _phanphimRepository.GetPhanPhim();
        }

        public IEnumerable<PhanPhim> GetListPhanPhims()
        {
            return _phanphimRepository.GetListPhanPhim();
        }

        public void AddPhanPhim(PhanPhim PhanPhim)
        {
            _phanphimRepository.AddPhanPhim(PhanPhim);
        }

        public void UpdatePhanPhim(PhanPhim PhanPhim)
        {
            _phanphimRepository.Update(PhanPhim);
        }

        public void DeletePhanPhim(string PhanPhimId)
        {
            _phanphimRepository.DeletePhanPhim(PhanPhimId);
        }


        public IEnumerable<DienVien?> GetAllDienViens()
        {
            return _dienVienRepository.GetListDienVien();
        }
        public IEnumerable<DienVien?> GetDienVienInPhanPhim(string? id)
        {
            return _phanPhimDienVienRepository.GetAllDienVienIdByPhanPhimId(id).Select(x=> _dienVienRepository.GetDienVienById(x).FirstOrDefault());
        }

        public IEnumerable<Hang?> GetListHang()
        {
            return _hangRepository.GetListHang();
        }
        public IEnumerable<TheLoai?> GetListTheLoai()
        {
            return _theLoaiRepository.GetListTheLoai();
        }
        public IEnumerable<Phim?> GetListPhim()
        {
            return _phimRepository.GetListPhim();
        }
        public IEnumerable<CT_DienVien?> AddDienVienPhanPhim(List<string> dienvienId, string phanphimId)
        {
            var dienvienPhanPhimList = new List<CT_DienVien?>();
            foreach (var id in dienvienId)
            {
                var dienvienPhanPhim = new CT_DienVien
                {
                    IdDienVien = id,
                    IdPhanPhim = phanphimId
                };
                dienvienPhanPhimList.Add(dienvienPhanPhim);
            }
            return dienvienPhanPhimList;
        }

        public IEnumerable<CT_Hang?> AddHangPhanPhim(List<string> hangId, string phanphimId)
        {
            var hangPhanPhimList = new List<CT_Hang?>();
            foreach (var id in hangId)
            {
                var hangPhanPhim = new CT_Hang
                {
                    IdHang = id,
                    IdPhanPhim = phanphimId
                };
                hangPhanPhimList.Add(hangPhanPhim);
            }
            return hangPhanPhimList;
        }

        public IEnumerable<CT_TheLoai?> AddTheLoaiPhanPhim(List<string> theloaiId, string phanphimId)
        {
            var theloaiPhanPhimList = new List<CT_TheLoai?>();
            foreach (var id in theloaiId)
            {
                var theloaiPhanPhim = new CT_TheLoai
                {
                    IdTheLoai = id,
                    IdPhanPhim = phanphimId
                };
                theloaiPhanPhimList.Add(theloaiPhanPhim);
            }
            return theloaiPhanPhimList;
        }
    }
        
}
