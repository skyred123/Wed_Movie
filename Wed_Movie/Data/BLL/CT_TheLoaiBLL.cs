using Wed_Movie.Models;

namespace Wed_Movie.Data.BLL
{
    public static class CT_TheLoaiBLL
    {
        public static List<CT_TheLoai> ListCT_TheLoai(List<string> listIdTheLoai, string idPhanPhim)
        {
            List<CT_TheLoai> listcT_TheLoais = new List<CT_TheLoai>();
            foreach (var str in listIdTheLoai)
            {
                CT_TheLoai cT_DienVien = new CT_TheLoai()
                {
                    IdTheLoai = str,
                    IdPhanPhim = idPhanPhim,
                };
                listcT_TheLoais.Add(cT_DienVien);
            }
            return listcT_TheLoais;
        }
    }
}
