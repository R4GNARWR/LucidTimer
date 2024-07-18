using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LucidTimer
{
    internal class TimeManager
    {
        public TextBlock SecondsTextField { get; set; }
        public TextBlock MinutesTextField { get; set; }
        public TextBlock HoursTextField { get; set; }

        public DispatcherTimer TimerInstance { get; set; }
        public int TimerSeconds { get; private set; }
        public int TimerMinutes { get; private set; }
        public int TimerHours { get; private set; }
        public bool IsTimerStarted { get; private set; }

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
            if(SecondsTextField != null)
            {
                SecondsTextField.Text = "00";
            }
            if(MinutesTextField != null)
            {
                MinutesTextField.Text = "00";
            }
            if(HoursTextField != null)
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

            if(SecondsTextField != null)
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
