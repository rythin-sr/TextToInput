using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextToInput
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        System.Timers.Timer disappearTimer = new System.Timers.Timer(5000);
        public MainWindow()
        {
            InitializeComponent();
            labelCopySuccess.Visibility = Visibility.Hidden;
            
        }

        private void btnCopyText_Click(object sender, RoutedEventArgs e)
        {
            ConvertTextToInput(tbInput.Text);
            labelCopySuccess.Visibility = Visibility.Visible;
            disappearTimer.Elapsed += hideSuccessLabel;
            disappearTimer.Start();
        }

        public void ConvertTextToInput(string textToConvert)
        {
            byte[] convertedInputs = Encoding.ASCII.GetBytes(textToConvert);

            //temp
            Array.Reverse(convertedInputs);

            string output;

            if (cbEnterLastInput.IsChecked == true)
            {
                output = "|Kff0d:";
            }
            else
            {
                output = "|K";
            }

            foreach (byte b in convertedInputs)
            {
                output += b.ToString("x") + ":";
            }

            while (output.Contains("20:"))
            {
                if (cbIncludeSpaces.IsChecked == false)
                {
                    output = output.Remove(output.IndexOf("20:"), 3);
                }
                else
                {
                    break;
                }
            }

            output = output.Remove(output.Length - 1, 1);
            output += "|";

            Clipboard.SetText(output);
        }

        private void labelSpacebar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cbIncludeSpaces.IsChecked = !cbIncludeSpaces.IsChecked;
        }

        private void labelEnterText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cbEnterLastInput.IsChecked = !cbEnterLastInput.IsChecked;
        }

        public void hideSuccessLabel(Object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                labelCopySuccess.Visibility = Visibility.Hidden;
            });
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbInput.Clear();
            tbInput.Focus();
        }

        private void tbInput_GotFocus(object sender, RoutedEventArgs e)
        {
            labelHintText.Visibility = Visibility.Hidden;
        }

        private void tbInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbInput.Text))
            {
                labelHintText.Visibility= Visibility.Visible;
            }
        }
    }
}
