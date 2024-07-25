using System.Windows.Controls;
using System.Windows.Threading;

namespace LucidTimer
{
    internal class TimeManager
    {
        private TextBlock _secondsTextField;
        public TextBlock SecondsTextField
        {
            get { return _secondsTextField; }
            set { _secondsTextField = value; }
        }

        private TextBlock _minutesTextField;
        public TextBlock MinutesTextField
        {
            get { return _minutesTextField; }
            set { _minutesTextField = value; }
        }

        private TextBlock _hoursTextField;
        public TextBlock HoursTextField
        {
            get { return _hoursTextField; }
            set { _hoursTextField = value; }
        }

        private DispatcherTimer _timerInstance;
        public DispatcherTimer TimerInstance
        {
            get { return _timerInstance; }
            set { _timerInstance = value; }
        }

        private int _timerSeconds;
        public int TimerSeconds
        {
            get { return _timerSeconds; }
            private set { _timerSeconds = value; }
        }

        private int _timerMinutes;
        public int TimerMinutes
        {
            get { return _timerMinutes; }
            private set { _timerMinutes = value; }
        }

        private int _timerHours;
        public int TimerHours
        {
            get { return _timerHours; }
            private set { _timerHours = value; }
        }

        private bool _isTimerStarted;
        public bool IsTimerStarted
        {
            get { return _isTimerStarted; }
            private set { _isTimerStarted = value; }
        }

        public void InitializeTimer()
        {
            TimerInstance.Tick += new EventHandler((sender, e) => TimerTick());
            TimerInstance.Interval = new TimeSpan(0, 0, 1);
        }
        public void ToggleTimer(System.Windows.Controls.Button TimerControl)
        {
            if (TimerInstance == null) return;
            if (!IsTimerStarted)
            {
                TimerInstance.Start();
                IsTimerStarted = true;
                TimerControl.Content = "Стоп";
            }
            else
            {
                TimerInstance.Stop();
                IsTimerStarted = false;
                TimerControl.Content = "Старт";
            }
        }
        public void ResetTimer()
        {
            if (TimerInstance == null) return;

            TimerSeconds = 0;
            TimerMinutes = 0;
            TimerHours = 0;
            if (SecondsTextField != null)
            {
                SecondsTextField.Text = "00";
            }
            if (MinutesTextField != null)
            {
                MinutesTextField.Text = "00";
            }
            if (HoursTextField != null)
            {
                HoursTextField.Text = "00";
            }

            if (IsTimerStarted)
            {
                TimerInstance.Stop();
                TimerInstance.Start();
            }
        }

        private void TimerTick()
        {
            if (TimerInstance == null) return;
            TimerSeconds++;
            if (TimerSeconds == 60)
            {
                TimerSeconds = 0;
                TimerMinutes++;
                if (TimerMinutes == 60)
                {
                    TimerMinutes = 0;
                    TimerHours++;
                }
            }

            string secondsToEnter, minutesToEnter, hoursToEnter;

            if (SecondsTextField != null)
            {
                secondsToEnter = TimerSeconds < 10 ? "0" + TimerSeconds.ToString() : TimerSeconds.ToString();
                SecondsTextField.Text = secondsToEnter;
            }
            if (MinutesTextField != null)
            {
                minutesToEnter = TimerMinutes < 10 ? "0" + TimerMinutes.ToString() : TimerMinutes.ToString();
                MinutesTextField.Text = minutesToEnter;
            }
            if (HoursTextField != null)
            {
                hoursToEnter = TimerHours < 10 ? "0" + TimerHours.ToString() : TimerHours.ToString();
                HoursTextField.Text = hoursToEnter;
            }
        }
    }
}
