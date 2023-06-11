using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IDienVienRepository : IGenericRepository<DienVien>
    {
        IEnumerable<DienVien> GetDienVien();
        IEnumerable<DienVien> GetDienVienById(string? dienvienId);

        IEnumerable<DienVien> GetDienVienByName(string dienvienName);
        void AddDienVien(DienVien dienvien);
        void DeleteDienVien(string dienvienId);
        void UpdateDienVien(DienVien dienvien);
        IEnumerable<DienVien> GetListDienVien();

        IEnumerable<DienVien> SearchDienVienByName(string dienvienName);
    }
}
