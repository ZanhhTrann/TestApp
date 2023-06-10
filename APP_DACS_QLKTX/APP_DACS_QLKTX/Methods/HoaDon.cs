using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
namespace APP_DACS_QLKTX.Methods
{
    internal class HoaDon : Methods
    {
        private RadioButton r1;
        private RadioButton r2;
        public HoaDon(SqlConnection conn, String sql0, TextBox search, RadioButton r1, RadioButton r2)
        {
            this.conn = conn;
            this.sql0 = sql0;
            this.search = search;
            this.r1 = r1;
            this.r2 = r2;
        }
        public DataTable Check_data()
        {
            DataTable dt = new DataTable();
            String sql = sql0;
            if (search.Text != "")
            {
                if (r1.IsChecked == true)
                {
                    sql = sql + " where (MaHD= '" + search.Text + "' or MaPhong= '" + search.Text + "') and  TinhTrang!='Chưa thanh toán'";
                }
                else if (r2.IsChecked == true)
                {
                    sql = sql + " where (MaHD= '" + search.Text + "' or MaPhong= '" + search.Text + "') and  TinhTrang='Chưa thanh toán'";
                }
                else
                {
                    sql = sql + " where (MaHD= '" + search.Text + "' or MaPhong= '" + search.Text + "')";
                }
            }
            else
            {
                if (r1.IsChecked == true)
                {
                    sql = sql + " where TinhTrang!='Chưa thanh toán'";
                }
                else if (r2.IsChecked == true)
                {
                    sql = sql + " where TinhTrang='Chưa thanh toán'";
                }
            }
            dt = Load_data(sql);
            return dt;
        }

        //

        public int[] Get_Value_Month(String hsql, String esql, String dg,String year,String room)
        {

            String val = "select PhiDV from DonGiaDichVu Where MaDV='" + dg + "'";
            String c = "SELECT Sum(" + esql + "-" + hsql + ") FROM HoaDon where Year(NgayLapHD)="+int.Parse(year);
            if (room != "")
            {
                c = c + " And MaPhong='"+room+"' And MONTH(NgayLapHD)=";
            }
            else
            {
                c = c + " And MONTH(NgayLapHD)=";
            }
            int[] value = new int[12];
            try
            {
                for(int i = 0; i < 12; i++)
                {
                    if (Get_Value(c+(i+1))!="")
                    {
                       value[i] = int.Parse(Get_Value(c+(i+1))) * int.Parse(Get_Value(val));
                    }
                    else
                    {
                        value[i] = 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return value;
        }
        public int[] Get_Value_Month_Total( String year,String room)
        {
            int[] s = new int[12];
            int[] td = Get_Value_Month("SDD", "SDC", "DV01",year,room);
            int[] tn = Get_Value_Month("SND", "SNC", "DV02", year,room);
            String sql1 = "select PhiDV from DonGiaDichVu where MaDV='DV03'";
            String sql2 = "select PhiDV from DonGiaDichVu where MaDV='DV04'";
            String sql3 = "select PhiDV from DonGiaDichVu where MaDV='DV05'";
            String sql4 = "";
            if (room != "")
            {
                sql4="select SLSV from Phong_KTX where MaPhong='"+room+"'";
            }
            else
            {
                sql4 = "select count(*) from SinhVien";
            }
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    if ((td[i] == 0))
                    {
                        s[i] = 0;
                    }
                    else
                    {
                        s[i] = td[i] + tn[i] + (int.Parse(Get_Value(sql1)) + int.Parse(Get_Value(sql2)) + int.Parse(Get_Value(sql3)))*int.Parse(Get_Value(sql4));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return s;
        }
        public String changeM(String m)
        {
            String nm = "";
            int count = -1;
            for(int i = m.Length-1; i>=0; i--)
            {
                count++;
                if (count < 3)
                {
                    nm = m[i] + nm;
                }
                else
                {
                    count = 0;
                    nm = m[i]+ "." + nm;
                }
            }
            return nm;
        }
        //
        public void Print_File(DataTable dt)
        {
            String filePath = "";
            //tao savedialog de luu file
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel| *.xlsx| Excel 2003| *.xls";

            //nếu mở thành công thì lưu đường dẫn lại để dùng
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }
            // nếu không thì báo lỗi 
            if (String.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn không hợp lệ!!");
                return;
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    //đặt tên người tạo file 
                    p.Workbook.Properties.Author = "Trần Công Danh,Nguyễn Văn Tân,Dương Văn Quang";
                    //Đặt tiêu đề
                    p.Workbook.Properties.Title = "Báo cáo thông kê";
                    //tạo 1 sheet để làm việc
                    p.Workbook.Worksheets.Add("Test Sheet");
                    //Lấy sheet ra để làm việc
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    //Đặt tên cho sheet
                    ws.Name = "Test Sheet";
                    // Đặt formstyle 
                    ws.Cells.Style.Font.Size = 14;
                    // Đặt forn
                    ws.Cells.Style.Font.Name = "Times New Roman";

                    //Tạo danh sách các column 
                    String[] arrColumn = { "MaHD",
                                            "MaPhong",
                                            "SDD",
                                            "SDC",
                                            "SND",
                                            "SNC",
                                            "NgayLapHD",
                                            "TinhTrang"};

                    //lấy ra số lượng cột cần dùng phụ thuộc vào arrColumn
                    var CountArrColumn = arrColumn.Count();

                    ws.Cells[1, 1].Value = "Danh Sách Hóa Đơn Ký Túc Xá";
                    ws.Cells[1, 1, 1, CountArrColumn].Merge = true;
                    //in đậm
                    ws.Cells[1, 1, 1, CountArrColumn].Style.Font.Bold = true;
                    // Căn Giũa
                    ws.Cells[1, 1, 1, CountArrColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    int colIndex = 1;
                    int rowIndex = 2;
                    //Tạo các header từ column đã tạo
                    foreach (var item in arrColumn)
                    {
                        var cells = ws.Cells[rowIndex, colIndex];
                        var Border = cells.Style.Border;
                        Border.Bottom.Style =
                            Border.Top.Style =
                                Border.Left.Style =
                                    Border.Right.Style = ExcelBorderStyle.Thin;

                        //Gán giá trị
                        cells.Value = item;
                        colIndex++;

                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        //bắt đầu từ cột 1
                        colIndex = 1;
                        //rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;
                        ws.Cells[rowIndex, colIndex++].Value = row["MaHD"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["MaPhong"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["SDD"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["SDC"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["SND"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["SNC"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["NgayLapHD"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["TinhTrang"].ToString();
                    }
                    // Lưu file lại
                    byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);

                }
                MessageBox.Show("Lưu thành công!");
            }
            catch
            {
                MessageBox.Show("Lỗi khi lưu!!");
                return;
            }


        }
    }
}
