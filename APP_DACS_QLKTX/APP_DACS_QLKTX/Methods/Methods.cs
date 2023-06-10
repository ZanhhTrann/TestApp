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
    internal class Methods
    {
        protected SqlConnection conn;
        protected String sql0;
        protected TextBox search;

        public DataTable Load_data(String sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.ExecuteNonQuery();
                    da.SelectCommand = cmd;
                    dt.Clear();
                    da.Fill(dt);
                    cmd.Dispose();
                    //return dt;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            return dt;
        }
        //
        public String Get_Value(String sql)
        {
            String value = "";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //conn.Open();
                    if(cmd.ExecuteScalar().GetType() == typeof(DateTime))
                    {
                        DateTime dt = (DateTime)cmd.ExecuteScalar();
                        value = dt.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        value = cmd.ExecuteScalar().ToString();
                    }
                    //conn.Close();
                    //cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            return value.Replace("  ", "");
        }
        public Boolean Check_ID(String sql)
        {
            String sql1 = "select * from "+sql; 
            Boolean result = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql1, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = true;
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }
        public Boolean ExecuteNonQuery(String sql)
        {
            try
            {
                using(SqlCommand cmd= new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
