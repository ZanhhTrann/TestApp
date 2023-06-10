using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
namespace APP_DACS_QLKTX.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private int point = 0;
        public Login()
        {
            InitializeComponent();
        }

        private void _bt_Login_Click(object sender, RoutedEventArgs e)
        {
            {
                var pass = _tb_pass.Password;
                int check = 0;
                String id = tb_id.Text;
                String sql = "select ID from Account Where ID='" + id + "'";
                String sql1 = "select * from Account Where ID='" + id + "'and Pass='" + pass + "'";
                String sql2 = "select [Check] from Account where ID = '" + id + "'";
                String sql3 = "select * from Account where ID='" + id + "'and Pass like '&0b%'";
                String sqlconn = @"Data Source=ZANHH\SQLEXPRESS;Initial Catalog=DACS_QLKTX;Integrated Security=True";

                try
                {
                    SqlConnection conn = new SqlConnection(sqlconn);
                    if (id != "" && pass != "")
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            SqlDataReader dID = cmd.ExecuteReader();
                            if (dID.Read() == true)
                            {
                                dID.Close();
                                using (SqlCommand cmd2 = new SqlCommand(sql2, conn))
                                {
                                    cmd.Dispose();
                                    cmd.Clone();
                                    check = int.Parse(cmd2.ExecuteScalar().ToString());
                                    //MessageBox.Show("" + check);
                                    if (check < 5)
                                    {
                                        cmd2.Dispose();
                                        cmd2.Clone();
                                        using (SqlCommand cmd3 = new SqlCommand(sql1, conn))
                                        {
                                            SqlDataReader account = cmd3.ExecuteReader();
                                            if (account.Read() == true)
                                            {
                                                account.Close();
                                                check = 0;
                                                //point = 1;
                                                using (SqlCommand cmd4 = new SqlCommand(sql3, conn))
                                                {
                                                    SqlDataReader checkPass = cmd4.ExecuteReader();
                                                    if (checkPass.Read() == true)
                                                    {
                                                        checkPass.Close();
                                                        cmd4.Dispose();
                                                        point = -1;
                                                    }
                                                    else
                                                    {
                                                        checkPass.Close();
                                                        cmd4.Dispose();
                                                        point = 1;

                                                    }
                                                }

                                            }
                                            else
                                            {
                                                account.Close();
                                                cmd3.Dispose();
                                                cmd3.Clone();
                                                ++check;
                                                if (check < 3)
                                                {
                                                    MessageBox.Show("Mật khẩu không hợp lệ!", "Thông báo", MessageBoxButton.OK);
                                                }
                                                else if (check < 5)
                                                {
                                                    MessageBox.Show("Bạn còn " + (5 - check) + " lần thử trước khi tài khoản bị khóa!","Thông báo", MessageBoxButton.OK);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Tài khoản đã bị khóa tạm thời do nhập sai mật khẩu quá số lần quy định!", "Thông báo", MessageBoxButton.OK);
                                                }
                                            }
                                            String sql4 = "update Account set [Check] ='" + check + "' Where ID='" + id + "'";
                                            using (SqlCommand cmd4 = new SqlCommand(sql4, conn))
                                            {
                                                cmd4.ExecuteNonQuery();
                                                cmd4.Dispose();
                                                cmd4.Clone();
                                            }

                                        }

                                    }
                                    else
                                    {
                                        cmd2.Dispose();
                                        cmd2.Clone();
                                        MessageBox.Show("Tài khoản đã bị khóa tạm thời! Vui lòng liên hệ với người quản trị để được hỗ trợ!", "Thông báo", MessageBoxButton.OK);
                                    }
                                }
                            }
                            else
                            {
                                dID.Close();
                                cmd.Dispose();
                                cmd.Clone();
                                MessageBox.Show("Tài khoản này không tồn tại !!!", "Thông báo", MessageBoxButton.OK);
                            }
                            //MessageBox.Show("''");
                            conn.Close();
                            if (point == 1)
                            {
                                Menu mw = new Menu(id);
                                this.Close();
                                mw.Show();
                            }
                            if (point == -1)
                            {
                                ChangePassword cp = new ChangePassword(conn, id);
                                this.Close();
                                cp.Show();
                            }
                            point = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo", MessageBoxButton.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK);
                }
            }
        }
    }
}
