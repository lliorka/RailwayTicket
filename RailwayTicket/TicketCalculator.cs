namespace RailwayTicket
{
    /// <summary>
    /// Класс с бизнес-логикой расчёта стоимости проезда.
    /// Вынесен в отдельный класс для возможности модульного тестирования
    /// без зависимости от UI (WPF).
    /// </summary>
    public static class TicketCalculator
    {
        /// <summary>
        /// Ставка проезда за 1 км в рублях (по условию задачи).
        /// </summary>
        public const double RatePerKm = 8.0;

        /// <summary>
        /// Коэффициент комфортабельности для плацкарта (базовый, 100%).
        /// </summary>
        public const double CoefficientPlatzkart = 1.0;

        /// <summary>
        /// Коэффициент комфортабельности для купе (+10%).
        /// </summary>
        public const double CoefficientCoupe = 1.1;

        /// <summary>
        /// Коэффициент комфортабельности для полулюкса (+20%).
        /// </summary>
        public const double CoefficientSemiLux = 1.2;

        /// <summary>
        /// Коэффициент комфортабельности для люкса (+30%).
        /// </summary>
        public const double CoefficientLux = 1.3;

        /// <summary>
        /// Вычисляет итоговую стоимость билетов.
        /// Формула: Стоимость = Расстояние × Ставка × Количество_билетов × Коэффициент
        /// </summary>
        /// <param name="distanceKm">Расстояние в километрах (должно быть > 0)</param>
        /// <param name="ticketCount">Количество билетов (должно быть > 0)</param>
        /// <param name="comfortCoefficient">Коэффициент комфортабельности (1.0–1.3)</param>
        /// <returns>Итоговая стоимость в рублях</returns>
        /// <exception cref="System.ArgumentException">
        /// Выбрасывается если distanceKm или ticketCount не положительные
        /// </exception>
        public static double Calculate(int distanceKm, int ticketCount, double comfortCoefficient)
        {
            if (distanceKm <= 0)
                throw new System.ArgumentException("Расстояние должно быть положительным числом.", nameof(distanceKm));

            if (ticketCount <= 0)
                throw new System.ArgumentException("Количество билетов должно быть положительным числом.", nameof(ticketCount));

            // Базовая стоимость одного билета: расстояние × ставка
            double baseCostPerTicket = distanceKm * RatePerKm;

            // Итоговая стоимость с учётом количества и комфортабельности
            return baseCostPerTicket * ticketCount * comfortCoefficient;
        }
    }
}
