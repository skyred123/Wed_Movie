using Wed_Movie.Functions;
using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public class TapPhimBLL
    {
        private static readonly AppDbContext _dbContext = AppDbContext.Create();

        public static List<TapPhim>? ListTapPhim(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            else
            {
                return _dbContext.PhanPhims.ToList().FirstOrDefault(e=>e.Id == id).TapPhim;
            }
        }


        public static List<TapPhim> List(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dbContext.TapPhims.ToList();
            }
            else
            {
                List<TapPhim> ListTapPhim = new List<TapPhim>();
                foreach (var item in _dbContext.TapPhims.ToList())
                {
                    if (UTF8_Convert.utf8Convert(item.Name).ToLower().Contains(search.ToLower()))
                    {
                        ListTapPhim.Add(item);
                    }
                }
                return ListTapPhim;
            }
        }
        public static TapPhim? Item(string? id)
        {
            return _dbContext.TapPhims.FirstOrDefault(e => e.Id == id);
        }
        public static bool Add(TapPhim theLoai)
        {
            try
            {
                _dbContext.TapPhims.Add(theLoai);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Update(TapPhim TapPhim)
        {
            try
            {
                var temp = Item(TapPhim.Id);
                if (temp == null) { return false; }
                temp.Name = TapPhim.Name;
                _dbContext.TapPhims.Update(temp);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Delete(TapPhim tapPhim)
        {
            try
            {
                if (tapPhim != null)
                {
                    _dbContext.TapPhims.Remove(tapPhim);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
