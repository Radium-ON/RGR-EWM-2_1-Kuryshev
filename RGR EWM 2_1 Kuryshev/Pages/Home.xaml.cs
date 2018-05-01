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
        private Regex regex = new Regex(@"^(?!0)-?[0-5]*\,?[0-5]*$");//(первый разряд не 0!)минус 0 или 1 в начале строки; числа 0-5, одна запятая, ещё числа 0-5
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
            if (textBoxFirstNum.Text != String.Empty && textBoxSecondNum.Text != String.Empty)
            {
                try
                {
                    string FirstNumber;
                    string SecondNumber;
                    switch (combobox.SelectedIndex)
                    {
                        case 1:
                            FirstNumber = textBoxFirstNum.Text;
                            SecondNumber = textBoxSecondNum.Text;
                            a1 = SextupleToDecimal(FirstNumber);
                            b1 = SextupleToDecimal(SecondNumber);
                            c = a1 + b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = c1;
                            break;
                        case 2:
                            FirstNumber = textBoxFirstNum.Text;
                            SecondNumber = textBoxSecondNum.Text;
                            a1 = SextupleToDecimal(FirstNumber);
                            b1 = SextupleToDecimal(SecondNumber);
                            c = a1 - b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = Convert.ToString(c1);
                            break;
                        case 3:
                            FirstNumber = textBoxFirstNum.Text;
                            SecondNumber = textBoxSecondNum.Text;
                            a1 = SextupleToDecimal(FirstNumber);
                            b1 = SextupleToDecimal(SecondNumber);
                            c = a1 * b1;
                            z = c;
                            c1 = Convert.ToString(DecToTrip(z));
                            textBoxResult.Text = Convert.ToString(c1);
                            break;
                        case 4:
                            FirstNumber = textBoxFirstNum.Text;
                            SecondNumber = textBoxSecondNum.Text;
                            a1 = SextupleToDecimal(FirstNumber);
                            b1 = SextupleToDecimal(SecondNumber);
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
                    var dlgException = new ModernDialog()
                    {
                        Title = "Ошибка ввода",
                        Content = "Введите корректные данные",
                    };
                    dlgException.ShowDialog();
                }
            }
            else
            {
                textBoxResult.Visibility = Visibility.Hidden;
                var dlgException = new ModernDialog()
                {
                    Title = "Ошибка ввода",
                    Content = "Введите первый или второй опоперанд!",
                };
                dlgException.ShowDialog();
            }

        }
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxFirstNum.Clear();
            textBoxSecondNum.Clear();
            textBoxResult.Clear();
            combobox.SelectedIndex = 0;
        }//нажатие кнопки!
        private void textBoxFirstNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            int operandLeft = 0;
            Match match = regex.Match(textBoxFirstNum.Text);
            if (!match.Success && textBoxFirstNum.Text != "")//проверка ввода регулярным выражением
            {
                var dlgException = new ModernDialog()
                {
                    Title = "Ошибка ввода",
                    Content = "Ошибка в " + (operandLeft=textBoxFirstNum.Text.Length) + " разряде первого операнда!",
                };
                dlgException.ShowDialog();

                textBoxFirstNum.Text = textBoxFirstNum.Text.Remove(textBoxFirstNum.Text.Length - 1);
                textBoxFirstNum.Select(textBoxFirstNum.Text.Length, 0);
            }
            textBoxFirstNum.MaxLength = 10; 
        }
        private void textBoxSecondNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            int operandRight = 0;
            Match match = regex.Match(textBoxSecondNum.Text);
            if (!match.Success && textBoxSecondNum.Text != "")//проверка ввода регулярным выражением
            {
                var dlgException = new ModernDialog()
                {
                    Title = "Ошибка ввода",
                    Content = "Ошибка в " + (operandRight=textBoxSecondNum.Text.Length)+ " разряде первого операнда!",
                };
                dlgException.ShowDialog();

                textBoxSecondNum.Text = textBoxSecondNum.Text.Remove(textBoxSecondNum.Text.Length - 1);
                textBoxSecondNum.Select(textBoxSecondNum.Text.Length, 0);
            }
            textBoxSecondNum.MaxLength = 10;
        }
        static double SextupleToDecimal(string FloatNumber)
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
                FractionalPart = floatNumber[1].ToString();
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

        /*--------------Перевод из 10 в 6 --------------------*/
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

        private void textBoxFirstNum_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxFirstNum.Focus();
        }
    }
}
