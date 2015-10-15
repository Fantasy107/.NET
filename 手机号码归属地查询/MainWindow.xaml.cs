using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 手机号码归属地查询
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static string ConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            #region version 1.0
            /* 
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件|*.txt";
            if(ofd.ShowDialog()==false)
            {
                return;
            }

           string[] lines= File.ReadLines(ofd.FileName, Encoding.Default).ToArray();
            for(int i=1;i<lines.Count();i++)
            {
               string[] line = lines[i].Split('\t');
               string Haoduan = line[0];
                
               string guishudi = line[1];
               guishudi = guishudi.Trim('"');

               string TelType = line[2];
               TelType = TelType.Trim('"');
               string quhao = line[3];
               quhao = quhao.Trim('"');
               SqlHelper.ExecuteNonQuery
                   ("insert into T_TelNum(HaoDUan,GuiShuDi,LeiXing,quHao) values(@HaoDuan,@GuiShuDi,@LeiXing,@quHao)",
                  new SqlParameter("@HaoDuan", Haoduan), new SqlParameter("@GuiShuDi", guishudi), new SqlParameter("@LeiXing", TelType),
                  new SqlParameter("@quHao", quhao));
            }
            */
            #endregion

            #region  version 2.0
            ////打开文件
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "文本文件|*.txt";
            //if (ofd.ShowDialog() == false)
            //{
            //    return;
            //}
            ////读取每一行
            //string[] lines = File.ReadLines(ofd.FileName, Encoding.Default).ToArray();
            
            //using (SqlConnection conn = new SqlConnection(ConnStr))
            //{
            //    //打开数据库连接
            //    conn.Open();

            //    for (int i = 1; i < lines.Count(); i++)
            //    {
                    
            //        //把每一行的字符串分割成几个字符串
            //        string[] line = lines[i].Split('\t');
                  
            //        string Haoduan = line[0];

            //        string guishudi = line[1];
            //        guishudi = guishudi.Trim('"');

            //        string TelType = line[2];
            //        TelType = TelType.Trim('"');
            //        string quhao = line[3];
            //        quhao = quhao.Trim('"');
            //        //向数据库发送Sql语句
            //        using(SqlCommand cmd=conn.CreateCommand())
            //        {
            //            #region 测试
            //            //cmd.CommandText = "select * from T_TelNum ";
            //            //using(SqlDataReader read=cmd.ExecuteReader())
            //            //{
            //            //    while(read.Read())
            //            //    {
            //            //        string temp = read.GetString(1);
            //            //        MessageBox.Show(temp);
            //            //    }
            //            //}
            //            #endregion
            //            cmd.CommandText = "insert into T_TelNum (HaoDuan,GuiShuDi,LeiXing,quHao) values(@HaoDuan,@GuiShuDi,@LeiXing,@quHao)";
            //            SqlParameter[] parameters ={new SqlParameter("@HaoDuan", Haoduan),
            //                new SqlParameter("@GuiShuDi", guishudi),
            //                new SqlParameter("@LeiXing", TelType), 
            //                new SqlParameter("@quHao", quhao)};

            //            cmd.Parameters.AddRange(parameters);
            //            cmd.ExecuteNonQuery();

                        
            //        }
            //    }
            //}

            #endregion

            //version 3.0
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件|*.txt";
            if (ofd.ShowDialog() == false)
            {
                return;
            }
            //读取每一行
            string[] lines = File.ReadLines(ofd.FileName, Encoding.Default).ToArray();
            DataTable table = new DataTable();
            table.Columns.Add("HaoDuan");
            table.Columns.Add("guishudi");
            table.Columns.Add("telType");
            table.Columns.Add("quhao");
            for (int i = 1; i < lines.Length; i++)
            {
               // 把每一行的字符串分割成几个字符串
                    string[] line = lines[i].Split('\t');
                  
                    string Haoduan = line[0];

                    string guishudi = line[1];
                    guishudi = guishudi.Trim('"');

                    string TelType = line[2];
                    TelType = TelType.Trim('"');
                    string quhao = line[3];
                    quhao = quhao.Trim('"');
                    DataRow row = table.NewRow();//新建一行
                    row["HaoDuan"] = Haoduan;
                    row["guishudi"] = guishudi;
                    row["telType"] = TelType;
                    row["quhao"] = quhao;
                    table.Rows.Add(row);//把一行添加到表中
            }
            using (SqlBulkCopy sbc = new SqlBulkCopy(ConnStr))
               {
                   sbc.DestinationTableName = "T_TelNum";
                sbc.ColumnMappings.Add("HaoDuan","HaoDuan");
                sbc.ColumnMappings.Add("guishudi", "GuishuDi");
                sbc.ColumnMappings.Add("telType", "LeiXing");
                sbc.ColumnMappings.Add("quhao", "quHao");
                sbc.WriteToServer(table);
               }
            MessageBox.Show("导入完成");

        }
    }
}
