using System;
using System.Text;
using System.Windows;

namespace PasswordKeyGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GeneratePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            int length = int.Parse(PasswordLengthTextBox.Text);
            bool includeLowercase = IncludeLowercaseCheckBox.IsChecked ?? false;
            bool includeUppercase = IncludeUppercaseCheckBox.IsChecked ?? false;
            bool includeNumbers = IncludeNumbersCheckBox.IsChecked ?? false;
            bool includeSymbols = IncludeSymbolsCheckBox.IsChecked ?? false;

            string password = GeneratePassword(length, includeLowercase, includeUppercase, includeNumbers, includeSymbols);
            GeneratedPasswordTextBox.Text = password;
        }

        private void GenerateActivationKeyButton_Click(object sender, RoutedEventArgs e)
        {
            int segments = int.Parse(SegmentsTextBox.Text);
            int segmentLength = int.Parse(SegmentLengthTextBox.Text);

            string activationKey = GenerateActivationKey(segments, segmentLength);
            GeneratedActivationKeyTextBox.Text = activationKey;
        }

        private string GeneratePassword(int length, bool includeLowercase, bool includeUppercase, bool includeNumbers, bool includeSymbols)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "1234567890";
            const string symbolChars = "!@#$%^&*()";

            StringBuilder validChars = new StringBuilder();
            if (includeLowercase) validChars.Append(lowercaseChars);
            if (includeUppercase) validChars.Append(uppercaseChars);
            if (includeNumbers) validChars.Append(numberChars);
            if (includeSymbols) validChars.Append(symbolChars);

            if (validChars.Length == 0)
                throw new ArgumentException("At least one character type must be included.");

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }

            return password.ToString();
        }

        private string GenerateActivationKey(int segments, int segmentLength)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder activationKey = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < segments; i++)
            {
                if (i > 0)
                    activationKey.Append('-');

                for (int j = 0; j < segmentLength; j++)
                {
                    int index = random.Next(validChars.Length);
                    activationKey.Append(validChars[index]);
                }
            }

            return activationKey.ToString();
        }
    }
}