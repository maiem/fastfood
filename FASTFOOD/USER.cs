//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FASTFOOD
{
    using System;
    using System.Collections.Generic;
    
    public partial class USER
    {
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public Nullable<int> MaQuyen { get; set; }
        public Nullable<int> MaNV { get; set; }
        public Nullable<int> MaKH { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual NHOMQUYEN NHOMQUYEN { get; set; }
    }
}
