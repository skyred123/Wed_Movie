using System.Text.RegularExpressions;
using System.Text;
using Wed_Movie.Models;
using Wed_Movie.Functions;

namespace Wed_Movie.Data.BLL
{
    public static class TheLoaiBLL
    {
        private static readonly AppDbContext _dbContext = AppDbContext.Create();

        public static List<TheLoai> List(string search)
        {
            if(search == null)
            {
                return _dbContext.TheLoais.ToList();
            }
            else
            {
                List<TheLoai> ListTheLoai = new List<TheLoai>();
                foreach (var item in _dbContext.TheLoais.ToList())
                {
                    if(UTF8_Convert.utf8Convert(item.Name).ToLower().Contains(search.ToLower()))
                    {
                        ListTheLoai.Add(item);
                    }
                }
                return ListTheLoai;
            }
        }
        public static TheLoai? Item(string? id) {
            return _dbContext.TheLoais.FirstOrDefault(e=>e.Id== id);
        }
        public static bool Add(TheLoai theLoai)
        {
            try
            {
                _dbContext.TheLoais.Add(theLoai);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Update(TheLoai theLoai)
        {
            try
            {
                var temp = Item(theLoai.Id);
                if(temp == null) { return false; }
                temp.Name = theLoai.Name;
                _dbContext.TheLoais.Update(temp);
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
                var theLoai = Item(id);
                if (theLoai != null)
                {
                    _dbContext.TheLoais.Remove(theLoai);
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
