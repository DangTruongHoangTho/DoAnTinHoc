using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class CHoaDon
    {
        public string maHoaDon { get; set; }
        public DateTime ngayTao { get; set; }
        public List<CSanPham> danhSachSanPham;
        public List<CSanPham> danhSachSanPhamTrongHoaDon;
        public List<CKhachHang> danhSachKhachHang;
        public List<CKhachHang> danhSachKhachHangTrongHoaDon;
        public CHoaDon(List<CSanPham> dsSanPham, List<CKhachHang> danhSachKhachHang)
        {
            this.maHoaDon = "";
            this.ngayTao = DateTime.Now;
            this.danhSachSanPham = dsSanPham;
            this.danhSachSanPhamTrongHoaDon = new List<CSanPham>();
            this.danhSachKhachHang = danhSachKhachHang;
            this.danhSachKhachHangTrongHoaDon= new List<CKhachHang> ();    
        }

        public CHoaDon(string maHoaDon, DateTime ngayTao,List<CSanPham> danhSachSanPham, List<CSanPham> dssptronghd,List<CKhachHang> dskh, List<CKhachHang> dskhtronghd)
        {
            this.maHoaDon = maHoaDon;
            this.ngayTao = ngayTao;
            this.danhSachSanPham = danhSachSanPham;
            this.danhSachSanPhamTrongHoaDon = dssptronghd;
            this.danhSachKhachHang = dskh;
            this.danhSachKhachHangTrongHoaDon = dskhtronghd;
        }


        public bool KiemTraTrungHoaDon(string maHDCanTim, List<CHoaDon> dshd)
        {
            foreach (CHoaDon hoaDon in dshd)
            {
                if (hoaDon.maHoaDon == maHDCanTim)
                {
                    return true;
                }
            }
            return false;
        }
        public CSanPham TimSanPhamTheoMaSP(string maSP)
        {
            foreach (CSanPham sp in danhSachSanPham)
            {
                if (sp.maSP == maSP)
                {
                    return sp;
                }
            }
            return null;
        }
        public CKhachHang TimKhachHangTheoMaKH(string maKH)
        {
            foreach (CKhachHang kh in danhSachKhachHang)
            {
                if (kh.maKH == maKH)
                {
                    return kh;
                }
            }
            return null;
        }
        public CSanPham TimSanPhamTheoMaSPTrongHoaDon(string maSP)
        {
            foreach (CSanPham sp in danhSachSanPhamTrongHoaDon)
            {
                if (sp.maSP == maSP)
                {
                    return sp;
                }
            }
            return null;
        }
        public void ThemSanPhamVaoHoaDon()
        {
            do
            {
                Console.WriteLine("Nhap ma san pham muon them vao hoa don:");
                string maSP = Console.ReadLine();
                // Kiem tra xem ma san pham đa nhap co ton tai trong danh sach san pham khong
                CSanPham sanPhamTimKiem = TimSanPhamTheoMaSP(maSP);
                if (sanPhamTimKiem != null)
                {
                    int soLuong;
                    do
                    {
                        Console.WriteLine($"Nhap so luong can mua cho san pham ({sanPhamTimKiem.tenSP}):");
                        while (!int.TryParse(Console.ReadLine(), out soLuong) || soLuong <= 0)
                        {
                            Console.WriteLine("So luong khong hop le. Vui long nhap lai.");
                        }

                        if (soLuong > sanPhamTimKiem.soLuong)
                        {
                            Console.WriteLine($"Khong du so luong cho san pham {maSP} trong kho.");
                            Console.WriteLine($"So luong hien tai trong kho: {sanPhamTimKiem.soLuong}");
                        }
                        else
                        {
                            break; //thoat neu soLuong nhap vao hop le
                        }

                        Console.WriteLine("Nhap lai so luong? (y/n)");
                        string st1;

                        do
                        {
                            st1 = Console.ReadLine().ToLower();
                            if (st1 != "y" && st1 != "n")
                            {
                                Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                            }
                        } while (st1 != "y" && st1 != "n");

                        if (st1 == "n")
                            return;

                    } while (true);

                    // Tao mot ban sao cua san pham voi so luong đa chi đinh va them vao hoa đon
                    CSanPham sanPhamInHoaDon = new CSanPham(sanPhamTimKiem.maSP, sanPhamTimKiem.tenSP, sanPhamTimKiem.gia, soLuong, sanPhamTimKiem.phanLoai);
                    danhSachSanPhamTrongHoaDon.Add(sanPhamInHoaDon);
                    CBanHang.dssp.CapNhatSoLuongSanPhamTrongFile(maSP, soLuong); //cap nhat so luong ton trong dssp
                    Console.WriteLine($"San pham ({sanPhamInHoaDon.tenSP}) da duoc them vao hoa don.");
                    Console.WriteLine("Ban co muon them san pham khac vao hoa don khong? (y/n)");
                    string st;
                    do
                    {
                        st = Console.ReadLine().ToLower();
                        if (st != "y" && st != "n")
                        {
                            Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                        }
                    } while (st != "y" && st != "n");

                    if (st == "n")
                        break;
                }
                else
                {
                    Console.WriteLine("Khong tim thay san pham co ma: " + maSP);
                    Console.WriteLine("Ban co muon nhap lai ma san pham khong? (y/n)");
                    string st;

                    do
                    {
                        st = Console.ReadLine().ToLower();
                        if (st != "y" && st != "n")
                        {
                            Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                        }
                    } while (st != "y" && st != "n");

                    if (st == "n")
                        break;
                }
            } while (true);
        }
        public void themKhachHangVaoHoaDon()
        {
            do
            {
                Console.WriteLine("Nhap ma khach hang:");
                string maKH = Console.ReadLine().ToLower();
                // Kiem tra xem ma san pham đa nhap co ton tai trong danh sach san pham khong
                CKhachHang khachHangTimKiem = TimKhachHangTheoMaKH(maKH);
                if (khachHangTimKiem != null)
                {
                    // Tao mot ban sao cua san pham voi so luong đa chi đinh va them vao hoa đon
                    CKhachHang khachHangTrongHoaDon = new CKhachHang(khachHangTimKiem.maKH,khachHangTimKiem.tenKH,khachHangTimKiem.sdt,khachHangTimKiem.gt);
                    danhSachKhachHangTrongHoaDon.Add(khachHangTrongHoaDon);
                    Console.WriteLine($"Khach hang ({khachHangTrongHoaDon.tenKH}) da duoc them vao hoa don.");
                    break;
                }
                else
                {
                    Console.WriteLine("Khong tim thay khach hang co ma: " + maKH);
                    Console.WriteLine("Ban co muon nhap lai ma khach hang khong? (y/n)");
                    string st;

                    do
                    {
                        st = Console.ReadLine().ToLower();
                        if (st != "y" && st != "n")
                        {
                            Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                        }
                    } while (st != "y" && st != "n");

                    if (st == "n")
                        break;
                }
            } while (true);
        }
        public void SuaSanPhamTrongHoaDonTheoMa()
        {
            
            Console.WriteLine("Nhap ma san pham can sua:");
            string maSPCanSua = Console.ReadLine();
            CSanPham sanPhamCanSua = TimSanPhamTheoMaSPTrongHoaDon(maSPCanSua);
            if (sanPhamCanSua != null)
            {
                Console.WriteLine("Nhap so luong moi:");
                int soLuongMoi;
                while (!int.TryParse(Console.ReadLine(), out soLuongMoi) || soLuongMoi <= 0)
                {
                    Console.WriteLine("So luong khong hop le. Vui long nhap lai.");
                }
                CBanHang.dssp.CapNhatSoLuongSanPhamTrongFileSauKhiSuaHoaDon(maSPCanSua,sanPhamCanSua.soLuong,soLuongMoi);
                sanPhamCanSua.soLuong = soLuongMoi;
            }
            else
            {
                Console.WriteLine($"Khong tim thay san pham co ma {maSPCanSua} trong hoa don.");
            }
        }

        public void NhapHoaDon(List<CHoaDon> dshd)
        {
            Console.Write("Nhap ma hoa don: ");
            maHoaDon = Console.ReadLine();

            while (KiemTraTrungHoaDon(maHoaDon, dshd))
            {
                Console.WriteLine("Ma hoa don da ton tai. Vui long nhap ma khac.");
                maHoaDon = Console.ReadLine();
            }
            // ngayTao bang ngay hien tai
            ngayTao = DateTime.Now;
            Console.WriteLine("Nhap thong tin khach hang: ");
            themKhachHangVaoHoaDon();
            Console.WriteLine("Nhap thong tin san pham cho hoa don:");
            ThemSanPhamVaoHoaDon();
        }
        public void XuatThongTinSanPhamTrongHoaDon()
        {
           
            if (danhSachSanPhamTrongHoaDon.Count == 0)
            {
                Console.WriteLine("Hoa don chua co san pham.");
            }
            else
            {
                Console.WriteLine("Danh sach san pham trong hoa don:");

                int i = 1;
                double tongTienHoaDon = 0;

                Console.WriteLine("{0,-15}{1,-10}{2,-35}{3,-15}{4,-15}{5,-15}{6,-15}", "San pham thu", "Ma SP", "Ten san pham", "Gia", "So luong", "Phan loai", "TtienSP");
                foreach (CSanPham sp in danhSachSanPhamTrongHoaDon)
                {
                    string tensp = CBanHang.dssp.LayTenSanPhamTheoMa(sp.maSP);
                    PhanLoai pl = CBanHang.dssp.LayPhanLoaiTheoMa(sp.maSP);
                    double tienSanPham = TinhTienSanPham(sp);
                    Console.WriteLine("{0,-15}{1,-10}{2,-35}{3,-15}{4,-15}{5,-15}{6,-15}", i, sp.maSP, tensp, sp.gia, sp.soLuong, pl, tienSanPham);
                    i++;
                    tongTienHoaDon += tienSanPham;
                }
                Console.WriteLine("Tong tien hoa don: " + tongTienHoaDon);
            }
        }
        public double TinhTienSanPham(CSanPham sanPham)
        {
            return sanPham.gia * sanPham.soLuong;
        }

        public void XuatHoaDon()
        {
            
            Console.WriteLine($"Ma Hoa Don: {maHoaDon}");
            Console.WriteLine($"Ngay Tao: {ngayTao}");
            foreach (CKhachHang kh in danhSachKhachHangTrongHoaDon)
            {
                string tenKH = CBanHang.dskh.LayTenKHTheoMa(kh.maKH);
                string sdt= CBanHang.dskh.LaySDTTheoMa(kh.maKH);
                GioiTinh gt=CBanHang.dskh.LayGIoiTinhTheoMa(kh.maKH);
                Console.WriteLine($"Ma KH: {kh.maKH}");
                Console.WriteLine($"Ten khach hang: {tenKH}");
                Console.WriteLine($"So dien thoai: {sdt}");
                Console.WriteLine($"Gioi tinh: {gt}");
            }
            XuatThongTinSanPhamTrongHoaDon();
            Console.WriteLine($"==========================================================================");

        }
    }
    public class CDanhSachHoaDon
    {

        private List<CHoaDon> danhSachHoaDon = new List<CHoaDon>();
        private List<CSanPham> dssp = new List<CSanPham>();
        private List<CKhachHang> dskh=new List<CKhachHang>();
        public CDanhSachHoaDon()
        {

        }
        public List<CHoaDon> layDanhSachHoaDon
        {
            get { return danhSachHoaDon; }
        }
        public void TaoDSHoaDon(bool displayMenu = true)
        {
            Console.Clear();
            do
            {
                CBanHang.dssp.XuatDanhSachSanPham();
                CBanHang.dskh.XuatDanhSachKhachHang();
                CHoaDon hd = new CHoaDon(CBanHang.dssp.DanhSachSanPham,CBanHang.dskh.DanhSachKhachHang);
                hd.NhapHoaDon(danhSachHoaDon);
                danhSachHoaDon.Add(hd);
                Console.WriteLine("Ban co muon nhap hoa don nua khong? (y/n)");
                string st;
                do
                {
                    st = Console.ReadLine().ToLower();
                    if (st != "y" && st != "n")
                    {
                        Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                    }
                } while (st != "y" && st != "n");
                if (st == "n")
                    break;
            } while (true);
            GhiDanhSachHoaDonVaoFile("danh_sach_hoa_don.txt");
            Console.WriteLine("Cac hoa don da duoc them vao danh sach hoa don");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void XuatDanhSachHoaDon(bool displayMenu = true)
        {
            Console.Clear();
            if (danhSachHoaDon.Count == 0)
            {
                Console.WriteLine("Danh sach san pham trong.");
                return;
            }
            Console.WriteLine("Danh sach hoa don:");
            int i = 1;
            foreach (CHoaDon hd in danhSachHoaDon)
            {
                Console.WriteLine($"Hoa don thu " + i);
                hd.XuatHoaDon();
                i++;
            }
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
            }
        }
        public bool KiemTraTrungHoaDon(string maHDCanTim)
        {
            foreach (CHoaDon hoaDon in danhSachHoaDon)
            {
                if (hoaDon.maHoaDon == maHDCanTim)
                {
                    return true;
                }
            }
            return false;
        }
        public void Them1HoaDon(bool displayMenu = true)
        {
            Console.Clear();
            CBanHang.dssp.XuatDanhSachSanPham();
            CBanHang.dskh.XuatDanhSachKhachHang();
            CHoaDon hoaDon = new CHoaDon(CBanHang.dssp.DanhSachSanPham, CBanHang.dskh.DanhSachKhachHang);
            hoaDon.NhapHoaDon(danhSachHoaDon);
            while (hoaDon.KiemTraTrungHoaDon(hoaDon.maHoaDon, danhSachHoaDon))
            {
                Console.WriteLine("Ma hoa don da ton tai. Vui long nhap ma khac.");
                hoaDon.NhapHoaDon(danhSachHoaDon);
            }
            danhSachHoaDon.Add(hoaDon);
            GhiDanhSachHoaDonVaoFile("danh_sach_hoa_don.txt");
            Console.WriteLine("Hoa don da duoc them vao danh sach.");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void SuaHoaDonTheoMa(bool displayMenu = true)
        {
            do
            {
                Console.Clear();
                XuatDanhSachHoaDon();
                Console.Write("Nhap ma hoa don muon sua: ");
                string maHoaDonSua = Console.ReadLine();
                CHoaDon hoaDonCanSua = danhSachHoaDon.Find(hd => hd.maHoaDon == maHoaDonSua);

                if (hoaDonCanSua != null)
                {
                    Console.Clear();
                    hoaDonCanSua.XuatHoaDon();
                    Console.ReadLine();
                    hoaDonCanSua.SuaSanPhamTrongHoaDonTheoMa();
                    GhiDanhSachHoaDonVaoFile("danh_sach_hoa_don.txt");
                    Console.WriteLine("Hoa don đuoc cap nhat.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Khong tim thay hoa don co ma {maHoaDonSua} de sua.");
                   
                }
                Console.WriteLine("Nhap lai? (y/n)");
                string st;
                do
                {
                    st = Console.ReadLine().ToLower();
                    if (st != "y" && st != "n")
                    {
                        Console.WriteLine("Nhap sai, vui long nhap lai (y/n).");
                    }
                } while (st != "y" && st != "n");
                if (st == "n")
                    break; ;
            } while (true);
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void XoaHoaDonTheoMa(bool displayMenu = true)
        {
            Console.Clear();
            Console.Write("Nhap ma hoa don muon xoa: ");
            string maHoaDonXoa = Console.ReadLine();

            CHoaDon hoaDonCanXoa = danhSachHoaDon.Find(hd => hd.maHoaDon == maHoaDonXoa);

            if (hoaDonCanXoa != null)
            {
                Console.WriteLine("Hoa don truoc khi xoa:");
                hoaDonCanXoa.XuatHoaDon();

                Console.WriteLine("Ban co chac chan muon xoa hoa don nay? (y/n)");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "y")
                {
                    danhSachHoaDon.Remove(hoaDonCanXoa);
                    Console.WriteLine("Da xoa hoa don thanh cong.");
                    GhiDanhSachHoaDonVaoFile("danh_sach_hoa_don.txt");
                }
                else
                {
                    Console.WriteLine("Hoa don khong bi xoa.");
                }
            }
            else
            {
                Console.WriteLine($"Khong tim thay hoa don co ma {maHoaDonXoa} de xoa.");
            }
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void TimHoaDonTheoMa(bool displayMenu = true)
        {
            Console.Clear();
            Console.Write("Nhap ma hoa don muon tim: ");
            string maHoaDonTimKiem = Console.ReadLine();

            CHoaDon hoaDonTimKiem = danhSachHoaDon.Find(hd => hd.maHoaDon == maHoaDonTimKiem);

            if (hoaDonTimKiem != null)
            {
                Console.WriteLine("Hoa don tim thay:");
                hoaDonTimKiem.XuatHoaDon();
            }
            else
            {
                Console.WriteLine($"Khong tim thay hoa don co ma {maHoaDonTimKiem}.");
            }
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void GhiDanhSachHoaDonVaoFile(string tenTep)
        {
            try
            {
                using (StreamWriter noiDung = new StreamWriter(tenTep))
                {
                    noiDung.WriteLine("Danh sach hoa don:");
                    int j = 1;
                    foreach (CHoaDon hoaDon in danhSachHoaDon)
                    {
                        noiDung.WriteLine($"Hoa don thu " + j);
                        noiDung.WriteLine($"Ma Hoa Don: {hoaDon.maHoaDon}");
                        noiDung.WriteLine($"Ngay Tao: {hoaDon.ngayTao}");
                        foreach (CKhachHang kh in hoaDon.danhSachKhachHangTrongHoaDon)
                        {
                            noiDung.WriteLine($"Ma KH: {kh.maKH}");
                        }
                        noiDung.WriteLine("Danh sach san pham trong hoa don:");
                        noiDung.WriteLine("Ma SP\tGia\tSo luong");
                        foreach (CSanPham sp in hoaDon.danhSachSanPhamTrongHoaDon)
                        {
                            noiDung.WriteLine(sp.maSP +"\t"+ sp.gia + "\t" + sp.soLuong);
                        }
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ghi file that bai. {ex.Message}");
            }
        }
        public void DocDanhSachHoaDonTuFile(string tenTep)
        {
            try
            {
                danhSachHoaDon.Clear();
                CHoaDon hoaDon = null;
                CBanHang.dssp.DocDanhSachSanPhamTuTapTin("danh_sach_san_pham.txt");
                CBanHang.dskh.DocDanhSachKhachHangTuTapTin("danh_sach_khach_hang.txt");
                using (StreamReader sr = new StreamReader(tenTep))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("Ma Hoa Don:"))
                        {
                            hoaDon = new CHoaDon(dssp,dskh);
                            hoaDon.maHoaDon = line.Substring("Ma Hoa Don: ".Length);
                        }
                        else if (line.StartsWith("Ngay Tao:"))
                        {
                            DateTime ngayTao;
                            if (DateTime.TryParse(line.Substring("Ngay Tao: ".Length), out ngayTao))
                            {
                                hoaDon.ngayTao = ngayTao;
                            }
                        }
                        else if (line.StartsWith("Ma KH:"))
                        {
                            CKhachHang khachHang = new CKhachHang
                            {
                                maKH = line.Substring("Ma KH: ".Length)
                            };
                            hoaDon.danhSachKhachHangTrongHoaDon.Add(khachHang);
                        }
                        else if (line.Equals("Ma SP\tGia\tSo luong"))
                        {
                            hoaDon.danhSachSanPhamTrongHoaDon = new List<CSanPham>();
                            while ((line = sr.ReadLine()) != null)
                            {
                                
                                string[] sanPhamInfo = line.Split('\t');
                                if (sanPhamInfo.Length < 3) break;
                                {
                                    CSanPham sanPham = new CSanPham
                                    {
                                        maSP = sanPhamInfo[0].Trim(),
                                        gia = double.Parse(sanPhamInfo[1].Trim()),
                                        soLuong = int.Parse(sanPhamInfo[2].Trim()),
                                    };
                                    hoaDon.danhSachSanPhamTrongHoaDon.Add(sanPham);
                                }
                            }
                            if (hoaDon != null)
                            {
                                danhSachHoaDon.Add(hoaDon);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Doc file that bai. {ex.Message}");
            }
        }

        public void MenuHoaDon()
        {
            CHoaDon hoaDon = new CHoaDon(CBanHang.dssp.DanhSachSanPham, CBanHang.dskh.DanhSachKhachHang);
            do
            {
                Console.Clear();
                Console.WriteLine("_______________DANH SACH HOA DON_______________");
                Console.WriteLine("| 0. Thoat                                    |");
                Console.WriteLine("| 1. Nhap danh sach hoa don                   |");
                Console.WriteLine("| 2. Xuat danh sach hoa don                   |");
                Console.WriteLine("| 3. Them hoa don vao danh sach hoa don       |");
                Console.WriteLine("| 4. Xoa hoa don ra khoi danh sach hoa don    |");
                Console.WriteLine("| 5. Sua hoa don                              |");
                Console.WriteLine("| 6. Tim kiem hoa don                         |");
                Console.WriteLine("|_____________________________________________|");
                Console.WriteLine("\nChon chuc nang");
                int chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        Console.Clear();
                        TaoDSHoaDon();

                        break;
                    case 2:
                        Console.Clear();
                        XuatDanhSachHoaDon();
                        break;
                    case 3:
                        Console.Clear();
                        Them1HoaDon();
                        break;
                    case 4:
                        Console.Clear();
                        XoaHoaDonTheoMa();
                        break;
                    case 5:
                        Console.Clear();
                        SuaHoaDonTheoMa();
                        break;
                    case 6:
                        Console.Clear();
                        TimHoaDonTheoMa();
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
