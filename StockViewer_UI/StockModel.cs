using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StockViewer_UI
{
    public class StockModel //: INotifyPropertyChanged
    {
        private string companyName;
        private List<DailyStockValue> _stockvalues;
        public event PropertyChangedEventHandler PropertyChanged;
        

        private ObservableCollection<DailyStockValue> _stockValuesByDate = new ObservableCollection<DailyStockValue>();
        public string CSymbol { get; private set; }
        public double Price { get; private set; }
        private DateTime? _date;

      
        public IEnumerable<DailyStockValue> DailyStockValues => _stockValuesByDate;

        public DateTime? Date {

            get { return _date; }
            set
            {
                _date = value;

                //***3 ways to update datagrid on selected date 1. Call either BindValues method 
                //                                              2.RaisePropertyChanged to fill datagrid for selected date ****
                //                                              3.Raise event from StockViewByCompany.xaml.cs
                
                BindValues();
                // RaisePropertyChanged(nameof(Date));

            }

        }

        private void BindValues()
        {
            if (_date.HasValue) {
                foreach (DailyStockValue dsv in _stockvalues) {
                    string sdate = _date.Value.ToShortDateString();
                    if (sdate == dsv.Date)
                    {
                        _stockValuesByDate.Add(dsv);

                    }

                }
            } 

        }

        public StockModel(string companyName, IEnumerable<DailyStockValue> values)
        {
            this.companyName = companyName;
            string newstr = companyName.ElementAt(0).ToString().ToUpper();
            CSymbol = newstr + companyName.Substring(1);
           _stockvalues = new List<DailyStockValue>(values);
            Price = _stockvalues[0].close;


        }

        //protected void RaisePropertyChanged(params string[] names)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        foreach (string name in names)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs(name));
        //            if (_date.HasValue)
        //            {
        //                foreach (DailyStockValue dsv in _stockvalues)
        //                {
        //                    string sdate = _date.Value.ToShortDateString();
        //                    if (sdate == dsv.Date)
        //                    {
        //                        _stockValuesByDate.Add(dsv);

        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

    }
}
