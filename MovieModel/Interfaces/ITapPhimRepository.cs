using MovieModel.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel.Interfaces
{
    public interface ITapPhimRepository : IGenericRepository<TapPhim>
    {
        IEnumerable<TapPhim> GetTapPhim();
        IEnumerable<TapPhim> GetTapPhimById(string? tapphimId);
        void AddTapPhim(TapPhim tapphim);
        void DeleteTapPhim(string tapphimId);
        void UpdateTapPhim(TapPhim tapphim);
        IEnumerable<TapPhim> GetListTapPhim();
        IEnumerable<TapPhim> SearchTapPhimByName(string phimName);
    }
}
