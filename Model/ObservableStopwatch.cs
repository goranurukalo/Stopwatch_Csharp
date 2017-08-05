using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;



namespace Stopwatch.Model
{
    
    public class ObservableStopwatch : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
      
        public System.Diagnostics.Stopwatch Stopwatch { get; set; }

        public ObservableStopwatch()
        {
            this.Stopwatch = new System.Diagnostics.Stopwatch();
            Thread thread = new Thread(NotifyingMethod);
            thread.IsBackground = true;
            thread.Start();
        }        

        void NotifyingMethod()
        {
            while (true)
            {
                Thread.Sleep(40);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Elapsed"));
            }
        }

        
        public TimeSpan Elapsed
        {
            get
            {
                return this.Stopwatch.Elapsed;
            }
        }
        
        public bool IsRunning
        {
            get
            {
                return this.Stopwatch.IsRunning;
            }
        }

        public void Start()
        {
            if (!this.IsRunning)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsRunning"));
            this.Stopwatch.Start();
        }

        public void Stop()
        {
            if (this.IsRunning)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsRunning"));
            this.Stopwatch.Stop();
        }
        public void Reset()
        {
            if (this.IsRunning)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsRunning"));
            this.Stopwatch.Reset();
        }
        public void Restart()
        {
            if (!this.IsRunning)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsRunning"));
            this.Stopwatch.Restart();
        }
    }
}
