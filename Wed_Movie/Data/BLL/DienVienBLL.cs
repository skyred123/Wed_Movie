using Wed_Movie.Functions;
using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public class DienVienBLL
    {
        private static readonly AppDbContext _dbContext = AppDbContext.Create();

        public static List<DienVien> List(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dbContext.DienViens.ToList();
            }
            else
            {
                List<DienVien> ListDienVien = new List<DienVien>();
                foreach (var item in _dbContext.DienViens.ToList())
                {
                    if (UTF8_Convert.utf8Convert(item.Name).ToLower().Contains(search.ToLower()))
                    {
                        ListDienVien.Add(item);
                    }
                }
                return ListDienVien;
            }
        }
        public static DienVien? Item(string? id)
        {
            return _dbContext.DienViens.FirstOrDefault(e => e.Id == id);
        }
        public static bool Add(DienVien dienVien)
        {
            try
            {
                _dbContext.DienViens.Add(dienVien);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Update(DienVien dienVien)
        {
            try
            {
                var temp = Item(dienVien.Id);
                if (temp == null) { return false; }
                temp.Name = dienVien.Name;
                temp.Birthday = dienVien.Birthday;
                temp.Nationality = dienVien.Nationality;
                temp.Sex = dienVien.Sex;
                temp.Image = dienVien.Image;
                _dbContext.DienViens.Update(temp);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Delete(DienVien dienvien)
        {
            try
            {
                if (dienvien != null)
                {
                    _dbContext.DienViens.Remove(dienvien);
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
