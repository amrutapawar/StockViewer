using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace StockViewer_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<GetStockValues> collection = new ObservableCollection<GetStockValues>();
        ObservableCollection<StockModel> _stockModels = new ObservableCollection<StockModel>();
        GetStockValues getStock = new GetStockValues();
        string symbol;
        string[] sym_comp;
       

        public string Compname { get; set; }
        public IEnumerable<StockModel> StockModels => _stockModels;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
           
            not_found.Visibility = Visibility.Hidden;
                       
            getStock.DataReady += file_DataReady ;
      

        }

        public void file_DataReady(object sender, EventArgs e)
        {
            
            if (File.ReadAllText(GetStockValues.Json_FilePath).StartsWith("{") )
            {
               
                string symbol = getStock.GetSymbol(comp_name.Text.ToLower().ToString());
                string[] sym_comp = symbol.Split(',');
                //     List<GetStockValues> parsed_json = getStock.Parse_Json();
                StockModel sm = new StockModel(sym_comp[1].ToString(), getStock.StockValues);
                _stockModels.Add(sm);
        //        _stockModels.Add(new StockModel(sym_comp[1].ToString(), getStock.StockValues));
              
                //Label sym = new Label();
                //Label price_label = new Label();
                //Label price = new Label();
                //DatePicker date = new DatePicker();
                //date.Width = 200;
                //date.Height = 25;
                                
             //   sym.Visibility = Visibility.Visible;
            //    sym.Content = sym_comp[1].ToString();
                //price_label.Content = "Current Stock price :";
                //sym.FontSize = 15;
                //sym.FontWeight = FontWeights.Bold;
                //price.Content = parsed_json[0].close;
                //date.Visibility = Visibility.Visible;
               
                
            //    StackPanel sp = new StackPanel();
            //    sp.Orientation = Orientation.Vertical;
          
            //    sp.Background = Brushes.DeepSkyBlue;
            //    sp.Height = 350;
            //    sp.Width = 500;

            //    date.SelectedDateChanged += GetStockByDate;
                    
            ////    StockByDate?.Invoke(sp,EventArgs.Empty);
                
            //    sp.Children.Add(sym);
            //    sp.Children.Add(price_label);
            //    sp.Children.Add(price);
            //    sp.Children.Add(date);
               
            //    UI_SP.Children.Add(sp);
            }

        }

       
        public void button_Click(object sender, RoutedEventArgs e)
        {
            Compname = comp_name.Text.ToLower().ToString();
       //     getStock = new GetStockValues(Compname);
     //       GetStockValues getStockObj = new GetStockValues(Compname);
            symbol = getStock.GetSymbol(Compname);

            if (symbol == "not found") {
                not_found.Visibility = Visibility.Visible;
                
            }
            else
            {
                not_found.Visibility = Visibility.Hidden;
                sym_comp = symbol.Split(',');
                getStock.Get_Json(sym_comp[0]);

            }

      
        }

        //public void GetStockByDate(object sender, SelectionChangedEventArgs e)
        //{
        //    List<GetStockValues> parsed_json1 = getStock.Parse_Json();
            
        //    var selected_date = sender as DatePicker;
        //    DateTime? date1 = selected_date.SelectedDate;

        //    for (int i = 0; i < parsed_json1.Count; i++)
        //    {
        //        if (date1 == null)
        //        {
        //          //  date_not_found.Visibility = Visibility.Visible;
                    
        //        }
                
        //        else if (date1.Value.ToShortDateString() == (parsed_json1[i].Date))
        //        {
                   
        //            DataGrid dg = new DataGrid();
                               
        //            collection.Add(parsed_json1[i]);
        //            Debug.WriteLine("hello");
        //            //datag.Visibility = Visibility.Visible;
        //            //datag.ItemsSource = collection;
                   
        //       //     dg.ItemsSource = collection;

        //        }


        //    }
            
        //}
    }
}
