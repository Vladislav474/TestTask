using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestTask
{
    class ApplicationViewModel
    {
        private const int MAX_COUNT_TIMERS = 10;

        public static bool fl = true;
        public string visibilityReset = "Hidden";
        public string VisibilityReset
        {
            get { return visibilityReset; }
            set
            {
                visibilityReset = value;
                OnPropertyChanged("VisibilityReset");
            }
        }
        private static MyTimer selectedTimer;
        public static ObservableCollection<MyTimer> Timers { get; set; }
        public MyTimer SelectedTimer
        {
            get { return selectedTimer; }
            set
            {
                if (value.Type == TypeTab.Add && fl)
                {
                    if (Timers.Count == MAX_COUNT_TIMERS + 1) return;
                    fl = false;
                    MyTimer newTimer = new MyTimer(TypeTab.Common);
                    Timers.Insert(Timers.Count - 1, newTimer);
                    fl = true;
                    selectedTimer = newTimer;
                    OnPropertyChanged("SelectedTimer");
                }
                else
                {
                    selectedTimer = value;
                    OnPropertyChanged("SelectedTimer");
                }
              
            }
        }
        public ApplicationViewModel()
        {
            Timers = new ObservableCollection<MyTimer>
            {
                new MyTimer(TypeTab.Common),
                new MyTimer(TypeTab.Add),
            };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
