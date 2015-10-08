using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MD5_Checker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int TIMER_CALLBACK_INTERVAL = 500;

        private DateTimeOffset timeOfLastChange;

        public MainWindow()
        {
            InitializeComponent();
            timeOfLastChange = DateTimeOffset.UtcNow;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += dispatcherTimer_Tick;
            //timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            CheckInput(null);
        }

        public void CheckInput(object state)
        {
            // hashedTextLabel.Content = $"{DateTimeOffset.UtcNow - timeOfLastChange} > {TimeSpan.FromSeconds(0.5)}";
            if (DateTimeOffset.UtcNow - timeOfLastChange > TimeSpan.FromSeconds(0.5) && !String.IsNullOrWhiteSpace(inputTextBox.Text))
            {
                MD5CryptoServiceProvider md5csp = new MD5CryptoServiceProvider();
                byte[] input = Encoding.Default.GetBytes(inputTextBox.Text);
                //MessageBox.Show(BitConverter.ToString(input));
                byte[] output = md5csp.ComputeHash(input);
                hashedTextLabel.Content = BitConverter.ToString(output).Replace("-","");

            }
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hashedTextLabel.Content = "Computing MD5...";
            timeOfLastChange = DateTimeOffset.UtcNow;
        }
    }
}
