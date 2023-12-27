using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    internal class CThongKe
    {
        private List<CHoaDon> danhSachHoaDon;
        public CThongKe()
        {

        }
        public CThongKe(List<CHoaDon> dsHoaDon)
        {
            this.danhSachHoaDon = dsHoaDon;
        }
        public void SanPhamBanNhieuNhat(bool displayMenu = true)
        {
            Console.Clear();
            if (danhSachHoaDon.Count == 0)
            {
                Console.WriteLine("Danh sach hoa don trong.");
            }
            else
            {
                Dictionary<string, int> soLuongBanTheoSanPham = new Dictionary<string, int>();

                foreach (CHoaDon hoaDon in danhSachHoaDon)
                {
                    foreach (CSanPham sanPham in hoaDon.danhSachSanPhamTrongHoaDon)
                    {
                        if (soLuongBanTheoSanPham.ContainsKey(sanPham.maSP))
                        {
                            soLuongBanTheoSanPham[sanPham.maSP] += sanPham.soLuong;
                        }
                        else
                        {
                            soLuongBanTheoSanPham[sanPham.maSP] = sanPham.soLuong;
                        }
                    }
                }

                var sanPhamBanNhieuNhat = soLuongBanTheoSanPham.OrderByDescending(x => x.Value).FirstOrDefault();

                if (sanPhamBanNhieuNhat.Key != null)
                {
                    CSanPham sanPham = CBanHang.dssp.TimSanPhamTheoMaSP(sanPhamBanNhieuNhat.Key);
                    if (sanPham != null)
                    {
                        Console.WriteLine($"Thong tin san pham ban it nhat:");
                        Console.WriteLine($"Ma SP: {sanPham.maSP}");
                        Console.WriteLine($"Ten san pham: {sanPham.tenSP}");
                        Console.WriteLine($"Gia: {sanPham.gia}");
                        Console.WriteLine($"Phan loai: {sanPham.phanLoai}");
                    }
                    Console.WriteLine($"So luong da ban: {sanPhamBanNhieuNhat.Value}");
                }
                else
                {
                    Console.WriteLine("Chua co hoa don nao.");
                }
            }
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void SanPhamBanItNhat(bool displayMenu = true)
        {
            Console.Clear();
            if (danhSachHoaDon.Count == 0)
            {
                Console.WriteLine("Danh sach hoa don trong.");
            }
            else
            {
                Dictionary<string, int> soLuongBanTheoSanPham = new Dictionary<string, int>();

                foreach (CHoaDon hoaDon in danhSachHoaDon)
                {
                    foreach (CSanPham sanPham in hoaDon.danhSachSanPhamTrongHoaDon)
                    {
                        if (soLuongBanTheoSanPham.ContainsKey(sanPham.maSP))
                        {
                            soLuongBanTheoSanPham[sanPham.maSP] += sanPham.soLuong;
                        }
                        else
                        {
                            soLuongBanTheoSanPham[sanPham.maSP] = sanPham.soLuong;
                        }
                    }
                }

                var sanPhamBanItNhat = soLuongBanTheoSanPham.OrderBy(x => x.Value).FirstOrDefault();

                if (sanPhamBanItNhat.Key != null)
                {
                    CSanPham sanPham = CBanHang.dssp.TimSanPhamTheoMaSP(sanPhamBanItNhat.Key);
                    if (sanPham != null)
                    {
                        Console.WriteLine($"Thong tin san pham ban it nhat:");
                        Console.WriteLine($"Ma SP: {sanPham.maSP}");
                        Console.WriteLine($"Ten san pham: {sanPham.tenSP}");
                        Console.WriteLine($"Gia: {sanPham.gia}");
                        Console.WriteLine($"Phan loai: {sanPham.phanLoai}");
                    }
                    Console.WriteLine($"So luong da ban: {sanPhamBanItNhat.Value}");
                }
                else
                {
                    Console.WriteLine("Chua co hoa don nao.");
                }
            }
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public double TongTienBanDuocTuNgayDenNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            double tongTien = 0;
            foreach (CHoaDon hoaDon in danhSachHoaDon)
            {
                if (hoaDon.ngayTao >= ngayBatDau && hoaDon.ngayTao <= ngayKetThuc)
                {
                    foreach (CSanPham sanPham in hoaDon.danhSachSanPhamTrongHoaDon)
                    {
                        tongTien += sanPham.gia * sanPham.soLuong;
                    }
                }
            }
            return tongTien;
        }

        public void NhapNgayBatDauNgayKetThuc(bool displayMenu = true)
        {
            Console.Clear();
            DateTime ngayBatDau, ngayKetThuc;
            Console.WriteLine("Nhap ngay bat đau (theo dinh dang dd/MM/yyyy):");
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayBatDau))
            {
                Console.WriteLine("Nhap sai dinh dang ngay. Vui long nhap lai:");
            }
            Console.WriteLine("Nhap ngay ket thuc (theo dinh dang dd/MM/yyyy):");
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayKetThuc))
            {
                Console.WriteLine("Nhap sai dinh dang ngay. Vui long nhap lai:");
            }
            double tongTien = TongTienBanDuocTuNgayDenNgay(ngayBatDau, ngayKetThuc);
            
            Console.WriteLine($"Danh sach cac hoa don tu ngay {ngayBatDau.ToString("dd/MM/yyyy")} den ngay {ngayKetThuc.ToString("dd/MM/yyyy")}:");
            foreach (CHoaDon hoaDon in danhSachHoaDon)
            {
                if (hoaDon.ngayTao >= ngayBatDau && hoaDon.ngayTao <= ngayKetThuc)
                {
                    Console.WriteLine($"Ma hoa don: {hoaDon.maHoaDon}");
                    Console.WriteLine($"Ngay tao: {hoaDon.ngayTao.ToString("dd/MM/yyyy")}");

                    double tongTienHoaDon = 0;
                    foreach (CSanPham sanPham in hoaDon.danhSachSanPhamTrongHoaDon)
                    {
                        tongTienHoaDon += sanPham.gia * sanPham.soLuong;
                    }

                    Console.WriteLine($"Tong tien hoa don: {tongTienHoaDon}");
                    Console.WriteLine("-----------------------------------");
                }
            }
            Console.WriteLine($"Tong tien ban duoc la: {tongTien}");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void MenuThongKe()
        {
            CThongKe tk = new CThongKe(CBanHang.dshd.layDanhSachHoaDon);
            do
            {
                Console.Clear();
                Console.WriteLine("__________________THONG KE DOANH SO_______________");
                Console.WriteLine("| 0. Thoat                                       |");
                Console.WriteLine("| 1. San pham ban nhieu nhat                     |");
                Console.WriteLine("| 2. San pham ban nhieu nhat                     |");
                Console.WriteLine("| 3. Tong tien ban duoc tu ngay ... den ngay ... |");
                Console.WriteLine("|________________________________________________|");
                Console.WriteLine("\nChon chuc nang");
                int chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        tk.SanPhamBanNhieuNhat();
                        break;
                    case 2:
                        tk.SanPhamBanItNhat();
                        break;
                    case 3:
                        tk.NhapNgayBatDauNgayKetThuc();
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                        Console.WriteLine("Nhan Enter de nhap lai...");
                        Console.ReadKey();
                        break;
                }
            } while (true);
        }
    }
}