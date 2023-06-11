using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface IPhimRepository : IGenericRepository<Phim>
    {
        IEnumerable<Phim> GetPhim();
        IEnumerable<Phim> GetPhimById(string phimId);

        IEnumerable<Phim> GetPhimByName(string phimName);
        void AddPhim(Phim phim);
        void DeletePhim(string phimId);
        void UpdatePhim(Phim phim);
        IEnumerable<Phim> GetListPhim();

        IEnumerable<Phim> SearchPhimByName(string phimName);
    }
}
