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

        //Обработчики кнопок
        private void ClickAddNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = (string)((Button)e.OriginalSource).Content;

                IsPreviousOperEqual();

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
        private void ClickOperation(object sender, RoutedEventArgs e)
        {
            try
            {
                string chosenSign = (string)((Button)e.OriginalSource).Content;
                CheckExcessZeroOrDot();

                if (sign.Equals(""))
                {
                    //При выборе первой операции записываем текущее число в результат,
                    //получаем знак выбранной операции, а текущее число делаем равным нулю
                    if (!chosenSign.Equals("="))
                    {
                        total = current;
                        sign = chosenSign;
                        current = "0";
                        result.Content = total + sign;
                        input.Content = current;
                    }
                }
                else
                {
                    if (chosenSign.Equals("="))
                    {
                        //Если после выбора предыдущей операции нажато "=", выполняем операцию через "=" (с очисткой полей)
                        Equality(chosenSign);
                    }
                    else
                    {
                        //Если выполняется несколько операций подряд, то только постоянно перезаписываем ввод и вывод (без очистки полей)
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
        private void CE_Click(object sender, RoutedEventArgs e)
        {
            //Очищаем текущее число
            current = "0";
            //Если операция выполнялась через равно, то очищаем и остальные поля
            if (sign.Equals("="))
            {
                total = "";
                sign = "";
            }
            input.Content = current;
            result.Content = total + sign;
        }
        private void C_Click(object sender, RoutedEventArgs e)
        {
            //Очищаем все поля
            current = "0";
            sign = "";
            total = "";
            result.Content = total + sign;
            input.Content = current;
        }
        private void DLT_CLick(object sender, RoutedEventArgs e)
        {
            //Если операция выполнялась через "=", то просто очищаем все поля
            if (!IsPreviousOperEqual())
            {
                //В противном случае убираем последнюю цифру текущего числа
                current = current.Substring(0, current.Length - 1);
                //Чтобы не было багов с null-числом 
                if (current.Equals(""))
                    current = "0";
                input.Content = current;
            }
        }
        private void Dot(object sender, RoutedEventArgs e)
        {
            try
            {
                IsPreviousOperEqual(); //Чтобы после выполнения через "=" поля очищались

                if (!current.Contains(','))
                    current += ",";
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
        private void Opposite(object sender, RoutedEventArgs e)
        {
            double num = double.Parse(current);
            num = 1 / num;
            current = num.ToString();
            input.Content = current;
        }
        private void Square(object sender, RoutedEventArgs e)
        {
            double num = double.Parse(current);
            num = Math.Pow(num, 2);
            current = num.ToString();
            input.Content = current;
        }
        private void Sqrt(object sender, RoutedEventArgs e)
        {
            double num = double.Parse(current);
            num = Math.Sqrt(num);
            current = num.ToString();
            input.Content = current;
        }
        private void GetPercent(object sender, RoutedEventArgs e)
        {
            double num = double.Parse(current);
            num /= 100;
            current = num.ToString();
            input.Content = current;
        }


        //Внутриклассовые методы для вычислений и корректного вывода
        void Equality(string chosenSign)
        {
            //Вывод результата операции при нажатии на "="
            result.Content = total + sign + current + chosenSign;
            DoOperation();
            input.Content = current;
            sign = chosenSign;
            //Число current не устанавливается на ноль, т.к. 
            //нужно чтобы можно было несколько операций подряд выполнять
            //(установка текущего числа на 0 есть в функциях Dot и ClickNumber)
        }
        void DoOperation()
        {
            try
            {
                double num1 = double.Parse(total);
                double num2 = double.Parse(current);

                switch (sign)
                {
                    case "+":
                        current = (num1 + num2).ToString();
                        break;
                    case "-":
                        current = (num1 - num2).ToString();
                        break;
                    case "*":
                        current = (num1 * num2).ToString();
                        break;
                    case "/":
                        current = (num1 / num2).ToString();
                        break;
                }
            }
            catch (Exception exc)
            {
                //На случай, если будут непредвиденные баги:
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
            //Убираем лишние нули в конце десятичной дроби и запятую без чисел после неё,
            //для того чтобы число корректно выводилось в поле result (и хранилось в переменной total)
            while (current[current.Length - 1] == '0' || current[current.Length - 1] == ',')
                if (current.Contains(',')) current = current.Substring(0, current.Length - 1);
                else break;

        }
        bool IsPreviousOperEqual()
        {
            if (sign.Equals("="))
            {
                current = "0";
                result.Content = "";
                total = "";
                sign = "";
                return true;
            }
            return false;
        }
    }
}
