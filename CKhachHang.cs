using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public enum GioiTinh { Nam,Nu, KhongXacDinh, KhongDoi }
    public class CKhachHang
    {
        public string maKH {  get; set; }
        public string tenKH { get; set; }
        public string sdt {  get; set; }
        public GioiTinh gt { get; set; }
        

        public CKhachHang() 
        {
            this.maKH = "";
            this.tenKH = "";
            this.sdt = "";
        }
        public CKhachHang(string maKH, string tenKH, string sdt, GioiTinh gt)
        {
            this.maKH = maKH;
            this.tenKH = tenKH;
            this.sdt = sdt;
            this.gt = gt;
        }
        public static bool KiemTraTrungMaKH(string maKHCanTim, List<CKhachHang> dskh)
        {
            foreach (CKhachHang khachHang in dskh)
            {
                if (khachHang.maKH == maKHCanTim)
                {
                    return true;
                }
            }
            return false;
        }
        public void NhapKH(List<CKhachHang> dskh)
        {

            Console.Write("Nhap ma khach hang: ");
            maKH = Console.ReadLine();
            while (KiemTraTrungMaKH(maKH, dskh))
            {
                Console.WriteLine("Ma khach hang da ton tai. Vui long nhap ma khac.");
                maKH = Console.ReadLine();
            }
            Console.Write("Nhap ten khach hang: ");
            tenKH = Console.ReadLine();
            
            Console.Write("Nhap so dien thoai khach hang: ");
            sdt= Console.ReadLine();
            do
            {
                Console.Write("Nhap gioi tinh khach hang: ");
                string gioiTinhiStr = Console.ReadLine().ToLower();
                if (!int.TryParse(gioiTinhiStr, out _) && Enum.TryParse(gioiTinhiStr, true, out GioiTinh parsedGioiTinh))
                {
                    gt=parsedGioiTinh;
                    break;
                }
                else
                {
                    Console.WriteLine("Gioi tinh khong hop le. Vui long nhap lai.");
                }
            } while (true);
        }
        public void XuatThongTinKH()
        {
            Console.WriteLine($"Ma KH: {maKH}");
            Console.WriteLine($"Ten khach hang: {tenKH}");
            Console.WriteLine($"So dien thoai: {sdt}");
            Console.WriteLine($"Gioi tinh: {gt}");
           
        }
    }
    public class CDanhSachKhachHang
    {
        List<CKhachHang> listKH=new List<CKhachHang>();
        public CDanhSachKhachHang()
        {

        }
        public List<CKhachHang> DanhSachKhachHang
        {
            get { return listKH; }
        }
        public string LayTenKHTheoMa(string maKH)
        {
            CKhachHang khachhang = listKH.Find(kh => kh.maKH == maKH);
            if (khachhang != null)
            {
                return khachhang.tenKH;
            }
            else
            {
                return "Khong tim thay san pham co ma: " + maKH;
            }
        }
        public string LaySDTTheoMa(string maKH)
        {
            CKhachHang khachhang = listKH.Find(kh => kh.maKH == maKH);
            if (khachhang != null)
            {
                return khachhang.sdt;
            }
            else
            {
                return "Khong tim thay san pham co ma: " + maKH;
            }
        }
        public GioiTinh LayGIoiTinhTheoMa(string maKH)
        {
            CKhachHang khachhang = listKH.Find(kh => kh.maKH == maKH);
            if (khachhang != null)
            {
                return khachhang.gt;
            }
            else
            {
                return GioiTinh.KhongXacDinh;
            }
        }
        public void TaoDSKhachHang(bool displayMenu = true)
        {
            Console.Clear();
            do
            {
                CKhachHang kh = new CKhachHang();
                kh.NhapKH(listKH);
                listKH.Add(kh);
                Console.WriteLine("Ban co muon nhap khach hang nua khong? (y/n)");
                string st;
                do
                {
                    st = Console.ReadLine().ToLower(); // chuyen doi thanh chu thuong de ko phan biet hoa thuong
                    if (st != "y" && st != "n")
                    {
                        Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                    }
                } while (st != "y" && st != "n");
                if (st == "n")
                    break;
            } while (true);
            GhiDanhSachKhachHang("danh_sach_khach_hang.txt");
            Console.WriteLine("Cac san pham da duoc them vao danh sach san pham");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void XuatDanhSachKhachHang(bool displayMenu = true)
        {
            if (listKH.Count == 0)
            {
                Console.WriteLine("Danh sach khach hang trong.");
                return;
            }
            Console.WriteLine("Danh sach san pham:");
            int i = 1;
            Console.WriteLine("{0,-20}{1,-10}{2,-20}{3,-20}{4,-15}", "Khach hang thu", "Ma KH", "Ten khach hang","So dien thoai", "Gioi tinh");
            foreach (CKhachHang kh in listKH)
            {
                Console.WriteLine("{0,-20}{1,-10}{2,-20}{3,-20}{4,-15}", i, kh.maKH, kh.tenKH, kh.sdt, kh.gt);
                i++;
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
            }
        }
        public bool TimKiemTrungMaKHKhiThem(string maSP)
        {
            foreach (CKhachHang kh in listKH)
            {
                if (kh.maKH==maSP)
                {
                    return true;
                }
            }
            return false;
        }
        public void ThemKhachHang(bool displayMenu = true)
        {
            Console.Clear();
            CKhachHang kh = new CKhachHang();
            kh.NhapKH(listKH);
            while (TimKiemTrungMaKHKhiThem(kh.maKH))
            {
                Console.WriteLine("Ma khach hang da ton tai. Vui long nhap ma khac.");
                kh.NhapKH(listKH);
            }
            listKH.Add(kh);
            GhiDanhSachKhachHang("danh_sach_khach_hang.txt", false);
            Console.WriteLine("Khach hang da duoc them");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void XoaKhachHang(bool displayMenu = true)
        {
            Console.Clear();
            XuatDanhSachKhachHang();
            Console.WriteLine("Chon ma khach hang muon xoa");
            string maKH = Console.ReadLine();
            CKhachHang khachHang = listKH.Find(kh => kh.maKH == maKH);
            if (khachHang != null)
            {
                listKH.Remove(khachHang);
                Console.WriteLine("Khach hang da bi xoa khoi danh sach.");
                GhiDanhSachKhachHang("danh_sach_khach_hang.txt", false);
            }
            else
            {
                Console.WriteLine("Khong tim thay khach hang co ma: " + maKH);
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void SuaThongTinKhachHang(bool displayMenu = true)
        {
            Console.Clear();
            XuatDanhSachKhachHang();
            Console.WriteLine("Chon ma khach hang muon sua");
            string maKH = Console.ReadLine();
            CKhachHang khachHang = listKH.Find(kh => kh.maKH == maKH);
            if (khachHang != null)
            {
                string tenkhcu = khachHang.tenKH;
                string sdtcu = khachHang.sdt;
                GioiTinh gioitinhcu = khachHang.gt;
                Console.WriteLine("Nhap thong tin moi cho khach hang:");
                Console.Write("Nhap ten khach hang: ");
                khachHang.tenKH = Console.ReadLine();
                if (khachHang.tenKH == "")
                    khachHang.tenKH = tenkhcu;
                Console.Write("Nhap so dien thoai khach hang: ");
                khachHang.sdt = Console.ReadLine();
                if (khachHang.sdt == "")
                    khachHang.sdt = sdtcu;
                do
                {
                    Console.Write("Nhap gioi tinh khach hang: ");
                    string gioiTinhiStr = Console.ReadLine().ToLower();
                    if (!int.TryParse(gioiTinhiStr, out _) && Enum.TryParse(gioiTinhiStr, true, out GioiTinh parsedGioiTinh))
                    {
                        khachHang.gt = parsedGioiTinh;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Gioi tinh khong hop le. Vui long nhap lai.");
                    }
                } while (true);
                if (khachHang.gt == GioiTinh.KhongDoi)
                    khachHang.gt = gioitinhcu;
                GhiDanhSachKhachHang("danh_sach_khach_hang.txt", false);
                Console.WriteLine("Khach hang đa đuoc cap nhat.");
            }
            else
            {
                Console.WriteLine("Khong tim thay khach hang co ma: " + khachHang.maKH);
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void TimKiemKHTheoMa(bool displayMenu = true)
        {
            Console.Clear();
            Console.WriteLine("Nhap ma khach hang muon tim:");
            string maKH = Console.ReadLine();
            CKhachHang khachHang = listKH.Find(kh => kh.maKH == maKH);
            if (khachHang != null)
            {
                Console.WriteLine("Khach hang đuoc tim thay:");
                khachHang.XuatThongTinKH();
            }
            else
            {
                Console.WriteLine("Khong tim thay khach hang co ma: " + maKH);
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public bool GhiDanhSachKhachHang(string tenTapTin, bool displayMenu = true)
        {
            try
            {
                using (StreamWriter noiDung = new StreamWriter(tenTapTin))
                {
                    noiDung.WriteLine("Khach hang thu\tMa KH\tTen khach hang\tSo dien thoai\tGioi tinh");
                    int i = 1;
                    foreach (CKhachHang kh in listKH)
                    {
                        noiDung.WriteLine(i + "\t\t  " + kh.maKH + "\t     " + kh.tenKH + "\t         " + kh.sdt + "\t   " + kh.gt );
                        i++;
                    }
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Loi khi ghi tep ");
                return false;
            }
        }
        public void DocDanhSachKhachHangTuTapTin(string tenTapTin, bool displayMenu = true)
        {
            List<CKhachHang> tempList = new List<CKhachHang>(); // Danh sach tam thoi
            try
            {
                listKH.Clear();
                using (StreamReader noiDung = new StreamReader(tenTapTin))
                {
                    string line;
                    while ((line = noiDung.ReadLine()) != null)
                    {
                        
                        string[] tokens = line.Split('\t'); 
                        if (tokens.Length >= 6) 
                        {
                            if (Enum.TryParse(tokens[5].Trim(), true, out GioiTinh gt))
                            {
                                listKH.Add(new CKhachHang(tokens[2].Trim(), tokens[3].Trim(), tokens[4].Trim(), gt));
                            }
                        }
                    }
                }
                // Sau khi đoc tu tap tin, gan danh sach tam thoi cho danh sach chinh
                listKH.AddRange(tempList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi doc du lieu tu file: " + ex.Message);
            }
        }
        public void MenuKhachhang()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("_______________DANH SACH KHACH HANG_______________");
                Console.WriteLine("| 0. Thoat                                       |");
                Console.WriteLine("| 1. Nhap danh sach khach hang                   |");
                Console.WriteLine("| 2. Xuat danh sach khach hang                   |");
                Console.WriteLine("| 3. Them khach hang vao danh sach khach hang    |");
                Console.WriteLine("| 4. Xoa khach hang ra khoi danh sach khach hang |");
                Console.WriteLine("| 5. Sua khach hang                              |");
                Console.WriteLine("| 6. Tim kiem khach hang                         |");
                Console.WriteLine("|________________________________________________|");
                Console.WriteLine("\nChon chuc nang");
                int chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        TaoDSKhachHang();
                        break;
                    case 2:
                        Console.Clear();
                        XuatDanhSachKhachHang();
                        break;
                    case 3:
                        ThemKhachHang();
                        break;
                    case 4:
                        XoaKhachHang();
                        break;
                    case 5:
                        Console.Clear();
                        SuaThongTinKhachHang();
                        break;
                    case 6:
                        TimKiemKHTheoMa();
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
