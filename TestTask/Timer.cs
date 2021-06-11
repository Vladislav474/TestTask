using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private string time;
        private string text;

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
                  (commandDropTimer = new RelayCommand(obj => DropTimer(obj)));
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

        Stopwatch stopwatch = new Stopwatch();
        TimerCallback tm = new TimerCallback(SetTimer);
        Timer timer;
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
        }
        private static void SetTimer(object timer)
        {
            MyTimer myTimer = (MyTimer)timer;
            TimeSpan ts = myTimer.stopwatch.Elapsed;
            myTimer.Time = String.Format("{0:00}:{1:00}.{2:00}",ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        private static void DropTimer(object obj)
        {
            //ApplicationViewModel.Timers.Remove(ApplicationViewModel.se)
            
        }
    }
}
