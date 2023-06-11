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
    public class DienVienRepository : GenericRepository<DienVien>, IDienVienRepository
    {
        public DienVienRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddDienVien(DienVien dienvien)
        {
            Add(dienvien);
        }

        public void DeleteDienVien(string dienvienId)
        {
            Delete(e => e.Id == dienvienId);
        }

        public IEnumerable<DienVien> GetDienVien()
        {
            return GetAll();
        }

        public IEnumerable<DienVien> GetDienVienById(string dienvienId)
        {
            return GetList(filter: x => x.Id == dienvienId);
        }

        public IEnumerable<DienVien> GetDienVienByName(string dienvienName)
        {
            return GetList(filter: x => x.Name == dienvienName);
        }

        public IEnumerable<DienVien> GetListDienVien()
        {
            return GetList();
        }

        public void UpdateDienVien(DienVien dienvien)
        {
            Update(dienvien);
        }

        public IEnumerable<DienVien> SearchDienVienByName(string dienvienName) {
            return GetAll().Where(e=>UTF8_Convert.UTF8Convert(e.Name.ToLower()).Contains(UTF8_Convert.UTF8Convert(dienvienName.ToLower())));
        }
    }
}
