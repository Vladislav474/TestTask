using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;


namespace TestTask
{
    enum TypeTab 
    {
        Common,
        Add
     };

    class MyTimer : INotifyPropertyChanged
    {
        private TypeTab type;
        private string number;
        private string timeCreate;
        private string time = "00:00.00";
        private string text;

        public string visibilityDrop = "Visible";
        public string visibilityStart = "Visible";
        public string visibilityStop = "Hidden";
        public string visibilityContinue = "Hidden";
        public string visibilityReset = "Hidden";
        public bool enableReset = false;

        private RelayCommand commandStart;
        private RelayCommand commandStop;
        private RelayCommand commandReset;
        private RelayCommand commandContinue;
        private RelayCommand commandDropTimer;
        public RelayCommand CommandDropTimer
        {
            get
            {
                return commandDropTimer ??
                  (commandDropTimer = new RelayCommand(obj => DropTimer(obj), (obj) => obj != null));
            }
        }
        public RelayCommand CommandStart
        {
            get
            {
                return commandStart ??
                  (commandStart = new RelayCommand(obj => StartTimer()));
            }
        }
        public RelayCommand CommandStop
        {
            get
            {
                return commandStop ??
                  (commandStop = new RelayCommand(obj => StopTimer()));
            }
        }
        public RelayCommand CommandReset
        {
            get
            {
                return commandReset ??
                  (commandReset = new RelayCommand(obj => ResetTimer()));
            }
        }
        public RelayCommand CommandContinue
        {
            get
            {
                return commandContinue ??
                  (commandContinue = new RelayCommand(obj => ContinueTimer()));
            }
        }

        readonly Stopwatch stopwatch = new Stopwatch();
        readonly TimerCallback tm = new TimerCallback(SetTimer);
        Timer timer;

        private bool StatusTimer { get; set; } = false;
        private static int CountTimers { get; set; }
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }
        public string TimeCreate
        {
            get { return timeCreate; }
            set
            {
                timeCreate = value;
                OnPropertyChanged("TimeCreate");
            }
        }
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }
        public string VisibilityStart
        {
            get { return visibilityStart; }
            set
            {
                visibilityStart = value;
                OnPropertyChanged("VisibilityStart");
            }
        }
        public string VisibilityStop
        {
            get { return visibilityStop; }
            set
            {
                visibilityStop = value;
                OnPropertyChanged("VisibilityStop");
            }
        }
        public string VisibilityContinue
        {
            get { return visibilityContinue; }
            set
            {
                visibilityContinue = value;
                OnPropertyChanged("VisibilityContinue");
            }
        }
        public string VisibilityReset
        {
            get { return visibilityReset; }
            set
            {
                visibilityReset = value;
                OnPropertyChanged("VisibilityReset");
            }
        }
        public string VisibilityDrop
        {
            get { return visibilityDrop; }
            set
            {
                visibilityDrop = value;
                OnPropertyChanged("VisibilityDrop");
            }
        }
        public bool EnableReset
        {
            get { return enableReset; }
            set
            {
                enableReset = value;
                OnPropertyChanged("EnableReset");
            }
        }
        public TypeTab Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public MyTimer(TypeTab type)
        {
            if (type == TypeTab.Add)
            {
                Type = type;
                Text = "+";
                VisibilityDrop = "Hidden";
            }
            else
            {
                CountTimers++;
                Text = "Таймер ";
                Number = CountTimers.ToString();
                TimeCreate = DateTime.Now.ToString("T"); ;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void StartTimer()
        {
            stopwatch.Start();
            timer = new Timer(tm, this, 0, 100);

            VisibilityStart = "Hidden";
            VisibilityStop = VisibilityReset = "Visible";
            StatusTimer = true;
        }
        public void ContinueTimer()
        {
            stopwatch.Start();

            VisibilityStop = "Visible";
            VisibilityContinue = "Hidden";
            EnableReset = false;
        }
        public void StopTimer() {

            stopwatch.Stop();

            VisibilityStop = "Hidden";
            VisibilityContinue = "Visible";
            EnableReset = true;
        }
        public void ResetTimer() {

            stopwatch.Reset();

            VisibilityStart = "Visible";
            VisibilityContinue = VisibilityReset = "Hidden";
            StatusTimer = false;
        }
        private static void SetTimer(object timer)
        {
            MyTimer myTimer = (MyTimer)timer;
            TimeSpan ts = myTimer.stopwatch.Elapsed;
            myTimer.Time = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        private static void DropTimer(object obj)
        {
            if (ApplicationViewModel.Timers.Count == 2) return;
     
            int number = Convert.ToInt32(obj);
            if (ApplicationViewModel.Timers[number - 1].StatusTimer) return;

            try
            {
                ApplicationViewModel.Timers.Remove(ApplicationViewModel.Timers[number - 1]);
            }
            catch { }
            finally
            {
                for (int i = number - 1; i < ApplicationViewModel.Timers.Count - 1; i++)
                    ApplicationViewModel.Timers[i].Number = (i + 1).ToString();

                CountTimers = ApplicationViewModel.Timers.Count - 1;
            }
        }
    }
}
