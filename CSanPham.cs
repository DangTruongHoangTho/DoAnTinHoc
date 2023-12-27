using System;
using System.Collections.Generic;
using System.IO;

namespace DoAnTinHoc
{
    public enum PhanLoai { Cho, Meo, KhongXacDinh, KhongDoi }
    public class CSanPham
    {
        public string maSP { get; set; }
        public string tenSP { get; set; }
        public double gia { get; set; }
        public int soLuong { get; set; }
        public PhanLoai phanLoai { get; set; }
        public CSanPham()
        {
            this.maSP = "";
            this.tenSP = "";
            this.gia = 0;
            this.soLuong = 0;
        }

        public CSanPham(string maSP, string tenSP, double gia, int soLuong, PhanLoai phanLoai)
        {
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.gia = gia;
            this.soLuong = soLuong;
            this.phanLoai = phanLoai;
        }

        public static bool KiemTraTrungMaSP(string maSPCanTim, List<CSanPham> dssp)
        {
            foreach (CSanPham sanPham in dssp)
            {
                if (sanPham.maSP == maSPCanTim)
                {
                    return true;
                }
            }
            return false;
        }
        public void NhapSP(List<CSanPham> dssp)
        {

            Console.Write("Nhap ma SP: ");
            maSP = Console.ReadLine();
            while (KiemTraTrungMaSP(maSP, dssp))
            {
                Console.WriteLine("Ma san pham da ton tai. Vui long nhap ma khac.");
                maSP = Console.ReadLine();
            }
            Console.Write("Nhap ten san pham: ");
            tenSP = Console.ReadLine();
            do
            {
                Console.Write("Nhap gia san pham: ");
                string giaStr = Console.ReadLine();
                gia = double.Parse(giaStr);
                if (gia < 0)
                {
                    Console.Write("Gia san pham phai lon hon hoac bang 0. Vui long nhap lai gia san pham: ");
                    gia = double.Parse(giaStr);
                }
            } while (gia < 0);
            Console.Write("Nhap so luong san pham: ");
            string soLuongStr = Console.ReadLine();
            soLuong = int.Parse(soLuongStr);
            do
            {
                Console.Write("Nhap phan loai san pham: ");
                string phanLoaiStr = Console.ReadLine().ToLower();
                if (!int.TryParse(phanLoaiStr, out _) && Enum.TryParse(phanLoaiStr, true, out PhanLoai parsedPhanLoai))
                {
                    phanLoai = parsedPhanLoai;
                    break;
                }
                else
                {
                    Console.WriteLine("Phan loai khong hop le. Vui long nhap lai.");
                }
            } while (true);
        }

        public void XuatThongTinSP()
        {
            Console.WriteLine($"Ma SP: {maSP}");
            Console.WriteLine($"Ten san pham: {tenSP}");
            Console.WriteLine($"Gia: {gia}");
            Console.WriteLine($"So luong: {soLuong}");
            Console.WriteLine($"Phan loai: {phanLoai}");
        }


    }

    public class CDanhSachSanPham
    {
        private List<CSanPham> list = new List<CSanPham>();
        public CDanhSachSanPham()
        {

        }
        public List<CSanPham> DanhSachSanPham
        {
            get { return list; }
        }
        public CSanPham TimSanPhamTheoMaSP(string maSP)
        {
            foreach (CSanPham sanPham in list)
            {
                if (sanPham.maSP == maSP)
                {
                    return sanPham;
                }
            }
            return null;
        }
        public string LayTenSanPhamTheoMa(string maSP)
        {
            CSanPham sanPham = list.Find(sp => sp.maSP == maSP);
            if (sanPham != null)
            {
                return sanPham.tenSP;
            }
            else
            {
                return "Khong tim thay san pham co ma: " + maSP;
            }
        }
    
        public PhanLoai LayPhanLoaiTheoMa(string maSP)
        {
            CSanPham sanPham = list.Find(sp => sp.maSP == maSP);
            if (sanPham != null)
            {
                return sanPham.phanLoai;
            }
            else
            {
                return PhanLoai.KhongXacDinh;
            }
        }
        public void TaoDSSanPham(bool displayMenu = true)
        {
            Console.Clear();
            do
            {
                CSanPham sp = new CSanPham();
                sp.NhapSP(list);
                list.Add(sp);
                Console.WriteLine("Ban co muon nhap san phan nua khong? (y/n)");
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
            GhiDanhSachSanPham("danh_sach_san_pham.txt");
            Console.WriteLine("Cac san pham da duoc them vao danh sach san pham");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void XuatDanhSachSanPham(bool displayMenu = true)
        {
            Console.Clear();
            if (list.Count == 0)
            {
                Console.WriteLine("Danh sach san pham trong.");
                return;
            }
            Console.WriteLine("Danh sach san pham:");
            int i = 1;
            Console.WriteLine("{0,-15}{1,-10}{2,-35}{3,-15}{4,-15}{5,-15}", "San pham thu", "Ma SP", "Ten san pham", "Gia", "So luong", "Phan loai");
            foreach (CSanPham sp in list)
            {
                Console.WriteLine("{0,-15}{1,-10}{2,-35}{3,-15}{4,-15}{5,-15}", i, sp.maSP, sp.tenSP, sp.gia, sp.soLuong, sp.phanLoai);
                i++;
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
            }
        }
        public bool KiemTrungMaSanPham(string maSP)
        {
            foreach (CSanPham sanPham in list)
            {
                if (sanPham.maSP == maSP)
                {
                    return true;
                }
            }
            return false;
        }
        public void ThemSanPham(bool displayMenu = true)
        {
            Console.Clear();
            CSanPham sp = new CSanPham();
            sp.NhapSP(list);
            while (KiemTrungMaSanPham(sp.maSP))
            {
                Console.WriteLine("Ma san pham da ton tai. Vui long nhap ma khac.");
                sp.NhapSP(list);
            }
            list.Add(sp);
            GhiDanhSachSanPham("danh_sach_san_pham.txt", false);// Ghi danh sach san pham sau khi them
            Console.WriteLine("San pham da duoc them");
            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void XoaSanPham(bool displayMenu = true)
        {
            Console.Clear();
            XuatDanhSachSanPham();
            Console.WriteLine("Chon ma san pham muon xoa");
            string maSP = Console.ReadLine();
            CSanPham sanPham = list.Find(sp => sp.maSP == maSP);
            if (sanPham != null)
            {
                Console.WriteLine("Ban co chac chan muon xoa san pham nay? (y/n)");
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "y")
                {
                    list.Remove(sanPham);
                    Console.WriteLine("San pham da bi xoa khoi danh sach.");
                    GhiDanhSachSanPham("danh_sach_san_pham.txt", false);
                }
                else
                {
                    Console.WriteLine("San pham khong bi xoa.");
                }
            }
            else
            {
                Console.WriteLine("Khong tim thay san pham co ma: " + maSP);
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void SuaSanPham(bool displayMenu = true)
        {
            Console.Clear();
            XuatDanhSachSanPham();
            Console.WriteLine("Chon ma san pham muon sua");
            string maSP = Console.ReadLine();
            CSanPham sanPham = list.Find(sp => sp.maSP == maSP);
            if (sanPham != null)
            {
                string spCu = sanPham.tenSP;
                double giaspCu = sanPham.gia;
                int slspcu = sanPham.soLuong;
                PhanLoai plcu = sanPham.phanLoai;
                Console.WriteLine("Nhap thong tin moi cho san pham:");
                Console.Write("Nhap ten san pham: ");
                sanPham.tenSP = Console.ReadLine();
                if (sanPham.tenSP == "")
                        sanPham.tenSP=spCu;
                do
                {
                    Console.Write("Nhap gia san pham moi (nhap -0 neu khong muon sua): ");
                    string giaStr = Console.ReadLine();
                    sanPham.gia = double.Parse(giaStr);
                    if (sanPham.gia < 0)
                    {
                        Console.Write("Gia san pham phai lon hon hoac bang 0. Vui long nhap lai gia san pham: ");
                        sanPham.gia = double.Parse(giaStr);
                    }
                    if (sanPham.gia == -0)
                        sanPham.gia = giaspCu;
                    Console.Write("Nhap so luong san pham (nhap -0 neu khong muon sua): ");
                    string soLuongStr = Console.ReadLine();
                    sanPham.soLuong = int.Parse(soLuongStr);
                    if (sanPham.soLuong == -0)
                        sanPham.soLuong = slspcu;
                } while (sanPham.gia < 0 && sanPham.gia != -0 && sanPham.soLuong <0 &&sanPham.soLuong != -0);
                do
                {
                    Console.Write("Nhap phan loai san pham (nhap khongdoi neu khong muon doi): ");
                    string phanLoaiStr = Console.ReadLine().ToLower();
                    if (!int.TryParse(phanLoaiStr, out _) && Enum.TryParse(phanLoaiStr, true, out PhanLoai parsedPhanLoai))
                    {
                        sanPham.phanLoai = parsedPhanLoai;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Phan loai khong hop le. Vui long nhap lai.");
                    }
                } while (true);
                if (sanPham.phanLoai == PhanLoai.KhongDoi)
                    sanPham.phanLoai = plcu;
                GhiDanhSachSanPham("danh_sach_san_pham.txt", false);
                Console.WriteLine("San pham da đuoc cap nhat.");
            }
            else
            {
                Console.WriteLine("Khong tim thay san pham co ma: " + maSP);
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void TimKiemSPTheoMa(bool displayMenu = true)
        {
            Console.Clear();
            Console.WriteLine("Nhap ma san pham muon tim");
            string maSP = Console.ReadLine();
            CSanPham sanPham = list.Find(sp => sp.maSP == maSP);
            if (sanPham != null)
            {
                Console.WriteLine("San pham đuoc tim thay:");
                sanPham.XuatThongTinSP();
            }
            else
            {
                Console.WriteLine("Khong tim thay san pham co ma: " + maSP);
            }

            if (displayMenu)
            {
                Console.WriteLine("Nhan Enter de tiep tuc...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public bool GhiDanhSachSanPham(string tenTapTin, bool displayMenu = true)
        {

            try
            {
                /* dung using de tao 1 khoi voi streamWriter.khi ket thuc using thi du lieu cua streamWriter 
                 se tu dong giai phong qua phuong thuc close mà khong can dung noiDung.Close()*/
                using (StreamWriter noiDung = new StreamWriter(tenTapTin))
                {
                    noiDung.WriteLine("San pham thu\tMa SP\tTen san pham\tGia\tSo luong\tPhan loai");
                    int i = 1;
                    foreach (CSanPham sp in list)
                    {
                        noiDung.WriteLine(i + "\t\t  " + sp.maSP + "\t     " + sp.tenSP + "\t         " + sp.gia + "\t   " + sp.soLuong + "\t           " + sp.phanLoai);
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
        public void CapNhatSoLuongSanPhamTrongFile(string maSP, int soLuongDaBan)
        {
            string path = "danh_sach_san_pham.txt";
            string[] lines = File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] spInfo = lines[i].Split('\t');
                if (spInfo.Length >= 7 && spInfo[2].Trim() == maSP)
                {
                    int soLuongHienTai = int.Parse(spInfo[5].Trim());
                    int soLuongConLai = soLuongHienTai - soLuongDaBan;

                    if (soLuongConLai < 0)
                    {
                        Console.WriteLine($"Khong du so luong cho san pham {maSP} trong kho.");
                        return;
                    }

                    lines[i] = $"{i}\t\t{spInfo[2]}\t{spInfo[3]}\t{spInfo[4]}\t{soLuongConLai}\t{spInfo[6]}";
                    File.WriteAllLines(path, lines);

                    Console.WriteLine($"Da cap nhat so luong san pham {maSP} trong file danh sach san pham.");
                    return;
                }
            }

            Console.WriteLine($"Khong tim thay san pham co ma {maSP} trong file danh sach san pham.");
        }

        public void CapNhatSoLuongSanPhamTrongFileSauKhiSuaHoaDon(string maSP, int soluong, int soLuongDaBan)
        {
            string path = "danh_sach_san_pham.txt";
            string[] lines = File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] spInfo = lines[i].Split('\t');
                if (spInfo.Length >= 7 && spInfo[2].Trim() == maSP)
                {
                    int soLuongHienTai = int.Parse(spInfo[5].Trim());
                    int soluongconglaidesua = soLuongHienTai + soluong;
                    int soLuongConLai = soluongconglaidesua - soLuongDaBan;

                    if (soLuongConLai < 0)
                    {
                        Console.WriteLine($"Khong du so luong cho san pham {maSP} trong kho.");
                        return;
                    }

                    lines[i] = $"{i}\t\t{spInfo[2]}\t{spInfo[3]}\t{spInfo[4]}\t{soLuongConLai}\t{spInfo[6]}";
                    File.WriteAllLines(path, lines);

                    Console.WriteLine($"Da cap nhat so luong san pham {maSP} trong file danh sach san pham.");
                    return;
                }
            }

            Console.WriteLine($"Khong tim thay san pham co ma {maSP} trong file danh sach san pham.");
        }
        public void DocDanhSachSanPhamTuTapTin(string tenTapTin, bool displayMenu = true)
        {
            Console.Clear();
            List<CSanPham> tempList = new List<CSanPham>(); // Danh sach tam thoi
            try
            {
                list.Clear();
                using (StreamReader noiDung = new StreamReader(tenTapTin))
                {
                    string line;
                    while ((line = noiDung.ReadLine()) != null)
                    {
                        //1,2,3,4,5 lan luot la maSP,tenSP,gia,so luong,phan loai
                        string[] tokens = line.Split('\t'); //tach dong thanh cac phan rieng biet
                        if (tokens.Length >= 7) //kiem tra mang co it nhat 7 phan tu. Neu it hon bo qua vi chua thong tin san pham khong hop le
                        //vi co 6 phan tu (stt,maSP,tenSP,gia,so luong,phan loai) nen tokens,Lenght phai >=7
                        {
                            /*kiem tra 3 va 4 co chuyen thanh long va int duoc khong
                            neu duoc thi tao 1 CSanPham them vao dssp*/
                            if (double.TryParse(tokens[4].Trim(), out double gia) && int.TryParse(tokens[5].Trim(), out int soLuong) && Enum.TryParse(tokens[6].Trim(), true, out PhanLoai phanLoai))
                            {
                                tempList.Add(new CSanPham(tokens[2].Trim(), tokens[3].Trim(), gia, soLuong, phanLoai));
                            }
                        }
                    }
                }
                // Sau khi đoc tu tap tin, gan danh sach tam thoi cho danh sach chinh
                list.AddRange(tempList);

                //CBanHang.danhsachsp.
            }
            catch
            {
                Console.WriteLine("Loi khi doc du lieu tu file");
            }
        }
        public void MenuSanPham()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("_______________DANH SACH SAN PHAM_______________");
                Console.WriteLine("| 0. Thoat                                     |");
                Console.WriteLine("| 1. Nhap danh sach san pham                   |");
                Console.WriteLine("| 2. Xuat danh sach san pham                   |");
                Console.WriteLine("| 3. Them san pham vao danh sach san pham      |");
                Console.WriteLine("| 4. Xoa san pham ra khoi danh sach san pham   |");
                Console.WriteLine("| 5. Sua san pham                              |");
                Console.WriteLine("| 6. Tim kiem san pham                         |");
                Console.WriteLine("|______________________________________________|");
                Console.WriteLine("\nChon chuc nang");
                int chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 0:
                        Console.Clear();
                        return;
                    case 1:
                        TaoDSSanPham();
                        break;
                    case 2:
                        XuatDanhSachSanPham();
                        break;
                    case 3:
                        ThemSanPham();
                        break;
                    case 4:
                        XoaSanPham();
                        break;
                    case 5:
                        SuaSanPham();
                        break;
                    case 6:
                        TimKiemSPTheoMa();
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