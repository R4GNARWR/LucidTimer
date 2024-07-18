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
        TimeManager mainTimeManager;
        private readonly DispatcherTimer mainTimer = new DispatcherTimer();

        private System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
            mainTimeManager = new TimeManager
            {
                TimerInstance = mainTimer,
                SecondsTextField = tbMainTimerSeconds,
                MinutesTextField = tbMainTimerMinutes,
                HoursTextField = tbMainTimerHours
            };
            mainTimeManager.InitializeTimer();
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
            mainTimeManager.ToggleTimer(bStartTimer);
        }

        private void bResetTimer_Click(object sender, RoutedEventArgs e)
        {
            mainTimeManager.ResetTimer();
        }
    }
}