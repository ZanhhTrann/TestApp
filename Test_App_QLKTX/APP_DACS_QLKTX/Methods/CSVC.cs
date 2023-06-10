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
    internal class CSVC : Methods
    {

        public CSVC(SqlConnection conn, String sql0, TextBox search)
        {
            this.conn = conn;
            this.sql0 = sql0;
            this.search = search;
        }
        public DataTable Check_data()
        {
            DataTable dt = new DataTable();
            String sql = sql0;
            if (search.Text != "")
            {
                sql = sql + " where MaCSVC='" + search.Text + "'";
            }
            dt = Load_data(sql);
            return dt;
        }
        //
        public void Print_File( DataTable dt)
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
                    String[] arrColumn = { "MaCSVC",
                                            "TenCSVC"};

                    //lấy ra số lượng cột cần dùng phụ thuộc vào arrColumn
                    var CountArrColumn = arrColumn.Count();

                    ws.Cells[1, 1].Value = "Danh Sách Cơ Sở Vật Chất Ký Túc Xá";
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
                        ws.Cells[rowIndex, colIndex++].Value = row["MaCSVC"].ToString();
                        ws.Cells[rowIndex, colIndex++].Value = row["TenCSVC"].ToString();

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
