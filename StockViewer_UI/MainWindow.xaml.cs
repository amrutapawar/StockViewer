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
        GetStockValues getStock = new GetStockValues();
        string symbol;
        string[] sym_comp;
        


        public MainWindow()
        {
            InitializeComponent();
           
            not_found.Visibility = Visibility.Hidden;
            //price_label.Visibility = Visibility.Hidden;
            //sym.Visibility = Visibility.Hidden;
            //price.Visibility = Visibility.Hidden;
            //date.Visibility = Visibility.Hidden;
            //dg.Visibility = Visibility.Hidden;
            
            getStock.DataReady += file_DataReady ;

        }

        private void file_DataReady(object sender, EventArgs e)
        {
            //   GetStockValues.Json_FilePath = getStock.data;
            //File.WriteAllText(GetStockValues.Json_FilePath, getStock.data);
            if (File.ReadAllText(GetStockValues.Json_FilePath) == getStock.data)
            {
                string done = "success";
                Debug.WriteLine(done);
            //    string symbol = getStock.GetSymbol(comp_name.Text.ToLower().ToString());
                //string[] sym_comp = symbol.Split(',');
                List<GetStockValues> parsed_json = getStock.Parse_Json();
                Label sym = new Label();
                Label price_label = new Label();
                Label price = new Label();
                DatePicker date = new DatePicker();
                date.Width = 200;
                date.Height = 30;
                
                
                sym.Visibility = Visibility.Visible;
                sym.Content = sym_comp[1].ToString();
                price_label.Visibility = Visibility.Visible;

                price.Visibility = Visibility.Visible;
                price_label.Content = "Current Stock price :";
                price.Content = parsed_json[0].close;
                date.Visibility = Visibility.Visible;
                //      dg.Visibility = Visibility.Visible;
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                Button btn = new Button();
                sp.Background = Brushes.White;
                sp.Height = 450;
                sp.Width = 700;

                date.SelectedDateChanged += GetStockByDate;

                sp.Children.Add(sym);
                sp.Children.Add(price_label);
                sp.Children.Add(price);
                sp.Children.Add(date);
            //    sp.Children.Add(dg);
             //   sp.Visibility = Visibility.Visible;
                
                grid.Children.Add(sp);
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        { 
            
            symbol = getStock.GetSymbol(comp_name.Text.ToLower().ToString());
            if (symbol == "not found") {
                not_found.Visibility = Visibility.Visible;
                
            }
            else
            {
                sym_comp = symbol.Split(',');
                getStock.Get_Json(sym_comp[0]);

            }

       //     dg.Visibility = Visibility.Hidden; 
        }

        private void GetStockByDate(object sender, SelectionChangedEventArgs e)
        {
            List<GetStockValues> parsed_json1 = getStock.Parse_Json();
            
            var selected_date = sender as DatePicker;
            DateTime? date1 = selected_date.SelectedDate;

            for (int i = 0; i < parsed_json1.Count; i++)
            {
                if (date1 == null)
                {
                  //  date_not_found.Visibility = Visibility.Visible;
                    
                }
                
                else if (date1.Value.ToShortDateString() == (parsed_json1[i].Date))
                {
                    //Table tb = new Table();
                    //DataGrid dg = new DataGrid();
                    // for (int col = 0; col < 6; col++)
                    // {
                    ////     tb.Columns.Add(new TableColumn());

                    // }
                    //TableRow tr = new TableRow();
                    //tr.Cells.Add(new TableCell (new Paragraph(new Run(parsed_json[i].Date.ToShortDateString()))));
                    //TableRowGroup tg = new TableRowGroup();
                    //tg.Rows.Add(tr);
                    //tb.RowGroups.Add(tg);
                    DataGrid dg = new DataGrid();
                    dg.Visibility = Visibility.Visible;
                    
                    collection.Add(parsed_json1[i]);
                    Debug.WriteLine(collection);
                    
                    dg.ItemsSource = collection;

                }


            }
        }
    }
}
