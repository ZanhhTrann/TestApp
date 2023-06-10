using APP_DACS_QLKTX.Methods;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Printing;
namespace APP_DACS_QLKTX.Views
{
    /// <summary>
    /// Interaction logic for Print_Bill.xaml
    /// </summary>
    /// 
    public partial class Print_Bill : Window
    {
        private HoaDon hd;
        public Print_Bill(SqlConnection conn, String sql0, TextBox search, RadioButton r1, RadioButton r2,String id)
        {
            int slsv, d, n, pt, dt, nt, mt, vst, ttt;
            hd = new HoaDon(conn,sql0,search,r1,r2);
            InitializeComponent();
            mhd.Text = search.Text;
            nhd.Text = hd.Get_Value("select NgayLapHD from HoaDon Where MaHD='" + search.Text + "'");
            mp.Text = hd.Get_Value("select MaPhong from HoaDon Where MaHD='" + search.Text + "'");
            slsv = int.Parse(hd.Get_Value("select SLSV from Phong_KTX Where MaPhong in (select MaPhong from HoaDon Where MaHD='" + search.Text + "')"));
            d = int.Parse(hd.Get_Value("select (SDC-SDD) from HoaDon Where MaHD='" + search.Text + "'"));
            n = int.Parse(hd.Get_Value("select (SNC-SND) from HoaDon Where MaHD='" + search.Text + "'"));
            pt = int.Parse(hd.Get_Value("select PhiDV from DonGiaDichVu Where MaDV='DV05'"));
            dt = int.Parse(hd.Get_Value("select PhiDV from DonGiaDichVu Where MaDV='DV01'"));
            nt = int.Parse(hd.Get_Value("select PhiDV from DonGiaDichVu Where MaDV='DV02'"));
            mt = int.Parse(hd.Get_Value("select PhiDV from DonGiaDichVu Where MaDV='DV03'"));
            vst = int.Parse(hd.Get_Value("select PhiDV from DonGiaDichVu Where MaDV='DV04'"));
            //
            tp.Text = hd.changeM((pt).ToString()) + " VND";
            td.Text = hd.changeM((dt).ToString()) + " VND";
            tn.Text = hd.changeM((nt).ToString()) + " VND";
            tm.Text= hd.changeM((mt).ToString()) + " VND";
            tvs.Text = hd.changeM((vst).ToString()) + " VND";
            kwh.Text = d.ToString() + " Kwh";
            khoi.Text = n.ToString() + " Khối";
            slsv0.Text = slsv1.Text = slsv2.Text = slsv3.Text = slsv.ToString()+" Sinh viên";
            //
            ttp.Text = hd.changeM((slsv * pt).ToString()) + " VND";
            ttd.Text = hd.changeM((d * dt).ToString()) + " VND";
            ttn.Text = hd.changeM((n * nt).ToString()) + " VND";
            ttm.Text = hd.changeM((slsv * mt).ToString()) + " VND";
            ttvs.Text = hd.changeM((slsv * vst).ToString()) + " VND";
            //
            ttt = slsv * (pt + mt + vst) + d * dt + n * nt;
            tt.Text = hd.changeM(ttt.ToString()) + " VND";
            String ten = "";
            String ht = "";
            ht= hd.Get_Value("select HoTen From Account Where id='" + id + "'");
            for(int i=ht.Length-1; i>=0; i--)
            {
                if (ht[i]!=' ')
                {
                    ten = ht[i] + ten;
                }
                else
                {
                    break;
                }
            }
            fullname.Text = ht;
            name.Text = ten;

        }


        private void click_InHoaDon(object sender, RoutedEventArgs e)
        {
            try
            {
                btnInHoaDon.Visibility = Visibility.Hidden;
                ex.Visibility = Visibility.Hidden;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "In Hóa đơn");
                    MessageBox.Show("In hóa đơn thành công");
                }
                else
                {
                    printDialog.PrintVisual(print, "In Hóa đơn");
                    MessageBox.Show("In hóa đơn Không thành công");
                }
            }
            catch
            {
                MessageBox.Show("In hóa đơn không thanh công");
            }
            finally
            {
                btnInHoaDon.Visibility = Visibility.Visible;
                ex.Visibility = Visibility.Visible;
            }
        }

        private void ex_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
