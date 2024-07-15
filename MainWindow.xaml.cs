using System.Windows;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.IO;

namespace LucidTimer
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer mainTimer = new DispatcherTimer();
        private System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon();
     
        private int mainTimerSeconds = 0;
        private int mainTimerMinutes = 0;
        private int mainTimerHours = 0;
        private bool isTimerStarted = false;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimerData();
            InitializeTrayIcon();
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();
            base.OnStateChanged(e);
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string messageBoxText = "Вы хотите закрыть приложение?";
            string caption = "Предупреждение";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if(result == MessageBoxResult.No)
            {
                this.Hide();
                base.OnStateChanged(e);
                e.Cancel = true;
            } else
            {
                e.Cancel = false;
            }

        }
        private void InitializeTimerData()
        {
            mainTimer.Tick += new EventHandler(MainTimer_Tick);
            mainTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void InitializeTrayIcon()
        {
            string projectPath = System.IO.Path.GetFullPath(@"..\..\..\");
            string iconPath = System.IO.Path.Combine(projectPath, @"Resources\Icons\Main.ico");
            trayIcon.Icon = new Icon(iconPath);
            trayIcon.Visible = true;
            trayIcon.DoubleClick += new EventHandler(TrayIcon_DoubleClick);
        }

        private void TrayIcon_DoubleClick(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            if(!isTimerStarted)
            {
                mainTimer.Start();
                isTimerStarted = true;
                StartTimer.Content = "Стоп";
            } else
            {
                mainTimer.Stop();
                isTimerStarted = false;
                StartTimer.Content = "Старт";
            }
        }

        private void MainTimer_Tick(object? sender, EventArgs e)
        { 
            mainTimerSeconds++;
            if (mainTimerSeconds == 60)
            {
                mainTimerSeconds = 0;
                mainTimerMinutes++;
                if (mainTimerMinutes == 60)
                {
                    mainTimerMinutes = 0;
                    mainTimerHours++;
                }
            }
            string secondsToEnter, minutesToEnter, hoursToEnter;

            secondsToEnter = mainTimerSeconds < 10 ? "0" + mainTimerSeconds.ToString() : mainTimerSeconds.ToString();
            minutesToEnter = mainTimerMinutes < 10 ? "0" + mainTimerMinutes.ToString() : mainTimerMinutes.ToString();
            hoursToEnter = mainTimerHours < 10 ? "0" + mainTimerHours.ToString() : mainTimerHours.ToString();

            tbMainTimerSeconds.Text = secondsToEnter;
            tbMainTimerMinutes.Text = minutesToEnter;
            tbMainTimerHours.Text = hoursToEnter;
        }


    }
}