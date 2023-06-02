using Wed_Movie.DAO;
using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public static class CT_DienVienBLL
    {
        public static List<CT_DienVien> ListCT_DienVien(List<string> listIdDienVien, string idPhanPhim)
        {
            List<CT_DienVien> listcT_DienViens = new List<CT_DienVien>();
            foreach (var str in listIdDienVien)
            {
                CT_DienVien cT_DienVien = new CT_DienVien()
                {
                    IdDienVien = str,
                    IdPhanPhim = idPhanPhim,
                };
                listcT_DienViens.Add(cT_DienVien);
            }
            return listcT_DienViens;
        }
    }
}
