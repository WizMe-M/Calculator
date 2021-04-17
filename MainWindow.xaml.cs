using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string current = "0"; string sign = ""; string total = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickOperation(object sender, RoutedEventArgs e)
        {
            try
            {
                string chosenSign = (string)((Button)e.OriginalSource).Content;
                CheckExcessZeroOrDot();

                if (sign.Equals(""))
                {
                    total = current;
                    sign = chosenSign;
                    current = "0";

                    result.Content = total + sign;
                    input.Content = current;
                }
                else
                {
                    if (chosenSign.Equals("="))
                    {
                        Equality(chosenSign);
                    }
                    else
                    {
                        DoOperation();
                        result.Content = current + chosenSign;
                        input.Content = "0";
                        total = current;
                        sign = chosenSign;
                        current = "0";
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Dot(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!current.Contains(','))
                    current += ",";

                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ClickAddNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = (string)((Button)e.OriginalSource).Content;

                if (sign.Equals("="))
                {
                    current = "0";
                    result.Content = "";
                    total = "";
                    sign = "";
                }

                if (current[0] == '0')
                {
                    if (current.Length == 1)
                    {

                        current = s;
                    }
                    else current += s;

                }
                else current += s;
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Neg(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse(current);
                current = (-num).ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        void Equality(string chosenSign)
        {
            result.Content = total + sign + current + chosenSign;
            DoOperation();
            input.Content = current;
            //current = "0";
            sign = chosenSign;
        }
        void DoOperation()
        {
            try
            {
                double num1 = double.Parse(total);
                double num2 = double.Parse(current);
                // И выполняем операцию
                switch (sign)
                {
                    case "+":
                        current = (num1 + num2).ToString();
                        break;
                    case "-":
                        current = (num1 - num2).ToString();
                        break;
                    case "*":
                        //double x = Math.Round(num1 * num2, 5);
                        current = (num1 * num2).ToString();
                        break;
                    case "/":
                        current = (num1 / num2).ToString();
                        break;
                }
            }
            catch (Exception exc)
            {
                current = "";
                sign = "";
                total = "";
                result.Content = "";
                input.Content = "";
                MessageBox.Show(exc.Message);
            }
        }
        void CheckExcessZeroOrDot()
        {
            while (current[current.Length - 1] == '0' || current[current.Length - 1] == ',')
                if (current.Contains(',')) current = current.Substring(0, current.Length - 1);
                else break;

        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            if (sign.Equals("="))
            {
                total = "";
                sign = "";
            }
            current = "0";
            input.Content = current;
            result.Content = total + sign;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            current = "0";
            sign = "";
            total = "";
            result.Content = total + sign;
            input.Content = current;
        }

        private void DLT_CLick(object sender, RoutedEventArgs e)
        {
            if (sign.Equals("="))
            {
                total = "";
                sign = "";
                result.Content = "";
                current = "0";
            }
            else
            {
                current = current.Substring(0, current.Length - 1);
                if (current.Equals(""))
                    current = "0";
                input.Content = current;
            }
        }

        //private void Opposite(object sender, RoutedEventArgs e)
        //{
        //    double num = double.Parse(current);
        //    num = 1 / num;
        //    current = num.ToString();
        //    input.Content = current;
        //}

        private void Square(object sender, RoutedEventArgs e)
        {

        }
    }
}
