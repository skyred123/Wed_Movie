using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface ITheLoaiRepository : IGenericRepository<TheLoai>
    {
        IEnumerable<TheLoai> GetTheLoai();
        IEnumerable<TheLoai> GetTheLoaiById(string theloaiId);

        IEnumerable<TheLoai> GetTheLoaiByName(string theloaiName);
        void AddTheLoai(TheLoai theloai);
        void DeleteTheLoai(string theloaiid);
        void UpdateTheLoai(TheLoai theloai);
        IEnumerable<TheLoai> GetListTheLoai();

        IEnumerable<TheLoai> SearchTheLoaiByName(string theloaiName);
    }
}
