using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FASTFOOD;


namespace FASTFOOD.Security
{
    public class NguoiDungSecurity
    {
        public static bool DangNhap(string Email, string MatKhau)
        {
            using (CSDL_FASTFOOD_DBContext entities = new CSDL_FASTFOOD_DBContext())
            {
                return entities.USERS.Any(
                  u => u.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)
                  && u.MatKhau == MatKhau);
            }
        }

    }
}