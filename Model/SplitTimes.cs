using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch.Model
{
    public class SplitTimes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<TimeSpan> items = new ObservableCollection<TimeSpan>();

        public ObservableCollection<TimeSpan> Items
        {
            get
            {
                return items;
            }
        }

        public void WriteToTextFile(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TimeSpan item in this.items)
                sb.AppendLine(item.ToString());
            File.WriteAllText(fileName, sb.ToString());
        }
    }
}
