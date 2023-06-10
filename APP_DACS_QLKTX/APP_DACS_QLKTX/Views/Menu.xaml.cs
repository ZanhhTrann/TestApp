using APP_DACS_QLKTX.Methods;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace APP_DACS_QLKTX.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private String id;
        private String sqlconn = @"Data Source=ZANHH\SQLEXPRESS;Initial Catalog=DACS_QLKTX;Integrated Security=True";
        private String sql0 = "select * from SinhVien";
        private String sql1 = "select * from Khoa";
        private String sql2 = "select * from Toa_KTX";
        private String sql3 = "select * from Phong_KTX";
        private String sql4 = "Select * from HopDong";
        private String sql5 = "select * from HoaDon";
        private String sql6 = "select * from CoSoVatChat";
        private String sql7 = "select * from DonGiaDichVu";
        private String sql8 = "select * from Phong_CSVC";
        private String sql9 = "select * from HoaDon_DonGia";
        private String year = "2022";
        private String sqldn1 = "select Count(*) from SinhVien Where SLKyLuat=";
        private String sqldn2 = "select count(*) from Phong_KTX where SLSV";
        private String sqldn3 = "select count(*) from HoaDon";
        private String sqldn4 = "select count(*) from SinhVien Where";
        private String room = "";
        private int check = 0;
        private int check_sv = 0;
        private int check_k = 0;
        private int check_tktx = 0;
        private int check_pktx = 0;
        private int check_csvc = 0;
        private int check_dgdv = 0;
        private int check_pcsvc = 0;
        private int check_hddg = 0;
        private int check_hopd = 0;
        private int check_hoad = 0;
        private int check_tk = 0;
        //int check_change_sv = 0;
        private SinhVien sv;
        private Khoa k;
        private Toa_KTX tktx;
        private Phong_KTX pktx;
        private HopDong hopd;
        private HoaDon hoad;
        private CSVC csvc;
        private DGDV dgdv;
        private SqlConnection conn;
        private DataRowView dataRow;
        private Phong_CSVC pcsvc;
        private HoaDonDonGia hddg;
        public Menu(String id)
        {
            this.id = id;
            InitializeComponent();
            conn = new SqlConnection(sqlconn);
            sv = new SinhVien(conn, sql0, sv_search, sv_hhhd, sv_vpkl);
            k = new Khoa(conn, sql1, k_search);
            tktx = new Toa_KTX(conn, sql2, tktx_search, tktx_fixing);
            pktx = new Phong_KTX(conn, sql3, pktx_search, pktx_pt, pktx_tsv, pktx_fixing);
            hopd = new HopDong(conn, sql4, hopd_search, hopd_hhhd);
            hoad = new HoaDon(conn, sql5, hoad_search, hoad_dtt, hoad_ctt);
            dgdv = new DGDV(conn, sql7, dgdv_search);
            csvc = new CSVC(conn, sql6, csvc_search);
            pcsvc = new Phong_CSVC(conn, sql8, pcsvc_search, pcsvc_dhh);
            hddg = new HoaDonDonGia(conn, sql9, hddg_search);
            //
            conn.Open();
            sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
            k_data.ItemsSource = k.Load_data(sql1).DefaultView;
            tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
            pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
            hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
            hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
            csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
            dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
            pcsvc_data.ItemsSource = pcsvc.Load_data(sql8).DefaultView;
            hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
            //
            ud_sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
            ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;
            ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
            ud_pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
            ud_hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
            ud_hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
            ud_csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
            ud_dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
            ud_pcsvc_data.ItemsSource = pcsvc.Load_data(sql8).DefaultView;
            ud_hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
            //
            tbl_name.Text = sv.Get_Value("select HoTen from Account Where ID='" + id + "'");
            tbl_SDT.Text = sv.Get_Value("select SDT from Account Where ID='" + id + "'");
            tbl_dob.Text = sv.Get_Value("select NgaySinh from Account Where ID='" + id + "'");
            tbl_dc.Text = sv.Get_Value("select DiaChi from Account Where ID='" + id + "'");
            SeriesCollection = new SeriesCollection();
            SeriesCollection1 = new SeriesCollection();
            SeriesCollection2 = new SeriesCollection();
            SeriesCollection3 = new SeriesCollection();
            SeriesCollection4 = new SeriesCollection();
            DataContext = this;

            try
            {
                runliv();
            }
            catch
            {

            }
        }

        public void runliv()
        {
            sd_donat();
            sd_donat2();
            sd_donat3();
            sd_donat4();
            sd_line();
        }
        public void sd_donat()
        {
            s0.Text = sv.Get_Value(sqldn1 + 0);
            s1.Text = sv.Get_Value(sqldn1 + 1);
            s2.Text = sv.Get_Value(sqldn1 + 2);
            s3.Text = sv.Get_Value(sqldn1 + 3);
            this.SeriesCollection.Clear();
            this.SeriesCollection.Add(
                new PieSeries
                {
                    Title = "0",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(sv.Get_Value(sqldn1 + 0))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection.Add(
                new PieSeries
                {
                    Title = "3",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(sv.Get_Value(sqldn1 + 3))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection.Add(
                new PieSeries
                {
                    Title = "2",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(sv.Get_Value(sqldn1 + 2))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection.Add(
                new PieSeries
                {
                    Title = "1",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(sv.Get_Value(sqldn1 + 1))) },
                    DataLabels = true
                }
                );
        }

        public void sd_donat2()
        {
            tk_room_csd.Text = pktx.Get_Value(sqldn2 + "=0");
            tk_room_dsd.Text = pktx.Get_Value(sqldn2 + ">0");
            tk_room_full.Text = pktx.Get_Value(sqldn2 + "=8");
            tk_room_sc.Text = pktx.Get_Value("select count(*) from Phong_KTX where TinhTrang!=''");
            this.SeriesCollection2.Clear();
            this.SeriesCollection2.Add(
                new PieSeries
                {
                    Title="đã sử dụng",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(pktx.Get_Value(sqldn2 + ">0"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection2.Add(
                new PieSeries
                {   
                    Title="Chưa sử dụng",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(pktx.Get_Value(sqldn2 +"=0"))) },
                    DataLabels = true
                }
                );
        }

        public void sd_donat3()
        {
            tk_bill_dtt.Text = hoad.Get_Value(sqldn3 + " where TinhTrang=N'Đã thanh toán'");
            tk_bill_ctt.Text = hoad.Get_Value(sqldn3 + " where TinhTrang!=N'Đã thanh toán'");
            tk_bill_tt.Text = hoad.Get_Value(sqldn3);
            this.SeriesCollection3.Clear();
            this.SeriesCollection3.Add(
                new PieSeries
                {
                    Title = "Đã thanh toán",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn3 + " where TinhTrang=N'Đã Thanh Toán'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection3.Add(
                new PieSeries
                {
                    Title = "Chưa thanh toán",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn3 + " where TinhTrang!=N'Đã Thanh Toán'"))) },
                    DataLabels = true
                }
                );
        }

        public void sd_donat4()
        {
            tk_tt_csvc.Text = csvc.Get_Value("select count(*) from CoSoVatChat");
            tk_tt_dv.Text = dgdv.Get_Value("select count(*) from DonGiaDichVu");
            tk_tt_hd.Text = hopd.Get_Value("select count(*) from HopDong");
            tk_tt_k.Text = k.Get_Value("select count(*) from Khoa");
            tk_tt_room.Text = pktx.Get_Value("select count(*) from Phong_KTX");
            tk_tt_sv.Text = sv.Get_Value("select count(*) from SinhVien");
            String d = DateTime.Now.ToString("yyyy/MM/dd");
            tk_tt_svhhhd.Text = hopd.Get_Value("select count(*) from HopDong where MaSV in ( select MaSV from HopDong where '" + d + "'>HanHD)");
            tk_tt_svchd.Text = hopd.Get_Value("select count(*) from HopDong where MaSV not in ( select MaSV from HopDong where '" + d + "'>HanHD)");
            tk_tt_t.Text = tktx.Get_Value("select count(*) from Toa_KTX");
            this.SeriesCollection4.Clear();
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Công nghệ thông tin",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " MaKhoa='cntt'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Dược",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " Makhoa='dl'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Kỹ thuật oto",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " MaKhoa='ktot'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Ngôn ngữ",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " MaKhoa='NN'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Quản trị kinh doanh",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " MaKhoa='qtkd'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Du lịch",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " MaKhoa='dul'"))) },
                    DataLabels = true
                }
                );
            this.SeriesCollection4.Add(
                new PieSeries
                {
                    Title = "Điện điện tử",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(int.Parse(hoad.Get_Value(sqldn4 + " MaKhoa='DDT'"))) },
                    DataLabels = true
                }
                );
        }

        public void sd_line() {
            int[] tn = hoad.Get_Value_Month("SND", "SNC", "DV02", year,room);
            int[] td = hoad.Get_Value_Month("SDD", "SDC", "DV01", year,room);
            int[] tt = hoad.Get_Value_Month_Total(year,room);
            int count = 0;
            int m1 = td[0], m2 = tn[0], m3 = tt[0];
            int t1=0, t2=0, t3 = 0;
            for(int i=0;i<12;i++)
            {
                if ((tn[i] != 0) && (td[i] != 0))
                {
                    count++;
                    t1 += td[i];
                    t2 += tn[i];
                    t3 += tt[i];
                    if (m1 < td[i])
                    {
                        m1 = td[i];
                    }
                    if (m2 < tn[i])
                    {
                        m2 = tn[i];
                    }
                    if(m3 <tt[i])
                    {
                        m3 = tt[i];
                    }
                }
            }
            if(count==0) count = 1;
            tkt0.Text = hoad.changeM(((double)t1 / count).ToString()) +" VNĐ";
            tkt1.Text = hoad.changeM(((double)t2 / count).ToString()) + " VNĐ";
            tkt2.Text = hoad.changeM(((double)t3 / count).ToString()) + " VNĐ";
            max0.Text = hoad.changeM(((double)m1).ToString()) + " VNĐ";
            max1.Text = hoad.changeM(((double)m2).ToString()) + " VNĐ";
            max2.Text = hoad.changeM(((double)m3).ToString()) + " VNĐ";
            this.SeriesCollection1.Clear();
            this.SeriesCollection1.Add(
                new LineSeries
                {
                    Title = "Tiền Điện",
                    Values = new ChartValues<int> { td[0], td[1],td[2], td[3], td[4], td[5], td[6], td[7], td[8], td[9], td[10], td[11] }
                }
                );
            this.SeriesCollection1.Add(
               new LineSeries
               {
                   Title = "Tiền Nước",
                   Values = new ChartValues<int> { tn[0], tn[1], tn[2], tn[3], tn[4], tn[5], tn[6], tn[7], tn[8], tn[9], tn[10], tn[11] }
               }
               );
            this.SeriesCollection1.Add(
               new LineSeries
               {
                   Title = "Tổng",
                   Values = new ChartValues<int> { tt[0], tt[1], tt[2], tt[3], tt[4], tt[5], tt[6], tt[7], tt[8], tt[9], tt[10], tt[11] }
               }
               );
            

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            YFormatter = value => value.ToString("C");
        
        }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public SeriesCollection SeriesCollection3 { get; set; }
        public SeriesCollection SeriesCollection4 { get; set; }

        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        // tìm kiếm thông tin sinh viên dựa theo khóa chính đồng thời dựa theo hai checkbox
        private void sv_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            sv_data.ItemsSource = sv.Check_data().DefaultView;
        }
        // load dữ liệu dựa trên vi phạm kỷ luật ( các đối tượng có số lần vi phạm lớn hơn 1) 
        private void sv_vpkl_Checked(object sender, RoutedEventArgs e)
        {
            sv_data.ItemsSource = sv.Check_data().DefaultView;
        }
        // load dữ liệu dựa theo hạn hợp đồng
        private void sv_hhhd_Checked(object sender, RoutedEventArgs e)
        {
            sv_data.ItemsSource = sv.Check_data().DefaultView;
        }
        // Load lại dữ liệu về trạng thái ban đầu và bỏ chọn hai checkbox
        private void sv_reload_Click(object sender, RoutedEventArgs e)
        {
            sv_hhhd.IsChecked = false;
            sv_vpkl.IsChecked = false;
            sv_search.Text = "";
            sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
        }
        // Tìm kiếm khoa dựa vào khóa chính (Mã khoa)
        private void k_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            k_data.ItemsSource = k.Check_data().DefaultView;
        }
        //tìm kiếm tòa ktx đang ở trạng thái sửa chữa
        private void tktx_fixing_Checked(object sender, RoutedEventArgs e)
        {
            tktx_data.ItemsSource = tktx.Check_data().DefaultView;
        }

        //load lại dữ liệu về trạng thái ban đầu
        private void tktx_reload_Click(object sender, RoutedEventArgs e)
        {
            tktx_fixing.IsChecked = false;
            tktx_search.Text = "";
            tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
        }

        //tìm kiếm thông tin về tòa ktx dựa vào khóa chính và checkbox kiểm tra tình trạng của tòa ktx
        private void tkxt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            tktx_data.ItemsSource = tktx.Check_data().DefaultView;

        }
        // tải lại dữ liệu về trạng thái ban đầu 
        private void pktx_reload_Click_1(object sender, RoutedEventArgs e)
        {
            pktx_tsv.IsChecked = false;
            pktx_pt.IsChecked = false;
            pktx_fixing.IsChecked = false;
            pktx_search.Text = "";
            pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
        }
        //tìm kiếm phòng dựa vào mã phòng và các radiobox hoặc checkbox để truy xuất thông tin về phòng ktx
        private void pktx_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            pktx_data.ItemsSource = pktx.Check_data().DefaultView;
        }
        // tìm kiếm phòng có đồ đang bị hỏng
        private void pktx_fixing_Checked(object sender, RoutedEventArgs e)
        {
            pktx_data.ItemsSource = pktx.Check_data().DefaultView;
        }
        //kiểm tra danh sách xem có phòng nào chưa có sinh viên ở hay không
        private void pktx_pt_Checked(object sender, RoutedEventArgs e)
        {
            pktx_data.ItemsSource = pktx.Check_data().DefaultView;
        }
        //kiểm tra xem có phòng nào số lượng sinh viên đang ở chưa đạt tối đa
        private void pktx_tsv_Checked(object sender, RoutedEventArgs e)
        {
            pktx_data.ItemsSource = pktx.Check_data().DefaultView;
        }
        //tìm kiếm thông tin hợp đồng dự vào khóa chính và khóa phụ cùng các điều kiện 
        private void hopd_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            hopd_data.ItemsSource = hopd.Check_data().DefaultView;
        }
        // tìm kiếm các hợp đồng đã bị hết hạn
        private void hopd_hhhd_Checked(object sender, RoutedEventArgs e)
        {
            hopd_data.ItemsSource = hopd.Check_data().DefaultView;
        }
        //load lại dự liệu về trạng thái ban đầu
        private void hopd_reload_Click(object sender, RoutedEventArgs e)
        {
            hopd_hhhd.IsChecked = false;
            hopd_search.Text = "";
            hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
        }
        //load lại dự liệu của hóa đơn về trạng thái ban đầu
        private void hoad_reload_Click(object sender, RoutedEventArgs e)
        {
            hoad_ctt.IsChecked = false;
            hoad_dtt.IsChecked = false;
            hoad_search.Text = "";
            hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
        }
        // tìm kiếm thông tin hóa đơn dựa vào khóa chính và khóa phụ
        private void hoad_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            hoad_data.ItemsSource = hoad.Check_data().DefaultView;
        }
        // tìm kiếm thông tin hóa đơn đã thanh toán 
        private void hoad_dtt_Checked(object sender, RoutedEventArgs e)
        {
            hoad_data.ItemsSource = hoad.Check_data().DefaultView;
        }
        // tìm kiếm thông tin hóa đơn chưa thanh toán
        private void hoad_ctt_Checked(object sender, RoutedEventArgs e)
        {
            hoad_data.ItemsSource = hoad.Check_data().DefaultView;
        }

        private void csvc_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            csvc_data.ItemsSource = csvc.Check_data().DefaultView;
        }

        private void dgdv_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgdv_data.ItemsSource = dgdv.Check_data().DefaultView;
        }

        private void bt_logout_Click(object sender, RoutedEventArgs e)
        {
            conn.Close();
            Login lg = new Login();
            this.Close();
            lg.Show();
            //sv.Check_ID("");
        }

        

        private void ud_sv_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "insert into SinhVien values ('" + ud_sv_msv.Text + "','" + ud_sv_name.Text + "','" + ud_sv_dob.Text + "','" + ud_sv_g.Text + "','" + ud_sv_dc.Text + "','" + ud_sv_sdt.Text + "','" + ud_sv_mk.Text + "','" + ud_sv_pname.Text + "','" + ud_sv_sdt_p.Text + "','" + ud_sv_qh.Text + "','" + ud_sv_slkl.Text + "')";
                if (sv.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo!");
                    sql = "select * from SinhVien where MaSV='" + ud_sv_msv.Text + "'";
                    ud_sv_data.ItemsSource = sv.Load_data(sql).DefaultView;
                    ud_sv_delete.IsEnabled = true;
                    ud_sv_add.IsEnabled = false;
                    check_sv = 1;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên không thành công!", "Thông báo!");
                }
            }
            catch ( Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_sv_msv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_sv_msv.Text != "")
                    {
                        if (sv.Check_ID("SinhVien where MaSV='" + ud_sv_msv.Text + "'") == true)
                        {
                            ud_sv_add.IsEnabled = false;
                            ud_sv_change.IsEnabled = true;
                            ud_sv_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_sv_add.IsEnabled = true;
                            ud_sv_change.IsEnabled = false;
                            ud_sv_delete.IsEnabled = false;

                        }
                        ud_sv_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_sv_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
            
        }

        private void ud_sv_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_sv_search.Text != "")
                {
                    if (sv.Check_ID("SinhVien where MaSV='" + ud_sv_search.Text + "'") == true)
                    {   
                        String sql="select * from SinhVien where MaSV='"+ud_sv_search.Text+"'";
                        ud_sv_data.ItemsSource = sv.Load_data(sql).DefaultView;
                        check = 1;
                        ud_sv_msv.Text = ud_sv_search.Text;
                        ud_sv_name.Text = sv.Get_Value("select HoTen from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_dob.Text = sv.Get_Value("select NgaySinh from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_g.Text = sv.Get_Value("select GioiTinh from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_dc.Text = sv.Get_Value("select DiaChi from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_sdt.Text = sv.Get_Value("select SDT from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_mk.Text = sv.Get_Value("select MaKhoa from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_pname.Text = sv.Get_Value("select TenTN from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_sdt_p.Text = sv.Get_Value("select SDTTN from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_qh.Text = sv.Get_Value("select QHvsSV from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        ud_sv_slkl.Text = sv.Get_Value("select SLKyLuat from SinhVien where MaSV='" + ud_sv_msv.Text + "'");
                        check_sv = 1;
                        check = 0;
                        ud_sv_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button=(MessageBoxButton) MessageBox.Show("Mã sinh viên không tồn tại!, Có muốn thêm sinh viên mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_sv_msv.Text = ud_sv_search.Text;
                        }
                    }
                    ud_sv_clearn.IsEnabled = true;
                }
                else
                {
                    ud_sv_clearn_Click(sender, e);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_sv_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_sv = 0;
            ud_sv_add.IsEnabled = false;
            ud_sv_delete.IsEnabled=false;
            ud_sv_change.IsEnabled = false;
            ud_sv_search.Text = "";
            ud_sv_msv.Text = "";
            ud_sv_name.Text = "";
            ud_sv_dob.Text = "";
            ud_sv_sdt.Text = "";
            ud_sv_dc.Text = "";
            ud_sv_g.Text = "";
            ud_sv_pname.Text = "";
            ud_sv_sdt_p.Text = "";
            ud_sv_mk.Text = "";
            ud_sv_qh.Text = "";
            ud_sv_slkl.Text = "";
            ud_sv_clearn.IsEnabled=false;
            check = 0;
            ud_sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
        }

        private void ud_sv_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_dob_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_g_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_dc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_sdt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_mk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_pname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_sdt_p_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_qh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_slkl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_sv == 1)
            {
                ud_sv_change.IsEnabled = true;
            }
        }

        private void ud_sv_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                if(sv.ExecuteNonQuery("delete from SinhVien where MaSV='" + ud_sv_msv.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    
                    ud_sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
                    sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
                    ud_sv_clearn_Click(sender, e);
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_sv_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String old = ud_sv_msv.Text;
                DateTime date = Convert.ToDateTime(ud_sv_dob.Text);
                
                if (sv.ExecuteNonQuery("update SinhVien set  MaSV='" +ud_sv_msv.Text+ "', HoTen=N'" + ud_sv_name.Text + "', NgaySinh='" +date.ToString("MM/dd/yyyy")+ "', GioiTinh=N'" +ud_sv_g.Text + "', DiaChi=N'" +ud_sv_dc.Text+ "', SDT='"+ud_sv_sdt.Text+"',MaKhoa='"+ud_sv_mk.Text+"',TenTN=N'"+ud_sv_pname.Text+"',SDTTN='"+ud_sv_sdt_p.Text+"', QHvsSV=N'"+ud_sv_qh.Text+"', SLKyLuat='"+ud_sv_slkl.Text+"' where MaSV='" + old + "'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    ud_sv_change.IsEnabled=false;
                    String sql = "select * from SinhVien where MaSV='" + ud_sv_msv.Text + "'";
                    ud_sv_data.ItemsSource = sv.Load_data(sql).DefaultView;
                    sv_data.ItemsSource = sv.Load_data(sql0).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void sv_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ct_cn.IsSelected = true;
            dataRow = (DataRowView)sv_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {

                    //dataRow = (DataRowView)sv_data.SelectedItem;
                    //int index = sv_data.CurrentCell.Column.DisplayIndex;
                    //string cellValue = dataRow.Row.ItemArray[index].ToString();
                    ud_sv_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_sv_search_bt_Click(sender,e);

                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

        }
        
        //

        private void pcsvc_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            pcsvc_data.ItemsSource = pcsvc.Check_data().DefaultView;
        }

        private void pcsvc_dhh_Checked(object sender, RoutedEventArgs e)
        {
            pcsvc_data.ItemsSource= pcsvc.Check_data().DefaultView;
        }

        private void pcsvc_reload_Click(object sender, RoutedEventArgs e)
        {
            pcsvc_search.Text = "";
            pcsvc_dhh.IsChecked = false;
            pcsvc_data.ItemsSource = pcsvc.Load_data(sql8).DefaultView;
        }

        private void hddg_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            hddg_data.ItemsSource = hddg.Check_data().DefaultView;
        }

        private void ud_k_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "insert into Khoa values ('" + ud_k_mk.Text + "','" + ud_k_name.Text + "')";
                // khoa.ExecuteNonQuery cx giống khoa.Check_ID
                if (k.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm khoa thành công!", "Thông báo!");
                    k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    ud_k_delete.IsEnabled = true;
                    ud_k_add.IsEnabled = false;
                    check_k = 1;
                    ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm khoa không thành công!", "Thông báo!");
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }


        ///
        private void ud_k_mk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_k_mk.Text != "")
                    {
                        if (k.Check_ID("Khoa where MaKhoa='" + ud_k_mk.Text + "'") == true)
                        {
                            ud_k_add.IsEnabled = false;
                            ud_k_change.IsEnabled = true;
                            ud_k_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_k_add.IsEnabled = true;
                            ud_k_change.IsEnabled = false;
                            ud_k_delete.IsEnabled=false;

                        }

                        ud_k_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_k_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_k == 1)
            {
                ud_k_change.IsEnabled = true;
            }

        }

        private void ud_k_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_k_search.Text != "")
                {
                    if (k.Check_ID("Khoa where MaKhoa='" + ud_k_search.Text + "'") == true)
                    {
                        ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                        check = 1;
                        ud_k_mk.Text = k.Get_Value("select Makhoa from Khoa where Makhoa='" + ud_k_search.Text + "'");
                        ud_k_name.Text = k.Get_Value("select TenKhoa from Khoa where Makhoa='" + ud_k_search.Text + "'");
                        check_k = 1;
                        check = 0;
                        ud_k_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã khoa không tồn tại!, Có muốn thêm khoa mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_k_mk.Text = ud_k_search.Text;
                        }
                    }
                    ud_k_clearn.IsEnabled = true;
                }
                else
                {
                    ud_k_clearn_Click(sender, e);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_k_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_k = 0;
            ud_k_add.IsEnabled = false;
            ud_k_delete.IsEnabled = false;
            ud_k_change.IsEnabled = false;
            ud_k_search.Text = "";
            ud_k_mk.Text = "";
            ud_k_name.Text = "";
            ud_k_clearn.IsEnabled = false;
            check = 0;
            ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;

        }

        private void ud_k_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql;
                if (k.ExecuteNonQuery("delete from Khoa where Makhoa='" + ud_k_mk.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_k_clearn_Click(sender, e);
                    k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                    sql = "select * from Khoa";
                    ud_k_data.ItemsSource = k.Load_data(sql).DefaultView;

                }
                
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_k_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                String old=ud_k_mk.Text;
                if (k.ExecuteNonQuery("update Khoa set  Makhoa='" + ud_k_mk.Text + "', TenKhoa=N'"+ud_k_name.Text+"' where MaKhoa='"+old+"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                    k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    ud_k_data.ItemsSource = k.Load_data(sql1).DefaultView;
                    runliv();
                    ud_k_change.IsEnabled = false;
                }
                
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_k_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_k == 1)
            {
                ud_k_change.IsEnabled = true;
            }
        }

        //

        private void ud_tktx_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "insert into Toa_KTX values ('" + ud_tktx_mt.Text + "','" + ud_tktx_slp.Text + "','" + ud_tktx_tt.Text + "')";
                // tktx.ExecuteNonQuery cx giống tktx.Check_ID
                if (tktx.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm tòa thành công!", "Thông báo!");
                    tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                    ud_tktx_delete.IsEnabled = true;
                    ud_tktx_add.IsEnabled = false;
                    check_tktx = 1;
                    ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm tòa không thành công!", "Thông báo!");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_tktx_mt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_tktx_mt.Text != "")
                    {
                        if (tktx.Check_ID("Toa_KTX where MaToa='" + ud_tktx_mt.Text + "'") == true)
                        {
                            ud_tktx_add.IsEnabled = false;
                            ud_tktx_change.IsEnabled = true;
                            ud_tktx_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_tktx_add.IsEnabled = true;
                            ud_tktx_change.IsEnabled = false;
                            ud_tktx_delete.IsEnabled=false;

                        }
                        ud_tktx_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_tktx_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_tktx == 1)
            {
                ud_tktx_change.IsEnabled = true;
            }

        }

        private void ud_tktx_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_tktx_search.Text != "")
                {
                    if (tktx.Check_ID("Toa_KTX where MaToa='" + ud_tktx_search.Text + "'") == true)
                    {
                        ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                        check = 1;

                        ud_tktx_mt.Text = tktx.Get_Value("select MaToa from Toa_KTX where MaToa='" + ud_tktx_search.Text + "'");
                        ud_tktx_slp.Text = tktx.Get_Value("select SLPhong from Toa_KTX where MaToa='" + ud_tktx_search.Text + "'");
                        ud_tktx_tt.Text = tktx.Get_Value("select TinhTrang from Toa_KTX where MaToa='" + ud_tktx_search.Text + "'");

                        check_tktx = 1;
                        check = 0;
                        ud_tktx_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã tòa không tồn tại!, Có muốn thêm tòa mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_tktx_mt.Text = ud_tktx_search.Text;
                        }
                    }
                    ud_tktx_clearn.IsEnabled = true;
                }
                else
                {
                    ud_tktx_clearn_Click(sender, e);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_tktx_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_tktx = 0;
            ud_tktx_add.IsEnabled = false;
            ud_tktx_delete.IsEnabled = false;
            ud_tktx_change.IsEnabled = false;
            ud_tktx_search.Text = "";
            ud_tktx_mt.Text = "";
            ud_tktx_slp.Text = "";
            ud_tktx_tt.Text = "";

            ud_tktx_clearn.IsEnabled = false;
            check = 0;
            ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;

        }

        private void ud_tktx_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql;
                if (tktx.ExecuteNonQuery("delete from Toa_KTX where MaToa='" + ud_tktx_mt.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_tktx_clearn_Click(sender, e);
                    tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                    runliv();
                    ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                    sql = "select * from Toa_KTX";
                    ud_tktx_data.ItemsSource = tktx.Load_data(sql).DefaultView;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_tktx_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                String old = ud_tktx_mt.Text;
                if (tktx.ExecuteNonQuery("update Toa_KTX set  MaToa='" + ud_tktx_mt.Text + "',SLPhong ='"+ud_tktx_slp.Text+"',TinhTrang=N'"+ud_tktx_tt.Text+"' where MaToa='"+old+"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                    tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                    ud_tktx_change.IsEnabled=false;
                    tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                    runliv();
                    ud_tktx_data.ItemsSource = tktx.Load_data(sql2).DefaultView;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_tktx_slp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_tktx == 1)
            {
                ud_tktx_change.IsEnabled = true;
            }
        }

        private void ud_tktx_tt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_tktx == 1)
            {
                ud_tktx_change.IsEnabled = true;
            }
        }



        ///
        private void ud_pktx_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "insert into Phong_KTX values ('" + ud_pktx_mp.Text + "','" + ud_pktx_mt.Text + "','" + ud_pktx_slsv.Text + "','" + ud_pktx_tt.Text + "')";
                // pktx.ExecuteNonQuery cx giống phong.Check_ID
                if (pktx.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm Phòng KTX thành công!", "Thông báo!");
                    pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
                    ud_pktx_delete.IsEnabled = true;
                    ud_pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
                    check_pktx = 1;
                    ud_pktx_add.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm Phòng KTX không thành công!", "Thông báo!");
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_pktx_mp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_pktx_mp.Text != "")
                    {
                        if (pktx.Check_ID("Phong_KTX where MaPhong='" + ud_pktx_mp.Text + "'") == true)

                        {
                            ud_pktx_add.IsEnabled = false;
                            ud_pktx_change.IsEnabled = true;
                            ud_pktx_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_pktx_add.IsEnabled = true;
                            ud_pktx_change.IsEnabled = false;
                            ud_pktx_delete.IsEnabled = false;

                        }
                        ud_pktx_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_pktx_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_pktx == 1)
            {
                ud_pktx_change.IsEnabled = true;
            }

        }

        private void ud_pktx_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_pktx_search.Text != "")
                {
                    if (pktx.Check_ID("Phong_KTX where MaPhong='"+ud_pktx_search.Text+"'") == true)
                    {
                        String sql = "select * from Phong_KTX where MaPhong='"+ud_pktx_search.Text+"'";
                        ud_pktx_data.ItemsSource = pktx.Load_data(sql).DefaultView;
                        check = 1;

                        ud_pktx_mp.Text = pktx.Get_Value("select MaPhong from Phong_KTX where Maphong='" + ud_pktx_search.Text + "'");
                        ud_pktx_slsv.Text = pktx.Get_Value("select SLSV from Phong_KTX where Maphong='" + ud_pktx_search.Text + "'");
                        ud_pktx_mt.Text = pktx.Get_Value("select MaToa from Phong_KTX where Maphong='" + ud_pktx_search.Text + "'");
                        ud_pktx_tt.Text = pktx.Get_Value("select TinhTrang from Phong_KTX where Maphong='" + ud_pktx_search.Text + "'");


                        check_pktx = 1;
                        check = 0;
                        ud_pktx_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã Phòng KTX không tồn tại!, Có muốn thêm Phòng KTX mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_pktx_mp.Text = ud_pktx_search.Text;
                        }
                    }
                    ud_pktx_clearn.IsEnabled = true;
                }
                else
                {
                    ud_pktx_clearn_Click(sender, e);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_pktx_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_pktx = 0;
            ud_pktx_add.IsEnabled = false;
            ud_pktx_delete.IsEnabled = false;
            ud_pktx_change.IsEnabled = false;
            ud_pktx_search.Text = "";
            ud_pktx_mp.Text = "";
            ud_pktx_slsv.Text = "";
            ud_pktx_mt.Text = "";
            ud_pktx_tt.Text = "";
            ud_pktx_clearn.IsEnabled = false;
            check = 0;
            ud_pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;


        }

        private void ud_pktx_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pktx.ExecuteNonQuery("delete from Phong_KTX where Maphong='" + ud_pktx_mp.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
                    ud_pktx_clearn_Click(sender, e);
                    pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_pktx_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String old = ud_pktx_mp.Text;

                if (pktx.ExecuteNonQuery("update Phong_KTX set Maphong='" + ud_pktx_mp.Text + "',MaToa='"+ud_pktx_mt.Text+"',SLSV="+ud_pktx_slsv.Text+",TinhTrang=N'"+ud_pktx_tt.Text+"' where MaPhong='"+old+"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    runliv();
                    String sql = "select * from Phong_KTX where MaPhong='" + ud_pktx_search.Text + "'";
                    ud_pktx_data.ItemsSource = pktx.Load_data(sql).DefaultView;
                    pktx_data.ItemsSource = pktx.Load_data(sql3).DefaultView;
                    ud_pcsvc_change.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void ud_pktx_mt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_pktx == 1)
            {
                ud_pktx_change.IsEnabled = true;
            }
        }

        private void ud_pktx_slsv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_pktx == 1)
            {
                ud_pktx_change.IsEnabled = true;
            }
        }

        private void ud_pktx_tt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_pktx == 1)
            {
                ud_pktx_change.IsEnabled = true;
            }
        }

        ///
        private void ud_csvc_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "insert into CoSoVatChat values ('" + ud_csvc_mcsvc.Text + "','" + ud_csvc_name.Text +"')";
                // csvc.ExecuteNonQuery cx giống CSVC.Check_ID
                if (csvc.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm CSVC thành công!", "Thông báo!");
                    csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
                    runliv();
                    ud_csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
                    check_csvc = 1;
                    ud_csvc_add.IsEnabled = false;
                    ud_sv_delete.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Thêm CSVC không thành công!", "Thông báo!");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_csvc_mcsvc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_csvc_mcsvc.Text != "")
                    {
                        if (csvc.Check_ID(" CoSoVatChat where MaCSVC='" + ud_csvc_mcsvc.Text + "'") == true)

                        {
                            ud_csvc_add.IsEnabled = false;
                            ud_csvc_change.IsEnabled = true;
                            ud_csvc_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_csvc_add.IsEnabled = true;
                            ud_csvc_change.IsEnabled=false;
                            ud_csvc_delete.IsEnabled =false;

                        }
                        ud_csvc_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_csvc_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_csvc == 1)
            {
                ud_csvc_change.IsEnabled = true;
            }

        }

        private void ud_csvc_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_csvc_search.Text != "")
                {
                    if (csvc.Check_ID(" CoSoVatChat where MaCSVC='" + ud_csvc_search.Text + "'") == true)
                    {
                        String sql = "select * from CoSoVatChat";
                        ud_csvc_data.ItemsSource = csvc.Load_data(sql).DefaultView;
                        check = 1;
                        ud_csvc_mcsvc.Text = csvc.Get_Value("select MaCSVC from  CoSoVatChat where MaCSVC='" + ud_csvc_search.Text + "'");
                        ud_csvc_name.Text = csvc.Get_Value("select TenCSVC from  CoSoVatChat where MaCSVC='" + ud_csvc_search.Text + "'");
                        check_csvc = 1;
                        check = 0;
                        ud_csvc_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã CSVC không tồn tại!, Có muốn thêm CSVC mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_csvc_mcsvc.Text = ud_csvc_search.Text;
                        }
                    }
                    ud_csvc_clearn.IsEnabled = true;
                }
                
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_csvc_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_csvc = 0;
            ud_csvc_add.IsEnabled = false;
            ud_csvc_delete.IsEnabled = false;
            ud_csvc_change.IsEnabled = false;
            ud_csvc_search.Text = "";
            ud_csvc_mcsvc.Text = "";
            ud_csvc_name.Text = "";

            ud_csvc_clearn.IsEnabled = false;
            check = 0;
            ud_csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;


        }

        private void ud_csvc_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql;
                if (csvc.ExecuteNonQuery("delete from CoSoVatChat where MaCSVC='" + ud_csvc_mcsvc.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
                    ud_csvc_clearn_Click(sender, e);
                    csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                    sql = "select * from CoSoVatChat";
                    ud_csvc_data.ItemsSource = csvc.Load_data(sql).DefaultView;

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_csvc_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String old = ud_csvc_mcsvc.Text;
                if (csvc.ExecuteNonQuery("update CoSoVatChat set  MaCSVC='" + ud_csvc_mcsvc.Text + "',TenCSVC=N'"+ud_csvc_name.Text+"' where MaCSVC='"+old+"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    runliv();
                    ud_csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
                    csvc_data.ItemsSource = csvc.Load_data(sql6).DefaultView;
                    ud_csvc_change.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_csvc_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_csvc == 1)
            {
                ud_csvc_change.IsEnabled = true;
            }
        }





        /// 

        private void ud_dgdv_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // thông tin của khoa thì là k.Check_ID tương tự với các hàm khác
                String sql = "insert into DonGiaDichVu values ('" + ud_dgdv_mdg.Text + "','" + ud_dgdv_name.Text + "','" + ud_dgdv_pdg.Text + "')";
                // dgdv.ExecuteNonQuery cx giống dg.Check_ID
                if (dgdv.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm đơn giá thành công!", "Thông báo!");
                    check_dgdv = 1;
                    dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    ud_sv_delete.IsEnabled = true;
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    ud_dgdv_add.IsEnabled = false;
                    ud_dgdv_delete.IsEnabled = true;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm đơn giá không thành công!", "Thông báo!");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_dgdv_mdg_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_dgdv_mdg.Text != "")
                    {
                        if (dgdv.Check_ID("DonGiaDichVu where MaDV='" + ud_dgdv_mdg.Text + "'") == true)
                        {
                            ud_dgdv_change.IsEnabled=true;
                            ud_dgdv_add.IsEnabled = false;
                            ud_dgdv_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_dgdv_add.IsEnabled = true;
                            ud_dgdv_change.IsEnabled = false;
                            ud_dgdv_delete.IsEnabled = false;

                        }
                        ud_dgdv_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_dgdv_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_dgdv == 1)
            {
                ud_dgdv_change.IsEnabled = true;
            }

        }

        private void ud_dgdv_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_dgdv_search.Text != "")
                {
                    if (dgdv.Check_ID("DonGiaDichVu where MaDV='" + ud_dgdv_search.Text + "'") == true)

                    {

                        String sql = "select * from DonGiaDichVu";
                        ud_dgdv_data.ItemsSource = dgdv.Load_data(sql).DefaultView;
                        check = 1;
                        ud_dgdv_mdg.Text = dgdv.Get_Value("select MaDV from DonGiaDichVu where MaDV='" + ud_dgdv_search.Text + "'");
                        ud_dgdv_name.Text = dgdv.Get_Value("select TenDV from DonGiaDichVu where MaDV='" + ud_dgdv_search.Text + "'");
                        ud_dgdv_pdg.Text = dgdv.Get_Value("select PhiDv from DonGiaDichVu where MaDV='" + ud_dgdv_search.Text + "'");
                        check_dgdv = 1;
                        check = 0;
                        ud_dgdv_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã đơn giá không tồn tại!, Có muốn thêm đơn giá mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_dgdv_mdg.Text = ud_dgdv_search.Text;
                        }
                    }
                    ud_dgdv_clearn.IsEnabled = true;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_dgdv_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_dgdv = 0;
            ud_dgdv_add.IsEnabled = false;
            ud_dgdv_delete.IsEnabled = false;
            ud_dgdv_change.IsEnabled = false;
            ud_dgdv_search.Text = "";
            ud_dgdv_mdg.Text = "";
            ud_dgdv_name.Text = "";
            ud_dgdv_pdg.Text = "";

            ud_dgdv_clearn.IsEnabled = false;
            check = 0;
            ud_dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;

        }

        private void ud_dgdv_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql;
                if (dgdv.ExecuteNonQuery("delete from DonGiaDichVu where MaDV='" + ud_dgdv_mdg.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_dgdv_clearn_Click(sender, e);
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                    sql = "select * from DonGiaDichVu";
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql).DefaultView;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_dgdv_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                String old = ud_dgdv_mdg.Text;
                if (dgdv.ExecuteNonQuery("update DonGiaDichVu set  MaDV='" + ud_dgdv_mdg.Text + "', TenDV=N'" + ud_dgdv_name.Text + "', PhiDV='" + ud_dgdv_pdg.Text + "' where MaDV='" + old +"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    hddg.ExecuteNonQuery("update HoaDon_DonGia set TenDG=N'" + ud_dgdv_name.Text + "' where MaDG='" + ud_dgdv_mdg.Text + "'");
                    ud_hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
                    hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    ud_dgdv_change.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_dgdv_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_dgdv == 1)
            {
                ud_dgdv_change.IsEnabled = true;
            }
        }

        private void ud_dgdv_pdg_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_dgdv == 1)
            {
                ud_dgdv_change.IsEnabled = true;
            }
        }


        /// Phòng cơ sở vật chất
        private void ud_pcsvc_search_bt_Click(object sender, RoutedEventArgs e)
        {
            if((ud_pcsvc_mp_search.Text != "")&&(ud_pcsvc_mcsvc_search.Text != ""))
            {
                try
                {
                    if(pcsvc.Check_ID("Phong_CSVC where MaPhong = '" + ud_pcsvc_mp_search.Text + "'and MaCSVC='" + ud_pcsvc_mcsvc_search.Text + "'") == true)
                    {
                        check = 1;
                        ud_pcsvc_mp.Text = ud_pcsvc_mp_search.Text;
                        ud_pcsvc_mcsvc.Text = ud_pcsvc_mcsvc_search.Text;
                        ud_pcsvc_sl.Text = pcsvc.Get_Value("select SoLuong from Phong_CSVC where MaPhong='" + ud_pcsvc_mp_search.Text + "' and MaCSVC='" + ud_pcsvc_mcsvc_search.Text + "'");
                        ud_pcsvc_tt.Text = pcsvc.Get_Value("select TinhTrang from Phong_CSVC where MaPhong='" + ud_pcsvc_mp_search.Text + "' and MaCSVC='" + ud_pcsvc_mcsvc_search.Text + "'");
                        check = 0;
                        check_pcsvc = 1;
                        ud_pcsvc_delete.IsEnabled = true;
                        ud_pcsvc_clearn.IsEnabled=true;
                        String sql = "select * from Phong_CSVC where MaPhong='" + ud_pcsvc_mp_search.Text + "'";
                        ud_pcsvc_data.ItemsSource = pcsvc.Load_data(sql).DefaultView;
                    }
                    else
                    {
                        if(pcsvc.Check_ID("CoSoVatChat where MaCSVC='" + ud_pcsvc_mcsvc_search.Text + "'") == true)
                        {
                            if(pcsvc.Check_ID("Phong_KTX where MaPhong='" + ud_pcsvc_mp_search.Text + "'") == true)
                            {
                                MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Phòng này chưa có đồ trên, có muốn cập nhập đồ trên vào danh sách!", "Thông báo!", MessageBoxButton.OKCancel);
                                if (button == MessageBoxButton.OKCancel)
                                {
                                    ud_pcsvc_mp.Text = ud_pcsvc_mp_search.Text;
                                    ud_pcsvc_mcsvc.Text = ud_pcsvc_mcsvc_search.Text;
                                    ud_pcsvc_clearn.IsEnabled = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã phòng trên không tồn tại!", "Thông báo!");
                            }
                        }
                        else
                        {
                            if (pcsvc.Check_ID("Phong_KTX where MaPhong='" + ud_pcsvc_mp_search.Text + "'") == true)
                            {
                                MessageBox.Show("Mã cơ sở vật chất trên không tồn tại!", "Thông báo!");
                            }
                            else
                            {
                                MessageBox.Show("Mã phòng và mã cơ sở vật chất trên không tồn tại!", "Thông báo!");
                            }
                        }
                    }
                }catch(Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
        }

        private void ud_pcsvc_mp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                if (ud_pcsvc_mcsvc.Text != "")
                {
                    if (pcsvc.Check_ID("Phong_CSVC where MaPhong = '" + ud_pcsvc_mp.Text + "'and MaCSVC='" + ud_pcsvc_mcsvc.Text + "'") == true)
                    {
                        ud_pcsvc_delete.IsEnabled = true;
                        ud_pcsvc_change.IsEnabled = true;
                        ud_pcsvc_add.IsEnabled = false;
                    }
                    else
                    {
                        ud_pcsvc_delete.IsEnabled = false;
                        ud_pcsvc_change.IsEnabled = false;
                        ud_pcsvc_add.IsEnabled = true;
                    }
                    ud_pcsvc_clearn.IsEnabled = true;
                }
                if (check_pcsvc == 1)
                {
                    ud_pcsvc_change.IsEnabled = true;
                }
            }

        }

        private void ud_pcsvc_mcsvc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(check == 0)
            {
                if (ud_pcsvc_mp.Text != "")
                {
                    if (pcsvc.Check_ID("Phong_CSVC where MaPhong = '" + ud_pcsvc_mp.Text + "' and MaCSVC='" + ud_pcsvc_mcsvc.Text + "'") == true)
                    {
                        ud_pcsvc_delete.IsEnabled = true;
                        ud_pcsvc_change.IsEnabled = true;
                        ud_pcsvc_add.IsEnabled = false;
                    }
                    else
                    {
                        ud_pcsvc_delete.IsEnabled = false;
                        ud_pcsvc_change.IsEnabled = false;
                        ud_pcsvc_add.IsEnabled = true;
                    }
                }
                if (check_pcsvc == 1)
                {
                    ud_pcsvc_change.IsEnabled = true;
                }
            }
        }

        private void ud_pcsvc_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // thông tin của khoa thì là k.Check_ID tương tự với các hàm khác
                String sql = "insert into Phong_CSVC values ('" + ud_pcsvc_mp.Text + "','" + ud_pcsvc_mcsvc.Text + "','" + ud_pcsvc_sl.Text + "','"+ud_pcsvc_tt.Text+"')";
                // dgdv.ExecuteNonQuery cx giống dg.Check_ID
                if (pcsvc.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo!");
                    check_pcsvc = 1;
                    pcsvc_data.ItemsSource = pcsvc.Load_data(sql8).DefaultView;
                    ud_sv_delete.IsEnabled = true;
                    sql = "select * from Phong_CSVC where MaPhong='"+ud_pcsvc_mp.Text+"'";
                    ud_pcsvc_data.ItemsSource = pcsvc.Load_data(sql).DefaultView;
                    ud_pcsvc_add.IsEnabled = false;
                    ud_pcsvc_delete.IsEnabled = true;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!", "Thông báo!");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_pcsvc_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                String old1 = ud_pcsvc_mp.Text;
                String old2= ud_pcsvc_mcsvc.Text;
                if (pcsvc.ExecuteNonQuery("update Phong_CSVC set  MaPhong='" + ud_pcsvc_mp.Text + "', MaCSVC='" + ud_pcsvc_mcsvc.Text + "', SoLuong='" + ud_pcsvc_sl.Text + "',TinhTrang='"+ud_pcsvc_tt.Text+"' where MaPhong='" + old1  + "' and MaCSVC='"+old2+"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    String sql = "select * from Phong_CSVC where MaPhong='"+ud_pcsvc_mp.Text+"'";
                    ud_pcsvc_data.ItemsSource = pcsvc.Load_data(sql).DefaultView;
                    pcsvc_data.ItemsSource = pcsvc.Load_data(sql8).DefaultView;
                    ud_pcsvc_change.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_pcsvc_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_pcsvc = 0;
            check = 1;
            ud_pcsvc_add.IsEnabled = false;
            ud_pcsvc_delete.IsEnabled = false;
            ud_pcsvc_change.IsEnabled = false;
            ud_pcsvc_mp_search.Text = "";
            ud_pcsvc_mcsvc_search.Text = "";
            ud_pcsvc_mp.Text = "";
            ud_pcsvc_mcsvc.Text = "";
            ud_pcsvc_sl.Text = "";
            ud_pcsvc_tt.Text = "";
            ud_pcsvc_clearn.IsEnabled = false;
            check = 0;
            ud_pcsvc_data.ItemsSource = pcsvc.Load_data(sql8).DefaultView;
        }

        private void ud_pcsvc_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pcsvc.ExecuteNonQuery("delete from Phong_CSVC where MaPhong='" + ud_pcsvc_mp.Text + "' and MaCSVC='"+ud_pcsvc_mcsvc.Text+"'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_pcsvc_clearn_Click(sender, e);
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql8).DefaultView;
                    dgdv_data.ItemsSource = dgdv.Load_data(sql8).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_pcsvc_sl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_pcsvc == 1)
            {
                ud_pcsvc_change.IsEnabled = true;
            }
        }

        private void ud_pcsvc_tt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_pcsvc == 1)
            {
                ud_pcsvc_change.IsEnabled = true;
            }
        }


        ///
        private void ud_hddg_search_bt_Click(object sender, RoutedEventArgs e)
        {
            if ((ud_hddg_mhd_search.Text != "") && (ud_hddg_mdv_search.Text != ""))
            {
                try
                {
                    if (hddg.Check_ID("HoaDon_DonGia where MaHD = '" + ud_hddg_mhd_search.Text + "'and MaDG='" + ud_hddg_mdv_search.Text + "'") == true)
                    {
                        check = 1;
                        ud_hddg_mhd.Text = ud_hddg_mhd_search.Text;
                        ud_hddg_mdv.Text = ud_hddg_mdv_search.Text;
                        ud_hddg_name.Text = hddg.Get_Value("select TenDG from HoaDon_DonGia where MaHD='" + ud_hddg_mhd_search.Text + "' and MaDG='" + ud_hddg_mdv_search.Text + "'");
                        check = 0;
                        check_hddg = 1;
                        ud_hddg_delete.IsEnabled = true;
                        ud_hddg_clearn.IsEnabled = true;
                        String sql = "select * from HoaDon_DonGia where MaHD='" + ud_hddg_mhd_search.Text + "'";
                        ud_hddg_data.ItemsSource = hddg.Load_data(sql).DefaultView;
                    }
                    else
                    {
                        if (hddg.Check_ID("DonGiaDichVu where MaDV='" + ud_hddg_mdv_search.Text + "'") == true)
                        {
                            if (hddg.Check_ID("HoaDon where MaHD='" + ud_hddg_mhd_search.Text + "'") == true)
                            {
                                MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Phòng này chưa có đồ trên, có muốn cập nhập đồ trên vào danh sách!", "Thông báo!", MessageBoxButton.OKCancel);
                                if (button == MessageBoxButton.OKCancel)
                                {
                                    ud_hddg_mhd.Text = ud_hddg_mhd_search.Text;
                                    ud_hddg_mdv.Text = ud_hddg_mdv_search.Text;
                                    ud_hddg_name.Text = hddg.Get_Value("select TenDV from DonGiaDichVu where MaDV='"+ud_hddg_mdv_search.Text+"'");
                                    ud_hddg_clearn.IsEnabled = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã hóa đơn trên không tồn tại!", "Thông báo!");
                            }
                        }
                        else
                        {
                            if (hddg.Check_ID("HoaDon where MaHD='" + ud_hddg_mhd_search.Text + "'") == true)
                            {
                                MessageBox.Show("Mã đơn giá dịch vụ trên không tồn tại!", "Thông báo!");
                            }
                            else
                            {
                                MessageBox.Show("Mã hóa đơn và mã đơn giá dịch vụ trên không tồn tại!", "Thông báo!");
                            }
                        }
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
        }

        private void ud_hddg_mhd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                if (ud_hddg_mdv.Text != "")
                {
                    if (hddg.Check_ID("HoaDon_DonGia where MaHD = '" + ud_hddg_mhd.Text + "'and MaDG='" + ud_hddg_mdv.Text + "'") == true)
                    {
                        ud_hddg_delete.IsEnabled = true;
                        ud_hddg_change.IsEnabled = true;
                        ud_hddg_add.IsEnabled = false;
                    }
                    else
                    {
                        ud_hddg_delete.IsEnabled = false;
                        ud_hddg_change.IsEnabled = false;
                        ud_hddg_add.IsEnabled = true;
                    }
                    ud_hddg_clearn.IsEnabled = true;
                }
                if (check_hddg == 1)
                {
                    ud_hddg_change.IsEnabled = true;
                }
            }

        }

        private void ud_hddg_mdv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                if (ud_hddg_mhd.Text != "")
                {
                    if (hddg.Check_ID("HoaDon_DonGia where MaHD = '" + ud_hddg_mhd.Text + "' and MaDG='" + ud_hddg_mdv.Text + "'") == true)
                    {
                        ud_hddg_delete.IsEnabled = true;
                        ud_hddg_change.IsEnabled = true;
                        ud_hddg_add.IsEnabled = false;
                    }
                    else
                    {
                        ud_hddg_delete.IsEnabled = false;
                        ud_hddg_change.IsEnabled = false;
                        ud_hddg_add.IsEnabled = true;
                    }
                }
                if (check_hddg == 1)
                {
                    ud_hddg_change.IsEnabled = true;
                }
            }
        }

        private void ud_hddg_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // thông tin của khoa thì là k.Check_ID tương tự với các hàm khác
                String sql = "insert into HoaDon_DonGia values ('" + ud_hddg_mhd.Text + "','" + ud_hddg_mdv.Text + "','" + ud_hddg_name.Text + "')";
                // dgdv.ExecuteNonQuery cx giống dg.Check_ID
                if (hddg.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo!");
                    check_hddg = 1;
                    hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
                    ud_sv_delete.IsEnabled = true;
                    sql = "select * from HoaDon_DonGia where MaHD='" + ud_hddg_mhd.Text + "'";
                    ud_hddg_data.ItemsSource = hddg.Load_data(sql).DefaultView;
                    ud_hddg_add.IsEnabled = false;
                    ud_hddg_delete.IsEnabled = true;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!", "Thông báo!");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hddg_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                String old1 = ud_hddg_mhd.Text;
                String old2 = ud_hddg_mdv.Text;
                if (hddg.ExecuteNonQuery("update HoaDon_DonGia set  MaHD='" + ud_hddg_mhd.Text + "', MaDG='" + ud_hddg_mdv.Text + "', TenDG=N'" + ud_hddg_name.Text + "' where MaHD='" + old1 + "' and MaDG='" + old2 + "'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    String sql = "select * from HoaDon_DonGia where MaHD='" + ud_hddg_mhd.Text + "'";
                    dgdv.ExecuteNonQuery("update DonGiaDichVu set TenDV=N'"+ud_hddg_name.Text+"' where MaDV='"+ud_hddg_mdv.Text+"'");
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    dgdv_data.ItemsSource = dgdv.Load_data(sql7).DefaultView;
                    ud_hddg_data.ItemsSource = hddg.Load_data(sql).DefaultView;
                    hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
                    ud_hddg_change.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hddg_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_hddg = 0;
            check = 1;
            ud_hddg_add.IsEnabled = false;
            ud_hddg_delete.IsEnabled = false;
            ud_hddg_change.IsEnabled = false;
            ud_hddg_mhd_search.Text = "";
            ud_hddg_mdv_search.Text = "";
            ud_hddg_mhd.Text = "";
            ud_hddg_mdv.Text = "";
            ud_hddg_name.Text = "";
            ud_hddg_clearn.IsEnabled = false;
            check = 0;
            ud_hddg_data.ItemsSource = hddg.Load_data(sql9).DefaultView;
        }

        private void ud_hddg_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hddg.ExecuteNonQuery("delete from HoaDon_DonGia where MaHD='" + ud_hddg_mhd.Text + "' and MaDG='" + ud_hddg_mdv.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_hddg_clearn_Click(sender, e);
                    ud_dgdv_data.ItemsSource = dgdv.Load_data(sql9).DefaultView;
                    dgdv_data.ItemsSource = dgdv.Load_data(sql9).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hddg_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hddg == 1)
            {
                ud_hddg_change.IsEnabled = true;
            }
        }


        //
        private void ud_hopd_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date1 = Convert.ToDateTime(ud_hopd_nlhd.Text);
                DateTime date2 = Convert.ToDateTime(ud_hopd_hhd.Text);
                String sql = "insert into HopDong values ('" + ud_hopd_mhd.Text + "','" + ud_hopd_msv.Text + "','" + ud_hopd_mp.Text + "','" + date1.ToString("MM/dd/yyyy") + "','" + date2.ToString("MM/dd/yyyy") + "')";
                // hd.ExecuteNonQuery cx giống hopd.Check_ID
                if (hopd.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm hợp đồng thành công!", "Thông báo!");
                    hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
                    ud_hopd_delete.IsEnabled = true;
                    sql = "select * from HopDong where MaHD='"+ud_hopd_mhd.Text+"'";
                    ud_hopd_data.ItemsSource = hopd.Load_data(sql).DefaultView;
                    check_hopd = 1;
                    ud_hopd_delete.IsEnabled=true;
                    ud_hopd_add.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm hợp đồng không thành công!", "Thông báo!");

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hopd_mhd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_hopd_mhd.Text!="")
                    {
                        if (sv.Check_ID("HopDong where MaHD='" + ud_hopd_mhd.Text + "'") == true)
                        {
                            ud_hopd_change.IsEnabled=true;
                            ud_hopd_add.IsEnabled = false;
                            ud_hopd_delete.IsEnabled=true;
                        }
                        else
                        {
                            ud_hopd_change.IsEnabled = false;
                            ud_hopd_add.IsEnabled = true;
                            ud_hopd_delete.IsEnabled = false;

                        }
                        ud_hopd_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_hopd_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_hopd == 1)
            {
                ud_hopd_change.IsEnabled = true;
            }

        }

        private void ud_hopd_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_hopd_search.Text != "")
                {
                    if (hopd.Check_ID("HopDong where MaHD='" + ud_hopd_search.Text + "'") == true)
                    {
                        String sql = "select * from HopDong where MaHD='" + ud_hopd_search.Text + "'";
                        ud_hopd_data.ItemsSource = hopd.Load_data(sql).DefaultView;
                        check = 1;

                        ud_hopd_mhd.Text = hopd.Get_Value("select MaHD from HopDong where Mahd='" + ud_hopd_search.Text + "'");
                        ud_hopd_nlhd.Text = hopd.Get_Value("select NgayLHD from HopDong where Mahd='" + ud_hopd_search.Text + "'");
                        ud_hopd_msv.Text = hopd.Get_Value("select MaSV from HopDong where Mahd='" + ud_hopd_search.Text + "'");
                        ud_hopd_mp.Text = hopd.Get_Value("select MaPhong from HopDong where Mahd='" + ud_hopd_search.Text + "'");
                        ud_hopd_hhd.Text = hopd.Get_Value("select HanHD from HopDong where Mahd='" + ud_hopd_search.Text + "'");

                        check_hopd = 1;
                        check = 0;
                        ud_hopd_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã hợp đồng không tồn tại!, Có muốn thêm hợp đồng mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_hopd_mhd.Text = ud_hopd_search.Text;
                        }
                    }
                    ud_hopd_clearn.IsEnabled = true;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hopd_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_hopd = 0;
            ud_hopd_add.IsEnabled = false;
            ud_hopd_delete.IsEnabled = false;
            ud_hopd_change.IsEnabled = false;
            ud_hopd_search.Text = "";
            ud_hopd_mhd.Text = "";
            ud_hopd_msv.Text = "";
            ud_hopd_nlhd.Text = "";
            ud_hopd_mp.Text = "";
            ud_hopd_hhd.Text = "";
            ud_hopd_clearn.IsEnabled = false;
            check = 0;
            ud_hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;

        }

        private void ud_hopd_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hopd.ExecuteNonQuery("delete from HopDong where MaHD='" + ud_hopd_mhd.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_hopd_clearn_Click(sender, e);
                    hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
                    ud_hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hopd_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String old = ud_hopd_mhd.Text;

                DateTime date1 = Convert.ToDateTime(ud_hopd_nlhd.Text);
                DateTime date2 = Convert.ToDateTime(ud_hopd_hhd.Text);
                if (hopd.ExecuteNonQuery("update HopDong set  MaHD='" + ud_hopd_mhd.Text + "', MaSV='"+ud_hopd_msv.Text+"',MaPhong='"+ud_hopd_mp.Text+"',NgayLHD='"+ date1.ToString("MM/dd/yyyy") + "',HanHD='"+ date2.ToString("MM/dd/yyyy") + "' where MaHD='"+old+"'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    ud_hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
                    hopd_data.ItemsSource = hopd.Load_data(sql4).DefaultView;
                    ud_hopd_change.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hopd_msv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hopd == 1)
            {
                ud_hopd_change.IsEnabled = true;
            }
        }

        private void ud_hopd_mp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hopd == 1)
            {
                ud_hopd_change.IsEnabled = true;
            }
        }

        private void ud_hopd_nlhd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hopd == 1)
            {
                ud_hopd_change.IsEnabled = true;
            }
        }

        private void ud_hopd_hhd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hopd == 1)
            {
                ud_hopd_change.IsEnabled = true;
            }
        }


        ///
        private void ud_hoad_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date1 = Convert.ToDateTime(ud_hoad_nlhd.Text);
                String sql = "insert into HoaDon values ('" + ud_hoad_mhd.Text + "','" + ud_hoad_mp.Text + "','" + ud_hoad_sdd.Text + "','" + ud_hoad_sdc.Text + "','" + ud_hoad_snd.Text + "','" + ud_hoad_snc.Text + "','" + date1.ToString("MM/dd/yyyy") + "','" + ud_hoad_tt.Text+"')";
                // hoadon.ExecuteNonQuery cx giống hoadon.Check_ID
                if (hoad.ExecuteNonQuery(sql) == true)
                {
                    MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo!");
                    hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
                    ud_hoad_delete.IsEnabled = true;
                    sql = "select * from HoaDon where MaHD='" + ud_hoad_mhd.Text + "'";
                    ud_hoad_data.ItemsSource = hoad.Load_data(sql).DefaultView;
                    ud_hoad_add.IsEnabled = false;
                    check_hoad = 1;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Thêm hóa đơn không thành công!", "Thông báo!");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hoad_search_bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ud_hoad_search.Text != "")
                {
                    if (hoad.Check_ID("HoaDon where MaHD='"+ud_hoad_search.Text+"'") == true)
                    {
                        String sql = "select * from HoaDon where MaHD='" + ud_hoad_search.Text + "'";
                        ud_hoad_data.ItemsSource = hoad.Load_data(sql).DefaultView;
                        check = 1;
                        ud_hoad_mhd.Text = ud_hoad_search.Text;
                        ud_hoad_mp.Text = hoad.Get_Value("select MaPhong from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        ud_hoad_sdd.Text = hoad.Get_Value("select SDD from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        ud_hoad_sdc.Text = hoad.Get_Value("select SDC from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        ud_hoad_snd.Text = hoad.Get_Value("select SND from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        ud_hoad_snc.Text = hoad.Get_Value("select SNC from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        ud_hoad_nlhd.Text = hoad.Get_Value("select NgayLapHD from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        ud_hoad_tt.Text = hoad.Get_Value("select TinhTrang from HoaDon where MaHD='" + ud_hoad_search.Text + "'");
                        check_hoad = 1;
                        check = 0;
                        ud_hoad_delete.IsEnabled = true;
                    }
                    else
                    {
                        MessageBoxButton button = (MessageBoxButton)MessageBox.Show("Mã hóa đơn không tồn tại!, Có muốn thêm hóa đơn mới", "Thông báo", MessageBoxButton.OKCancel);
                        if (button == MessageBoxButton.OKCancel)
                        {
                            ud_hoad_mhd.Text = ud_hoad_search.Text;
                        }
                    }
                    ud_hoad_clearn.IsEnabled = true;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void ud_hoad_clearn_Click(object sender, RoutedEventArgs e)
        {
            check_hoad = 0;
            ud_hoad_add.IsEnabled = false;
            ud_hoad_delete.IsEnabled = false;
            ud_hoad_change.IsEnabled = false;
            ud_hoad_search.Text = "";
            ud_hoad_mhd.Text = "";
            ud_hoad_mp.Text = "";
            ud_hoad_sdd.Text = "";
            ud_hoad_sdc.Text = "";
            ud_hoad_nlhd.Text = "";
            ud_hoad_snd.Text = "";
            ud_hoad_snc.Text = "";
            ud_hoad_tt.Text = "";
            ud_hoad_clearn.IsEnabled = false;
            check = 0;
            ud_hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;


        }

        private void ud_hoad_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hoad.ExecuteNonQuery("delete from HoaDon where MaHD='" + ud_hoad_mhd.Text + "'") == true)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    ud_hoad_clearn_Click(sender, e);
                    hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
                    ud_hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }



        private void ud_hoad_mhd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check == 0)
            {
                try
                {
                    if (ud_hoad_mhd.Text != "")
                    {
                        if (hoad.Check_ID("HoaDon where MaHD='" + ud_hopd_mhd.Text + "'") == true)
                        {
                            ud_hoad_add.IsEnabled = false;
                            ud_hoad_change.IsEnabled = true;
                            ud_hoad_delete.IsEnabled = true;
                        }
                        else
                        {
                            ud_hoad_add.IsEnabled = true;
                            ud_hoad_change.IsEnabled = false;
                            ud_hoad_delete.IsEnabled = false;
                        }
                        ud_hoad_clearn.IsEnabled = true;
                    }
                    else
                    {
                        ud_hoad_clearn_Click(sender, e);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String old = ud_hoad_mhd.Text;

                DateTime date1 = Convert.ToDateTime(ud_hoad_nlhd.Text);
                if (hopd.ExecuteNonQuery("update HoaDon set  MaHD='" + ud_hoad_mhd.Text + "', MaPhong='" + ud_hoad_mp.Text + "',SDD='" + ud_hoad_sdd.Text + "',SDC='" + ud_hoad_sdc.Text +"', SND = '" + ud_hoad_snd.Text +"', SNC = '" + ud_hoad_snc.Text + "', NgayLapHD='" + date1.ToString("MM/dd/yyyy") + "',TinhTrang='" +ud_hoad_tt.Text+ "' where MaHD='" + old + "'") == true)
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    String sql = "select * from HoaDon where MaHD='" + ud_hoad_mhd.Text + "'";
                    ud_hoad_data.ItemsSource = hoad.Load_data(sql).DefaultView;
                    hoad_data.ItemsSource = hoad.Load_data(sql5).DefaultView;
                    ud_hoad_change.IsEnabled = false;
                    runliv();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công!", "Thông báo");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ud_hoad_mp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_sdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_sdc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_snd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_snc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_nlhd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void ud_hoad_tt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check_hoad == 1)
            {
                ud_hoad_change.IsEnabled = true;
            }
        }

        private void tk_date_year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //year = "" + tk_date_year.SelectedItem.ToString();
                //year = "2020";
                //sd_line();
                if (tk_date_year.SelectedIndex.ToString() != "1")
                {
                    //year = tk_date_year.SelectedItem.ToString();
                    switch (tk_date_year.SelectedIndex.ToString())
                    {
                        case "0":
                        {
                            year = "2023";
                            break;
                        }
                        case "2":
                        {
                            year = "2021";
                            break;
                        }
                        case "3":
                        {
                            year = "2020";
                            break;
                        }
                    }
                    sd_line();
                    check_tk = 1;
                }
                else
                {
                    if (check_tk == 1)
                    {
                        year = "2022";
                        sd_line();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tk_search_btn_Click(object sender, RoutedEventArgs e)
        {
            sd_line();
            tk_reload.IsEnabled = true;
        }

        private void tk_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tk_search.Text != "")
            {
                tk_search_btn.IsEnabled = true;
            }
            else
            {
                tk_search_btn.IsEnabled = false;
            }
            room = tk_search.Text;
        }

        private void tk_reload_Click(object sender, RoutedEventArgs e)
        {
            tk_search.Text = "";
            tk_reload.IsEnabled = false;
            tk_date_year.SelectedIndex = 1;
            sd_line();
            
        }

        private void sv_out_Click(object sender, RoutedEventArgs e)
        {
            sv.Print_File(sv.Check_data());
        }

        private void k_out_Click(object sender, RoutedEventArgs e)
        {
            k.Print_File(k.Check_data());
        }

        private void tktx_out_Click(object sender, RoutedEventArgs e)
        {
            tktx.Print_File(tktx.Check_data());
        }

        private void pktx_out_Click(object sender, RoutedEventArgs e)
        {
            pktx.Print_File(pktx.Check_data());
        }

        private void hopd_out_Click(object sender, RoutedEventArgs e)
        {
            hopd.Print_File(hopd.Check_data());
        }

        private void hoad_out_Click(object sender, RoutedEventArgs e)
        {
            hoad.Print_File(hoad.Check_data());
        }

        private void csvc_out_Click(object sender, RoutedEventArgs e)
        {
            csvc.Print_File(csvc.Check_data());
        }

        private void dgdv_out_Click(object sender, RoutedEventArgs e)
        {
            dgdv.Print_File(dgdv.Check_data());
        }

        private void pcsvc_out_Click(object sender, RoutedEventArgs e)
        {
            pcsvc.Print_File(pcsvc.Check_data());
        }

        private void hddg_out_Click(object sender, RoutedEventArgs e)
        {
            hddg.Print_File(hddg.Check_data());
        }

        private void hoad_in_Click(object sender, RoutedEventArgs e)
        {
            if (hoad_search.Text != "")
            {
                Print_Bill pb = new Print_Bill(conn, sql5, hoad_search, hoad_dtt, hoad_ctt,id);
                pb.Show();
            }
            else
            {
                MessageBox.Show("Chưa chọn hóa đơn để in!", "Thông báo!");
            }
        }

        private void k_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ct_cn.IsSelected = true;
            cn_k.IsSelected = true;
            dataRow = (DataRowView)k_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_k_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_k_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void tktx_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ct_cn.IsSelected = true;
            cn_tktx.IsSelected = true;
            dataRow = (DataRowView)tktx_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_tktx_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_tktx_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void pktx_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_pktx.IsSelected = true;
            dataRow = (DataRowView)pktx_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_pktx_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_pktx_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void hopd_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_hopd.IsSelected = true;
            dataRow = (DataRowView)hopd_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_hopd_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_hopd_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void hoad_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_hoad.IsSelected = true;
            dataRow = (DataRowView)hoad_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_hoad_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_hoad_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void csvc_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_csvc.IsSelected = true;
            dataRow = (DataRowView)csvc_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_csvc_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_csvc_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void dgdv_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_dgdv.IsSelected = true;
            dataRow = (DataRowView)dgdv_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_dgdv_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_dgdv_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void pcsvc_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_pcsvc.IsSelected = true;
            dataRow = (DataRowView)pcsvc_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_pcsvc_mp_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_pcsvc_mcsvc_search.Text = dataRow.Row.ItemArray[1].ToString();
                    ud_pcsvc_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void hddg_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ct_cn.IsSelected = true;
            cn_hddg.IsSelected = true;
            dataRow = (DataRowView)hddg_data.SelectedItem;
            try
            {
                if (dataRow != null)
                {
                    ud_hddg_mhd_search.Text = dataRow.Row.ItemArray[0].ToString();
                    ud_hddg_mdv_search.Text = dataRow.Row.ItemArray[1].ToString();
                    ud_hddg_search_bt_Click(sender, e);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
