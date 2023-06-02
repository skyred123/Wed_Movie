using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public static class CT_HangBLL
    {
        public static List<CT_Hang> ListCT_Hang(List<string> listIdHang, string idPhanPhim)
        {
            List<CT_Hang> listcT_DienViens = new List<CT_Hang>();
            foreach (var str in listIdHang)
            {
                CT_Hang cT_DienVien = new CT_Hang()
                {
                    IdHang = str,
                    IdPhanPhim = idPhanPhim,
                };
                listcT_DienViens.Add(cT_DienVien);
            }
            return listcT_DienViens;
        }
    }
}
