using Wed_Movie.Functions;
using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public class PhanPhimBLL
    {

        private static AppDbContext CreateDbContext()
        {
            return AppDbContext.Create();
        }

        public static List<PhanPhim> List(string search)
        {
            using (var dbContext = CreateDbContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return dbContext.PhanPhims.ToList();
                }
                else
                {
                    List<PhanPhim> ListPhanPhim = new List<PhanPhim>();
                    foreach (var item in dbContext.PhanPhims.ToList())
                    {
                        if (UTF8_Convert.utf8Convert(item.Name).ToLower().Contains(search.ToLower()))
                        {
                            ListPhanPhim.Add(item);
                        }
                    }
                    return ListPhanPhim;
                }
            }
        }
        public static PhanPhim? Item(string? id)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.PhanPhims.FirstOrDefault(e => e.Id == id);
            }
            
        }
        public static bool Add(PhanPhim PhanPhim)
        {
            using (var dbContext = CreateDbContext())
            {
                try
                {
                    dbContext.PhanPhims.Add(PhanPhim);
                    dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }
        public static bool Update(PhanPhim phanPhim)
        {
            using (var dbContext = CreateDbContext())
            {
                try
                {
                    var temp = Item(phanPhim.Id);
                    if (temp == null) { return false; }
                    temp.Name = phanPhim.Name;
                    temp.Description = phanPhim.Description;
                    temp.TimeUpdate = phanPhim.TimeUpdate;
                    temp.PublicYear = phanPhim.PublicYear;
                    temp.CT_DienVien = phanPhim.CT_DienVien;
                    temp.CT_Hangs = phanPhim.CT_Hangs;
                    temp.CT_TheLoais = phanPhim.CT_TheLoais;
                    temp.PhimId = phanPhim.PhimId;
                    temp.Image = phanPhim.Image;
                    temp.Trailer = phanPhim.Trailer;
                    dbContext.PhanPhims.Update(temp);
                    dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }
        public static bool Delete(PhanPhim phanPhim)
        {
            using (var dbContext = CreateDbContext())
            {
                try
                {
                    if (phanPhim != null)
                    {
                        dbContext.PhanPhims.Remove(phanPhim);
                        dbContext.SaveChanges();
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
}
