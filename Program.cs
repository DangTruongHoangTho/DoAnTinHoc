using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoAnTinHoc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CBanHang.dssp.DocDanhSachSanPhamTuTapTin("danh_sach_san_pham.txt");
            CBanHang.dskh.DocDanhSachKhachHangTuTapTin("danh_sach_khach_hang.txt");
            CBanHang.dshd.DocDanhSachHoaDonTuFile("danh_sach_hoa_don.txt");
            do
            {
                Console.WriteLine("___________DANH SACH MENU___________");
                Console.WriteLine("| 0. Thoat                         |");
                Console.WriteLine("| 1. Danh sach san pham            |");
                Console.WriteLine("| 2. Danh sach khach hang          |");
                Console.WriteLine("| 3. Danh sach hoa don             |");
                Console.WriteLine("| 4. Thong ke doanh so             |");
                Console.WriteLine("|__________________________________|");
                Console.WriteLine("\nChon chuc nang");
                int chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1: //danh sach san pham
                        CBanHang.dssp.MenuSanPham();
                        break;
                    case 2: //danh sach khach hang
                        CBanHang.dskh.MenuKhachhang();
                        break;
                    case 3: //danh sach hoa hon
                        CBanHang.dshd.MenuHoaDon();
                        break;
                    case 4: //thong ke doanh so
                        CBanHang.thongKe.MenuThongKe();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                        break;
                }
            } while (true);
        }
    }
}
