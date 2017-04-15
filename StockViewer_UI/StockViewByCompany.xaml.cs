using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StockViewer_UI
{
    /// <summary>
    /// Interaction logic for StockViewByCompany.xaml
    /// </summary>
    public partial class StockViewByCompany : UserControl
    {
        GetStockValues getStock = new GetStockValues();
        ObservableCollection<DailyStockValue> collection = new ObservableCollection<DailyStockValue>();

        public StockViewByCompany()
        {
            InitializeComponent();
           //***datagrid is updated after the event is fired"
         //   date.SelectedDateChanged += GetStockByDate;
        }

        //private void GetStockByDate(object sender, SelectionChangedEventArgs e)
        //{
        //    List<DailyStockValue> parsed_json1 = getStock.Parse_Json();

        //    var selected_date = sender as DatePicker;
        //    DateTime? date1 = selected_date.SelectedDate;

        //    for (int i = 0; i < parsed_json1.Count; i++)
        //    {
        //        if (date1 == null)
        //        {
        //            //  date_not_found.Visibility = Visibility.Visible;

        //        }

        //        else if (date1.Value.ToShortDateString() == (parsed_json1[i].Date))
        //        {
        //            collection.Add(parsed_json1[i]);

        //            dg.ItemsSource = collection;

        //        }


        //    }

        //}
    }
}
