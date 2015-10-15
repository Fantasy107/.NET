using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 登陆
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           if(txtUserName.Text.Length<=0)
           {
               MessageBox.Show("用户名不能为空");
           }

            if(txtPassword.Password.Length<=0)
            {
                MessageBox.Show("密码不能为空");
            }

            DataTable table = SqlHelper.ExecuteDataset("select * from T_User where UserName=@Username", 
                new SqlParameter("@UserName", txtUserName.Text));
            
        
            if(table.Rows.Count<=0)
              {
                  MessageBox.Show("用户名不存在！");
              }

            if(table.Rows.Count>1)
            {
                throw new Exception("用户名重复");
            }

            DataRow row = table.Rows[0];
            string dbPassword =(string)row["Password"];
            long id = (long)row["Id"];
            int errorTimes = (int)row["ErrorTimes"];
            if(errorTimes>=3)
            {
                MessageBox.Show("输入次数太多，账户被锁定");
                return;
            }

            if(dbPassword!=txtPassword.Password)
            {
                MessageBox.Show(" 密码错误");
                SqlHelper.ExecuteNonQuery
                    ("update T_User set ErrorTimes=ErrorTimes+1 where Id=@id",new SqlParameter("@id",id));
            }
            else
            {
                MessageBox.Show("登陆成功");
            }
        }
    }
}
