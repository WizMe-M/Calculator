using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using Calculator;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для InzhenernyiWindow.xaml
    /// </summary>
    public partial class InzhenernyiWindow : Window
    {
        string lastbutton = "";
        string current = "0";
        string sign = "";
        string total = "";
        int bracketCount = 0;
        public InzhenernyiWindow()
        {
            InitializeComponent();
        }

        void Equality()
        {
            if (bracketCount == 0)
            {
                result.Content += (string)input.Content;

                if (lastbutton == "^")
                {
                    var x = new DataTable().Compute((string)result.Content, null);
                    double num1 = (double)x;
                    double num2 = double.Parse((string)input.Content);
                    input.Content = (Math.Pow(num1, num2)).ToString();
                    result.Content = x.ToString();
                    current = "0";
                }
                else { 
                    var res = new DataTable().Compute((string)result.Content, null);
                    input.Content = res;
                    current = "0";
                }
            }
            else if (bracketCount == 1)
            {
                total += current + ")";
                result.Content = total;
                if (lastbutton == "^")
                {
                    var x = new DataTable().Compute((string)result.Content, null);
                    double num1 = (double)x;
                    double num2 = double.Parse((string)input.Content);
                    input.Content = (Math.Pow(num1, num2)).ToString();
                    result.Content = x.ToString();
                    current = "0";
                }
                else
                {

                    var res = new DataTable().Compute(total + sign + current, null);
                    input.Content = res;
                }
            }
            else MessageBox.Show("Проверьте выражение. Где-то не хватает скобки");
        }

        void DoOperation()
        {
            try
            {
                if (bracketCount == 0)
                {
                    if (lastbutton == "^")
                    {
                        var x = new DataTable().Compute((string)result.Content, null);
                        double num1 = (double)x;
                        double num2 = double.Parse((string)input.Content);
                        input.Content = (Math.Pow(num1, num2)).ToString();
                        result.Content = x.ToString();
                        current = "0";
                    }
                    else
                    {

                        result.Content += current;
                        var res = new DataTable().Compute((string)result.Content, null);
                        current = res.ToString();
                        input.Content = res.ToString();
                    }
                }
                else MessageBox.Show("Проверьте выражение. Где-то не хватает скобки");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        void CheckExcessZeroOrDot()
        {
            while (current[current.Length - 1] == '0' || current[current.Length - 1] == ',')
                if (current.Contains(',')) current = current.Substring(0, current.Length - 1);
                else break;

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
                        Equality();
                    }
                    else
                    {
                        DoOperation();
                        result.Content += chosenSign;
                        input.Content = current;
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

        private void Equality(object sender, RoutedEventArgs e)
        {
            Equality();
        }

        private void ClickAddNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = (string)((Button)e.OriginalSource).Content;

                if (current[0] == '0')
                {
                    if (current.Length == 1)
                        current = s;
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
                double num = double.Parse((string)input.Content);
                current = (-num).ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
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

        private void Opposite(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse((string)input.Content);
                num = 1 / num;
                current = num.ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Square(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse((string)input.Content);
                num = Math.Pow(num, 2);
                current = num.ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Sqrt(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse((string)input.Content);
                num = Math.Sqrt(num);
                current = num.ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Absolute(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = double.Parse((string)input.Content);
                num = Math.Abs(num);
                current = num.ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AddPI(object sender, RoutedEventArgs e)
        {
            current = Math.PI.ToString();
            input.Content = current;
        }

        private void AddE(object sender, RoutedEventArgs e)
        {
            current = Math.E.ToString();
            input.Content = current;
        }

        private void Sinus(object sender, RoutedEventArgs e)
        {
            double gradusy = double.Parse((string)input.Content);
            current = Math.Sin((gradusy / 180d) * Math.PI).ToString();
            input.Content = current;
        }

        private void Cosinus(object sender, RoutedEventArgs e)
        {
            double gradusy = double.Parse((string)input.Content);
            current = Math.Cos((gradusy / 180d) * Math.PI).ToString();
            input.Content = current;
        }

        private void Tangens(object sender, RoutedEventArgs e)
        {
            double gradusy = double.Parse((string)input.Content);
            current = Math.Tan((gradusy / 180d) * Math.PI).ToString();
            input.Content = current;
        }

        private void AddLeftBracket(object sender, RoutedEventArgs e)
        {
            bracketCount++;
            total += "(";
            result.Content += "(";
        }

        private void AddRightBracket(object sender, RoutedEventArgs e)
        {
            if (bracketCount > 0)
            {
                bracketCount--;
                total += (string)input.Content + ")";
                result.Content += (string)input.Content + ")";
                Equality();
            }
        }

        private void Factorial(object sender, RoutedEventArgs e)
        {
            try
            {
                double n = double.Parse((string)input.Content);
                double res = 1;
                if (n % 1 == 0)
                {
                    for (int i = 1; i <= n; i++)
                        res *= i;
                    current = res.ToString();
                    input.Content = current;
                }
                else
                {
                    MessageBox.Show("Введите целое число для вычисления числа (дробные числа не поддерживаются)");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void TenPowX(object sender, RoutedEventArgs e)
        {
            try
            {
                double n = double.Parse((string)input.Content);
                current = Math.Pow(10, n).ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Log(object sender, RoutedEventArgs e)
        {
            try
            {
                double n = double.Parse((string)input.Content);
                current = Math.Log10(n).ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void LogN(object sender, RoutedEventArgs e)
        {
            try
            {
                double n = double.Parse((string)input.Content);
                current = Math.Log(n).ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow != null)
            {
                mainWindow.Close();
            }
        }

        private void Pow(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (bracketCount == 0)
                //{
                //    result.Content += (string)input.Content;
                //    total += (string)input.Content;
                //    lastbutton = "^";
                //    current = "0";

                //}
                //else if (bracketCount == 1)
                //{
                //    total += (string)input.Content + ")";
                //    result.Content = total;
                //}
                //else
                //    MessageBox.Show("Проверьте выражение. Где-то не хватает скобки");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

