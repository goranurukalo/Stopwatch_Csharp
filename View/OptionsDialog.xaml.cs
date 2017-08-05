using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Stopwatch.View
{
    /// <summary>
    /// Interaction logic for OptionsDialog.xaml
    /// </summary>
    public partial class OptionsDialog : Window
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        // Svojstva za pristup vrednostima kontrolau Options dijalogu
        public bool StartStopInSplitTimes
        {
            get
            {
                return this.chbStartStopInSplitTimes.IsChecked ?? false;
            }
            set
            {
                this.chbStartStopInSplitTimes.IsChecked = value;
            }
        }

        public Color? DisplayForeground
        {
            get
            {
                return this.clpDisplayForeground.SelectedColor;
            }
            set
            {
                this.clpDisplayForeground.SelectedColor = value;
            }
        }

        public Color? DisplayBackground
        {
            get
            {
                return this.clpDisplayBackground.SelectedColor;
            }
            set
            {
                this.clpDisplayBackground.SelectedColor = value;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {           
            // Svojstvo DialogResult se postavlja na true da bi se
            // u glavnom prozoru signaliziralo da je dijalog zatvoren
            // dugmetom OK. Postavljanjem ovog svojstva istovremeno se i zatvara dijalog.
            this.DialogResult = true;
        }
    }
}
