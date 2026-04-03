using System.Windows;

namespace RailwayTicket
{
    /// <summary>
    /// Главное окно приложения «Стоимость проезда железнодорожным транспортом».
    /// Вариант №15: расчёт стоимости в зависимости от расстояния, количества билетов
    /// и выбранного типа комфортабельности.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки «Вычислить».
        /// Считывает данные, вызывает расчёт и отображает результат.
        /// </summary>
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Проверка и разбор поля «Расстояние»
            if (!int.TryParse(TxtDistance.Text.Trim(), out int distance) || distance <= 0)
            {
                MessageBox.Show("Введите корректное расстояние (целое положительное число).",
                                "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtDistance.Focus();
                return;
            }

            // Проверка и разбор поля «Количество билетов»
            if (!int.TryParse(TxtTicketCount.Text.Trim(), out int ticketCount) || ticketCount <= 0)
            {
                MessageBox.Show("Введите корректное количество билетов (целое положительное число).",
                                "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtTicketCount.Focus();
                return;
            }

            // Определение коэффициента комфортабельности
            double comfortCoefficient = GetComfortCoefficient();

            // Расчёт итоговой стоимости
            double totalCost = TicketCalculator.Calculate(distance, ticketCount, comfortCoefficient);

            // Отображение результата
            TxtResult.Text = $"{totalCost:F2} руб.";
        }

        /// <summary>
        /// Возвращает коэффициент комфортабельности на основе выбранного RadioButton.
        /// Плацкарт — 1.0, Купе — 1.1, Полулюкс — 1.2, Люкс — 1.3.
        /// </summary>
        /// <returns>Коэффициент типа double</returns>
        private double GetComfortCoefficient()
        {
            if (RbCoupe.IsChecked == true)    return 1.1; // купе: +10%
            if (RbSemiLux.IsChecked == true)  return 1.2; // полулюкс: +20%
            if (RbLux.IsChecked == true)      return 1.3; // люкс: +30%
            return 1.0;                                    // плацкарт: базовая (100%)
        }
    }
}
