using Microsoft.EntityFrameworkCore;
using Wed_Movie.Functions;
using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public static class HangBLL
    {
        private static readonly AppDbContext _dbContext = AppDbContext.Create();

        public static List<Hang> List(string search)
        {
            if (string.IsNullOrEmpty(search)) {
                return _dbContext.Hangs.ToList();
            }
            else
            {
                List<Hang> ListHang = new List<Hang>();
                foreach (var item in _dbContext.Hangs.ToList())
                {
                    if (UTF8_Convert.utf8Convert(item.Name).ToLower().Contains(search.ToLower()))
                    {
                        ListHang.Add(item);
                    }
                }
                return ListHang;
            }
        }
        public static Hang? Item(string? id)
        {
            return _dbContext.Hangs.FirstOrDefault(e => e.Id == id);
        }
        public static bool Add(Hang theLoai)
        {
            try
            {
                _dbContext.Hangs.Add(theLoai);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Update(Hang hang)
        {
            try
            {
                var temp = Item(hang.Id);
                if (temp == null) { return false; }
                temp.Name = hang.Name;
                _dbContext.Hangs.Update(temp);
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
                    _dbContext.Hangs.Remove(theLoai);
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
