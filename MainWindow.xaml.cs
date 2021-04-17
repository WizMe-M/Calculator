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
        string first = ""; string second = "";
        string current = "0"; string sign = "";  string total = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        void ClickOperation(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = (string)((Button)e.OriginalSource).Content;
             
                //При нажатии на операцию содержимое текущей
                
                
                
                //input.Content += s;
                bool isnum = Int32.TryParse(s, out _);
                if (isnum)
                {
                    if (sign != "")
                    {
                        total = current + sign;
                        current = "";
                    }
                    if (current == "0")
                        current += s;
                }
                else
                {

                }




                if (isnum)
                {
                    if (sign == "") first += s;
                    else second += s;
                }
                else
                {
                    switch (s)
                    {
                        case "CE":
                            if (sign != "") second = "";
                            first = "";
                            sign = "";
                            input.Content = "";
                            break;
                        case "C":
                            first = "";
                            sign = "";
                            second = "";
                            result.Content = "";
                            input.Content = "";
                            break;
                        case "DLT":
                            if (sign == "")
                                if (first.Length != 0)
                                    first = first.Remove(first.Length - 2, 1);
                                else if (second.Length != 0)
                                    second = second.Remove(second.Length - 2, 1);
                            break;
                        case "+/-":
                            if (sign == "")
                                if (first[0] == '-') first = first.Remove(0, 1);
                                else first = "-" + first;
                            else if (second[0] == '-') second = second.Remove(0, 1);
                            else second = "-" + second;
                            break;
                        case "1/x":
                            if (sign == "")
                            {
                                double d = Double.Parse(first);
                                d = 1 / d;
                                first = d.ToString();
                            }
                            else
                            {
                                double d = Double.Parse(second);
                                d = 1 / d;
                                second = d.ToString();
                            }
                            break;
                        case "x^2":
                            if (first != "" && sign == "")
                                first = Math.Pow(Double.Parse(first), 2).ToString();
                            else
                                second = Math.Pow(Double.Parse(second), 2).ToString();
                            input.Content = first + sign + second;
                            break;
                        case "√x":
                            if (first != "")
                                first = (Math.Sqrt(Double.Parse(first))).ToString();
                            else
                                second = Math.Sqrt(Double.Parse(second)).ToString();
                            input.Content = first + sign + second;
                            break;
                        case "%":
                            if (sign == "")
                                first = (Double.Parse(first) / 10).ToString();
                            else
                                second = (Double.Parse(second) / 10).ToString();
                            break;

                        case "=":
                            Equality(s);
                            break;
                        default:
                            if (second != "")
                            {
                                if (sign != "") Equality(s);
                                else
                                {
                                    DoOperation();
                                    first = second;
                                    second = "";
                                }
                            }
                            sign = s;
                            input.Content = first + s;
                            break;
                    }
                }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        void Equality(string s)
        {
            result.Content = first + sign + second + "=";
            DoOperation();
            input.Content = second;
            if (s != "=") input.Content += s;
            first = second;
            second = "";
        }

        void DoOperation()
        {
            try
            {
                double num1 = double.Parse(first);
                double num2 = double.Parse(second);
                // И выполняем операцию
                switch (sign)
                {
                    case "+":
                        second = (num1 + num2).ToString();
                        break;
                    case "-":
                        second = (num1 - num2).ToString();
                        break;
                    case "*":
                        double x = Math.Round(num1 * num2, 5);
                        second = x.ToString();
                        break;
                    case "/":
                        second = (num1 / num2).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                first = "";
                sign = "";
                second = "";
                result.Content = "";
                input.Content = "";
                MessageBox.Show("Недостаточно операндов и/или нет знака операции.");
            }
        }

        void Dot(object sender, RoutedEventArgs e)
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

        void ClickAddNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = (string)((Button)e.OriginalSource).Content;
                if (current[0] == '0')
                {
                    if (current.Length == 1) current = s;
                    else 
                    //if (current.Contains('.')) 
                        current += s;
                }
                else current += s;
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void Neg(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = Double.Parse(current);
                current = (-num).ToString();
                input.Content = current;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
