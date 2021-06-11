using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace TestTask
{
    class ApplicationViewModel
    {
        private const int MAX_COUNT_TIMERS = 10;

        private RelayCommand commandDropTimer;
        public RelayCommand CommandDropTimer
        {
            get
            {
                return commandDropTimer ??
                  (commandDropTimer = new RelayCommand(obj => DropTimer()));
            }
        }

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
        private MyTimer selectedTimer;
        public static ObservableCollection<MyTimer> Timers { get; set; }
        public MyTimer SelectedTimer
        {
            get { return selectedTimer; }
            set
            {
                if (value == null)
                {
                    selectedTimer = Timers[Timers.Count - 2];
                    OnPropertyChanged("SelectedTimer");
                }
                else
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

        public void DropTimer()
        {
            Console.WriteLine("F");
        }

    }
}
