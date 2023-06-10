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

namespace APP_DACS_QLKTX.Views
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private SqlConnection conn;
        private String id;
        public ChangePassword(SqlConnection conn, String id)
        {

            InitializeComponent();
            this.conn = conn;
            this.id = id;
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            this.Close();
            lg.Show();
        }

        private void _bt_Login_Click(object sender, RoutedEventArgs e)
        {
            var np = pbl_np.Password;
            var rnp = pbl_rnp.Password;
            if (np != "")
            {
                if (np.Length >= 8)
                {
                    if (np == rnp)
                    {
                        String sql = "update Account set Pass='" + np + "' where ID='" + id + "'";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cmd.Clone();
                            conn.Close();
                            Menu mw = new Menu(id);
                            mw.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhập lại mật khẩu không chính xác!", "Thông báo", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu mới phải có độ dài ít nhất là 8 ký tự!!", "Thông báo", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButton.OK);
            }
        }
    }
}
