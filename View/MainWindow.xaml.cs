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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Stopwatch.Model;
using Stopwatch.Properties;
using Microsoft.Win32;

namespace Stopwatch.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private ObservableStopwatch stopwatch = new ObservableStopwatch();
        private SplitTimes splitTimes = new SplitTimes();


        private bool startStopInSplitTimes;
        private string timeFormat = @"hh\:mm\:ss\.fff";       

        public ObservableStopwatch Stopwatch
        {
            get
            {
                return stopwatch;
            }
        }

        public SplitTimes SplitTimes
        {
            get
            {
                return splitTimes;
            }
            
        }

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = this;
            
            this.Left = Settings.Default.MainWindowLeft;
            this.Top = Settings.Default.MainWindowTop;
            this.Width = Settings.Default.MainWindowWidth;
            this.Height = Settings.Default.MainWindowHeight;
            this.WindowState = Settings.Default.MainWindowState;
            this.startStopInSplitTimes = Settings.Default.StartStopInSplitTimes;
            this.txtDisplay.Foreground = Settings.Default.DisplayForeground;
            this.grdDisplay.Background = Settings.Default.DisplayBackground;
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.Stopwatch.Start();
            if (this.startStopInSplitTimes)
                AddTimeToSplitTimes();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTimeToSplitTimes();
        }

        private void AddTimeToSplitTimes()
        {
            TimeSpan elapsed = this.Stopwatch.Elapsed;
            SplitTimes.Items.Add(elapsed);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.Stopwatch.Stop();
            if (this.startStopInSplitTimes)
                AddTimeToSplitTimes();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.Stopwatch.Reset();
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            Settings.Default.MainWindowLeft = this.Left;
            Settings.Default.MainWindowTop = this.Top;
            Settings.Default.MainWindowWidth = this.Width;
            Settings.Default.MainWindowHeight = this.Height;            
            Settings.Default.MainWindowState = this.WindowState;
            Settings.Default.StartStopInSplitTimes = this.startStopInSplitTimes;
            
            SolidColorBrush colorBrush = this.txtDisplay.Foreground as SolidColorBrush;
            if (colorBrush != null)
                Settings.Default.DisplayForeground = colorBrush;
            colorBrush = this.grdDisplay.Background as SolidColorBrush;
            if (colorBrush != null)
                Settings.Default.DisplayBackground = colorBrush;
            
            Settings.Default.Save();
        }

        private void miHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog dialog = new View.AboutDialog();
            dialog.ShowDialog();
        }

        private void miToolsOptions_Click(object sender, RoutedEventArgs e)
        {
            OptionsDialog dialog = new OptionsDialog();            
            SolidColorBrush colorBrush = this.txtDisplay.Foreground as SolidColorBrush;
            dialog.StartStopInSplitTimes = startStopInSplitTimes;
            if (colorBrush != null)
                dialog.DisplayForeground = colorBrush.Color;
            colorBrush = this.grdDisplay.Background as SolidColorBrush;
            if (colorBrush != null)
                dialog.DisplayBackground = colorBrush.Color;
            if (dialog.ShowDialog() ?? false)
            {
                this.startStopInSplitTimes = dialog.StartStopInSplitTimes;
                if (dialog.DisplayForeground != null)
                    this.txtDisplay.Foreground = new SolidColorBrush(dialog.DisplayForeground.Value);
                if (dialog.DisplayBackground != null)
                    this.grdDisplay.Background = new SolidColorBrush(dialog.DisplayBackground.Value);
            }
        }

        private void miFileExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files|*.txt|All files|*.*";
            if (dialog.ShowDialog() ?? false)
            {                
                this.SplitTimes.WriteToTextFile(dialog.FileName);
            }
        }
    }
}
