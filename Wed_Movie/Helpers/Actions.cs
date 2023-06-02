using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wed_Movie.DAO;

namespace Wed_Movie.Functions
{
    public class Actions
    {
        public bool CheckAction(string nameAction, string nameHeader)
        {
            if (nameAction == "Update")
            {
                ActionDAO.AddOrEdit = "Update";
                ActionDAO.Heading = nameHeader;
                return true;
            }
            else if (nameAction == "Add")
            {
                ActionDAO.AddOrEdit = "Add";
                ActionDAO.Heading = nameHeader;
                return true;
            }
            return false;
        }
    }
}
