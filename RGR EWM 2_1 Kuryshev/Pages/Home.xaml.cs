using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using FirstFloor.ModernUI.Windows.Controls;

namespace RGR_EWM_2_1_Kuryshev.Pages
{

    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        double a1;
        double b1;
        double c;
        double z;
        string c1;
        ModernDialog dlgException = new ModernDialog()
        {
            Title="Ошибка ввода",
            Content= "Введите корректные данные",
        };
        public Home()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Студент группы БПИ-111\n Курышев Родион Вячеславович\nНовосибирск 2018");
        }
        public void buttonConvert_Click(object sender, RoutedEventArgs e)
        {
            if (textboxFirstNum.Text != String.Empty && textboxSecondNum.Text != String.Empty)
            {
                try
                {
                    string FirstNumber;
                    string SecondNumber;
                    switch (combobox.SelectedIndex)
                    {
                        case 1:
                            FirstNumber = textboxFirstNum.Text;
                            SecondNumber = textboxSecondNum.Text;
                            a1 = TripToDec(FirstNumber);
                            b1 = TripToDec(SecondNumber);
                            c = a1 + b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = c1;
                            break;
                        case 2:
                            FirstNumber = textboxFirstNum.Text;
                            SecondNumber = textboxSecondNum.Text;
                            a1 = TripToDec(FirstNumber);
                            b1 = TripToDec(SecondNumber);
                            c = a1 - b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = Convert.ToString(c1);
                            break;
                        case 3:
                            FirstNumber = textboxFirstNum.Text;
                            SecondNumber = textboxSecondNum.Text;
                            a1 = TripToDec(FirstNumber);
                            b1 = TripToDec(SecondNumber);
                            c = a1 * b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = Convert.ToString(c1);
                            break;
                        case 4:
                            FirstNumber = textboxFirstNum.Text;
                            SecondNumber = textboxSecondNum.Text;
                            a1 = TripToDec(FirstNumber);
                            b1 = TripToDec(SecondNumber);
                            c = a1 / b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = Convert.ToString(c1);
                            break;
                    }

                    textBoxResult.Visibility = Visibility.Visible;                   
                }

                catch (Exception)
                {
                    textBoxResult.Visibility = Visibility.Hidden;
                    dlgException.ShowDialog();
                }
            }
            else
            {
                textBoxResult.Visibility = Visibility.Hidden;
                ModernDialog dlgException = new ModernDialog()
                {
                    Title = "Ошибка ввода",
                    Content = "Введите первый или второй опоперанд!",
                };
                dlgException.ShowDialog();
            }

        }
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textboxFirstNum.Clear();
            textboxSecondNum.Clear();
            textBoxResult.Clear();
            combobox.SelectedIndex = 0;
        }//нажатие кнопки!
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxValidation();
            textboxFirstNum.MaxLength = 10;
        }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxValidation();
            textboxSecondNum.MaxLength = 10;
        }
        public bool textBoxValidation()
        {
            int operandLeft = 0;
            int operandRight = 0;
            bool verifyBoth = true;
            bool verifyLeft = true; // проверка первого числа 
            bool verifyRight = true; // проверка второго числа 

            for (int i = 0; i < textboxFirstNum.Text.Length; i++)
            {
                if ((textboxFirstNum.Text[i] > '5') || (textboxFirstNum.Text[i] == '.'))
                {
                    operandLeft = i + 1;
                    verifyLeft = false;
                    verifyBoth = true;
                    break;
                }
            }

            for (int i = 0; i < textboxSecondNum.Text.Length; i++)
            {
                if ((textboxSecondNum.Text[i] > '5') || (textboxSecondNum.Text[i] == '.'))
                {
                    operandRight = i + 1;
                    verifyBoth = true;
                    verifyRight = false;
                    break;
                }
            }

            if ((verifyLeft == false) && (verifyRight == true))
            {
                var dlgException = new ModernDialog()
                {
                    Title = "Ошибка ввода",
                    Content = "Ошибка в " + operandLeft + " элементе первого операнда!",
                };
                dlgException.ShowDialog();
                MessageBox.Show("Разрешенные знаки - \"0\" \"1\" \"2\" \"3\" \"4\" \"5\"\",\" !", "Подсказка!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if ((verifyLeft == true) && (verifyRight == false))
            {
                var dlgException = new ModernDialog()
                {
                    Title = "Ошибка ввода",
                    Content = "Ошибка в " + operandRight + " элементе первого операнда!",
                };
                dlgException.ShowDialog();
                return false;
            }

            return verifyBoth;
        }
        static double TripToDec(string FloatNumber)
        {
            string[] floatNumber = FloatNumber.Split(',');
            int IntegerPart = Convert.ToInt32(floatNumber[0]);
            string FractionalPart = "";
            double Res = 0;
            double res = 0;

            double res10 = 0;
            for (int g = 0; IntegerPart != 0; g++)
            {
                res10 += (IntegerPart % 10) * (int)Math.Pow(6, g); // перевод целой части в 10-ю систему
                IntegerPart /= 10;
            }

            try
            {
                FractionalPart = floatNumber[1].ToString();//поменял в коде 3 на 6!
                for (int j = 0; j < 5; j++)
                {
                    Res += (int)(FractionalPart[j] - 48) * Math.Pow(6, -(j + 1)); // перевод вещественой части в 10-ю систему
                }
            }
            catch { };

            if (res10 > 0)
            {
                res = res10 + Res;
            }
            else
            {
                res = res10 - Res;
            }
            return res;
        }

        /*--------------Перевод из 10 в 3 --------------------*/
        static double DecToTrip(double FloatNumber)
        {
            string[] floatNumber = FloatNumber.ToString().Split(',');

            string fractionalPart = "";
            int i = 0;

            try
            {
                double FractionalPart = Convert.ToDouble("0," + floatNumber[1]);
                if (FractionalPart == 0.33333333333333)
                {
                    fractionalPart = "1";
                }
                else

                {
                    FractionalPart *= 6;
                    do
                    {
                        string[] help = FractionalPart.ToString().Split(',');

                        if ((Convert.ToInt32(help[0]) <= 0) || (Convert.ToInt32(help[0]) < 6))
                        {
                            fractionalPart += help[0];
                        }
                        FractionalPart = Convert.ToDouble("0," + help[1]) * 6;
                        i++;
                    }
                    while (i < 5);
                }
            }
            catch { }

            int IntegerPart = Convert.ToInt32(floatNumber[0]);
            bool otr = false;

            if (IntegerPart < 0)
            {
                IntegerPart = IntegerPart * (-1);
                otr = true;
            }

            int IntEl;
            string integerPart = "";
            while ((IntegerPart > 0) || (IntegerPart < 0))
            {
                IntEl = IntegerPart % 6;
                IntegerPart /= 6;
                integerPart += IntEl.ToString();
            }

            char[] res3 = integerPart.ToCharArray();
            Array.Reverse(res3);
            integerPart = new string(res3);

            string otvet = integerPart + ',' + fractionalPart;
            double Otvet = 0;

            if (otvet == ",")
            {
                return 0;
            }

            if (otr == true)
            {
                Otvet = (-1) * Convert.ToDouble(otvet);

            }
            else { Otvet = Convert.ToDouble(otvet); }
            return Otvet;

        }
    }
}
