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
    public class TheLoaiRepository : GenericRepository<TheLoai>, ITheLoaiRepository
    {
        public TheLoaiRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        void ITheLoaiRepository.AddTheLoai(TheLoai theloai)
        {
            Add(theloai);
        }

        void ITheLoaiRepository.DeleteTheLoai(string theloaiid)
        {
            Delete(e => e.Id == theloaiid);
        }

        IEnumerable<TheLoai> ITheLoaiRepository.GetListTheLoai()
        {
            return GetList();
        }

        IEnumerable<TheLoai> ITheLoaiRepository.GetTheLoai()
        {
            return GetAll();
        }

        IEnumerable<TheLoai> ITheLoaiRepository.GetTheLoaiById(string theloaiId)
        {
            return GetList(filter: x => x.Id == theloaiId);
        }

        IEnumerable<TheLoai> ITheLoaiRepository.GetTheLoaiByName(string theloaiName)
        {
            return GetList(filter: x => x.Name == theloaiName);
        }

        IEnumerable<TheLoai> ITheLoaiRepository.SearchTheLoaiByName(string theloaiName)
        {
            return GetAll().Where(e => UTF8_Convert.UTF8Convert(e.Name.ToLower()).Contains(UTF8_Convert.UTF8Convert(theloaiName.ToLower())));
        }

        void ITheLoaiRepository.UpdateTheLoai(TheLoai theloai)
        {
            Update(theloai);
        }
    }
}
