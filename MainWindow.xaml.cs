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
        string first = ""; string second = ""; string sign = "";
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in UI.Children) 
                if(c is Button)
                    ((Button)c).Click += Click;
        }
        void Click(object sender, RoutedEventArgs e)
        {
            string s = (string)((Button)e.OriginalSource).Content;
            textBlock.Text += s;
            int x;
            bool isnum = Int32.TryParse(s, out x);
            if (isnum)
            { 
                if (sign == "") first += s; 
                else second += s; 
            }
            else 
            {
                switch (s)
                {
                    case "=": 
                        Operation();
                        Result.Text += second; //что не так???
                        sign = "";
                        break;
                    case "%":
                        Operation();
                        int num = Int32.Parse(second);
                        second = (num / 100).ToString() + "%";
                        Result.Text += second;
                        sign = "";
                        break;
                    case "CE":
                        first = "";
                        second = "";
                        sign = "";
                        Result.Text = "";
                        break;
                    case "C":
                        second = "";
                        Result.Text = first + sign;
                        break;
                    case "Delete":
                        if (second.Length != 0)
                            second = second.Remove(second.Length - 1);
                        break;
                    default:
                        if (second != "")
                        {
                            Operation();
                            first = second;
                            second = "";
                        }
                        sign = s;
                        break;
                }
            }
        }
        void Operation()
        {
            int num1 = Int32.Parse(first);
            int num2 = Int32.Parse(second);
            switch (sign)
            {
                case "+":
                    second = (num1 + num2).ToString();
                    break;
                case "-":
                    second = (num1 - num2).ToString();
                    break;
                case "*":
                    second = (num1 * num2).ToString();
                    break;
                case "/":
                    second = (num1 / num2).ToString();
                    break;
            }
        }
    }
}
