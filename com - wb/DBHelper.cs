using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sf
{
    public class DBHelper
    {
        //string connectionString = "Data Source=DESKTOP-FLL68BC\SQLEXPRESS;Initial Catalog=CanToolApp;Integrated Security=True";
        //数据库连接字符串
        private static string SQL_CONN_STR = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public static SqlConnection conn = new SqlConnection(SQL_CONN_STR);

        #region 【执行Select方法】
        public DataSet GetDataSet(string sql)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        #endregion


        //根据id获取signal的name
        public static ArrayList Getsigname(int id)
        {
            conn.Open();
            string sql = string.Format("select SignalName from CanSignal where ID = '{0}';", id);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ArrayList signame = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                signame.Add(row["SignalName"].ToString());
            }
            conn.Close();
            return signame;
        }

        //根据signalname获取其起始位
        public static int Getsigstart(string name)
        {
            conn.Open();
            string sql = string.Format("select OriginAndLenth from CanSignal where SignalName = '{0}';", name);
            SqlCommand cmd = new SqlCommand(sql, conn);
            object obj = cmd.ExecuteScalar();
            String OriginAndLenth = "";
            if (obj != null)
            {
                OriginAndLenth = obj.ToString();
            }
            int a = OriginAndLenth.IndexOf("|");
            string s = OriginAndLenth.Substring(0, a);
            int start = Convert.ToInt32(s);
            
            conn.Close();
            return start;
        }

        //根据signalname获取其长度
        public static int Getsiglength(string name)
        {
            conn.Open();
            string sql = string.Format("select OriginAndLenth from CanSignal where SignalName = '{0}';", name);
            SqlCommand cmd = new SqlCommand(sql, conn);
            object obj = cmd.ExecuteScalar();
            String OriginAndLenth = "";
            if (obj != null)
            {
                OriginAndLenth = obj.ToString();
            }
            int a = OriginAndLenth.IndexOf("|");
            int b = OriginAndLenth.IndexOf("@");
            string s = OriginAndLenth.Substring(a+1, b-a-1);
            int length = Convert.ToInt32(s);

            conn.Close();
            return length;
        }

        //根据signalname获取其A
        public static int GetsiglA(string name)
        {
            conn.Open();
            string sql = string.Format("select A from CanSignal where SignalName = '{0}';", name);
            SqlCommand cmd = new SqlCommand(sql, conn);
            object obj = cmd.ExecuteScalar();
            string str = "";
            if (obj != null)
            {
                str = obj.ToString();
            }
            int A = Convert.ToInt32(str);
            conn.Close();
            return A;
        }

        //根据signalname获取其B
        public static int GetsiglB(string name)
        {
            conn.Open();
            string sql = string.Format("select B from CanSignal where SignalName = '{0}';", name);
            SqlCommand cmd = new SqlCommand(sql, conn);
            object obj = cmd.ExecuteScalar();
            string str = "";
            if (obj != null)
            {
                str = obj.ToString();
            }
            int B = Convert.ToInt32(str);
            conn.Close();
            return B;
        }

        #region[获取Message的ID]
        //根据message 获取 signal 的 ID
        public int SelectIdInMessage(string name)
        {

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(name, conn);
            SqlCommand cmd = new SqlCommand(name, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int  result = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            conn.Close();

            
            return result;

        }

        //根据Signal的name 获取物理值的范围
        public float SelectCInMessage(string name)
        {

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(name, conn);
            SqlCommand cmd = new SqlCommand(name, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            float result = float.Parse(ds.Tables[0].Rows[0][0].ToString());
            conn.Close();


            return result;

        }

        //public int SelectIdInMessage(string name)
        //{

        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SelectIdInMessage";
        //        cmd.Parameters.AddWithValue("@MeaasgeName", name);
        //        int result = Convert.ToInt32(cmd.ExecuteScalar());
        //        return result;

        //}
        #endregion
       
    }
}
