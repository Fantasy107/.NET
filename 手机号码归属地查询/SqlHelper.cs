using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 手机号码归属地查询

{
    class SqlHelper
    {
        private static string ConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;


       
        #region  version 1.0
        /*
        /// <summary>
        /// 返回一个受影响的行数
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            using(SqlConnection conn=new SqlConnection(ConnStr))
            {
                conn.Open();
                using(SqlCommand cmd=conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 用于返回一个有且只有一行一列的项
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            using(SqlConnection conn=new SqlConnection(ConnStr))
            {
                conn.Open();
                using(SqlCommand cmd=conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataset(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet dataset = new DataSet();

                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

         */
        #endregion

        //version 2.0

        /// <summary>
        /// 返回一个受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">用params修饰，表示一个长度可变的数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    //foreach (var param in parameters)
                    //{
                    //    cmd.Parameters.Add(param);
                    //}
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 返回有且只有一行一列的项
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql,params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        ///  返回一个表并保存到本地
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataset(string sql,params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet dataset = new DataSet();

                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
    }
}
