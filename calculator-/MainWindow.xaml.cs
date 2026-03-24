using System;
using System.Windows;
using System.Windows.Input;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод для получения чисел из текстовых полей
        private bool TryGetNumbers(out double first, out double second)
        {
            first = 0;
            second = 0;

            if (!double.TryParse(FirstNumberTextBox.Text, out first))
            {
                MessageBox.Show("Введите корректное первое число!", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!double.TryParse(SecondNumberTextBox.Text, out second))
            {
                MessageBox.Show("Введите корректное второе число!", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        // Метод для получения одного числа (для корня)
        private bool TryGetSingleNumber(out double number)
        {
            number = 0;
            if (!double.TryParse(FirstNumberTextBox.Text, out number))
            {
                MessageBox.Show("Введите корректное число!", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        // Метод добавления в историю для бинарных операций
        private void AddToHistory(string operation, double first, double second, double result)
        {
            string record = $"{first} {operation} {second} = {result}";
            HistoryListBox.Items.Add(record);
            if (HistoryListBox.Items.Count > 0)
                HistoryListBox.ScrollIntoView(HistoryListBox.Items[HistoryListBox.Items.Count - 1]);
        }

        // Метод добавления в историю для операций с одним числом
        private void AddToHistory(string operation, double number, double result)
        {
            string record = $"{operation}({number}) = {result}";
            HistoryListBox.Items.Add(record);
            if (HistoryListBox.Items.Count > 0)
                HistoryListBox.ScrollIntoView(HistoryListBox.Items[HistoryListBox.Items.Count - 1]);
        }

        // Сложение
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetNumbers(out double first, out double second))
            {
                double result = first + second;
                ResultTextBox.Text = result.ToString();
                AddToHistory("+", first, second, result);
            }
        }

        // Вычитание
        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetNumbers(out double first, out double second))
            {
                double result = first - second;
                ResultTextBox.Text = result.ToString();
                AddToHistory("-", first, second, result);
            }
        }

        // Умножение
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetNumbers(out double first, out double second))
            {
                double result = first * second;
                ResultTextBox.Text = result.ToString();
                AddToHistory("×", first, second, result);
            }
        }

        // Деление
        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetNumbers(out double first, out double second))
            {
                if (second == 0)
                {
                    MessageBox.Show("Деление на ноль невозможно!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                double result = first / second;
                ResultTextBox.Text = result.ToString();
               


AddToHistory("÷", first, second, result);
            }
        }

        // Возведение в степень (x^y) - задание уровня 1
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetNumbers(out double first, out double second))
            {
                double result = Math.Pow(first, second);
                ResultTextBox.Text = result.ToString();
                AddToHistory("^", first, second, result);
            }
        }

        // Квадратный корень - задание уровня 2
        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSingleNumber(out double number))
            {
                if (number < 0)
                {
                    MessageBox.Show("Нельзя извлечь квадратный корень из отрицательного числа!",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                double result = Math.Sqrt(number);
                ResultTextBox.Text = result.ToString();
                AddToHistory("√", number, result);
            }
        }

        // Очистка
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNumberTextBox.Text = string.Empty;
            SecondNumberTextBox.Text = string.Empty;
            ResultTextBox.Text = string.Empty;
            FirstNumberTextBox.Focus();
        }

        // Обработка клавиатуры - задание уровня 2
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Add:
                case Key.OemPlus:
                    AddButton_Click(sender, null);
                    e.Handled = true;
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    SubtractButton_Click(sender, null);
                    e.Handled = true;
                    break;
                case Key.Multiply:
                    MultiplyButton_Click(sender, null);
                    e.Handled = true;
                    break;
                case Key.Divide:
                    DivideButton_Click(sender, null);
                    e.Handled = true;
                    break;
                case Key.P:
                    PowerButton_Click(sender, null);
                    e.Handled = true;
                    break;
                case Key.R:
                    SqrtButton_Click(sender, null);
                    e.Handled = true;
                    break;
                case Key.C:
                    ClearButton_Click(sender, null);
                    e.Handled = true;
                    break;
            }
        }
    }
}