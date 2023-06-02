using Wed_Movie.Functions;
using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public static class PhimBLL
    {
        private static readonly AppDbContext _dbContext = AppDbContext.Create();

        public static List<Phim> List(string search)
        {
            if (search == null)
            {
                return _dbContext.Phims.ToList();
            }
            else
            {
                List<Phim> ListPhhim = new List<Phim>();
                foreach (var item in _dbContext.Phims.ToList())
                {
                    if (UTF8_Convert.utf8Convert(item.Name).ToLower().Contains(search.ToLower()))
                    {
                        ListPhhim.Add(item);
                    }
                }
                return ListPhhim;
            }
        }
        public static Phim? Item(string? id)
        {
            return _dbContext.Phims.FirstOrDefault(e => e.Id == id);
        }
        public static bool Add(Phim phim)
        {
            try
            {
                _dbContext.Phims.Add(phim);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Update(Phim phim)
        {
            try
            {
                var temp = Item(phim.Id);
                if (temp == null) { return false; }
                temp.Name = phim.Name;
                _dbContext.Phims.Update(temp);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Delete(string id)
        {
            try
            {
                var phim = Item(id);
                if (phim != null)
                {
                    _dbContext.Phims.Remove(phim);
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
