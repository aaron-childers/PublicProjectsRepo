using MouseShaker.Config;
using System.Windows;

namespace MouseShaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MouseShakeConfig _config;
        private System.Timers.Timer _shakeTimer;

        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void LoadConfig()
        {
            _config = MouseShakeConfig.Load("MouseShakeConfig.xml");
            ShakeDistanceTextBox.Text = _config.ShakeDistance.ToString();
            ShakeDurationTextBox.Text = _config.ShakeDurationMilliseconds.ToString();
            IntervalTextBox.Text = _config.IntervalMilliseconds.ToString();
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartStopToggle.IsChecked == true)
            {
                StartStopToggle.Content = "Stop Shaking";
                _shakeTimer = new System.Timers.Timer(_config.IntervalMilliseconds);
                _shakeTimer.Elapsed += (s, args) => MouseMover.ShakeMouse(_config.ShakeDistance, _config.ShakeDurationMilliseconds);
                _shakeTimer.Start();
            }
            else
            {
                StartStopToggle.Content = "Start Shaking";
                _shakeTimer?.Stop();
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate and apply settings
            bool parsedDistance = int.TryParse(ShakeDistanceTextBox.Text, out int shakeDistance);
            bool parsedDuration = int.TryParse(ShakeDurationTextBox.Text, out int shakeDuration);
            bool parsedInterval = int.TryParse(IntervalTextBox.Text, out int interval);

            if(parsedDistance != true && parsedDistance != parsedDuration && parsedDuration != parsedInterval)
            {
                //Log error
                System.Windows.MessageBox.Show("ERROR: Failed to parse configuration data");
                return;
            }

            _config.ShakeDistance = shakeDistance;
            _config.ShakeDurationMilliseconds = shakeDuration;
            _config.IntervalMilliseconds = interval;

            // Save updated config to XML (optional)
            _config.Save("MouseShakerConfig.xml");
        }
    }
}