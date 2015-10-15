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

namespace 省市县联动查询案例
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable table = SqlHelper.ExecuteDataset
                ("select * from AreaFull where AreaPid=@AreaPid", new SqlParameter("@AreaPid", "0"));
         List<Area> ListProv = new List<Area>();
            foreach (DataRow row in table.Rows)
         {
             Area area = new Area();
             area.AreaId =(int) row["AreaId"];
             area.AreaName = (string)row["AreaName"];
             ListProv.Add(area);
               
         }
            lbProv.ItemsSource = ListProv;
        }

        private void listProv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //取出被选中的项
            Area areaProv = (Area)lbProv.SelectedItem;
            
            List<Area> ListCity = new List<Area>();
           DataTable table = SqlHelper.ExecuteDataset
               ("select * from AreaFull where AreaPid=@AreaPid", new SqlParameter("@AreaPid",areaProv.AreaId));

           foreach (DataRow row in table.Rows)
           {
               Area area = new Area();
               area.AreaId = (int)row["AreaId"];
               area.AreaName = (string)row["AreaName"];
               ListCity.Add(area);
           }
           lbCity.ItemsSource = ListCity;
        }

        private void lbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area areaCity = (Area)lbCity.SelectedItem;

            List<Area> ListCountry = new List<Area>();
           DataTable table = SqlHelper.ExecuteDataset
                ("Select * from AreaFull where AreaPid=@AreaPid", new SqlParameter("@AreaPid", areaCity.AreaId));
           foreach (DataRow row in table.Rows)
           {
               Area area = new Area();
               area.AreaId = (int)row["AreaId"];
               area.AreaName = (string)row["AreaName"];
               ListCountry.Add(area);
           }
           lbCountry.ItemsSource = ListCountry;
        }
    }
}
