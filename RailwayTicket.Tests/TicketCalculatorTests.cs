using NUnit.Framework;
using RailwayTicket;

namespace RailwayTicket.Tests
{
    /// <summary>
    /// Автоматизированные тесты для класса TicketCalculator.
    /// Покрывают: базовые расчёты, все типы комфортабельности,
    /// граничные значения и обработку некорректных данных.
    /// </summary>
    [TestFixture]
    public class TicketCalculatorTests
    {
        // -------------------------------------------------------
        // TC_CALC_1 — TC_CALC_4: Проверка коэффициентов комфортабельности
        // -------------------------------------------------------

        /// <summary>
        /// TC_CALC_1: Плацкарт — коэффициент 1.0 (базовая стоимость).
        /// Стоимость = 100 км × 8 руб. × 1 билет × 1.0 = 800 руб.
        /// </summary>
        [Test]
        public void Calculate_Platzkart_ReturnsBasePrice()
        {
            double result = TicketCalculator.Calculate(100, 1, TicketCalculator.CoefficientPlatzkart);
            Assert.That(result, Is.EqualTo(800.0));
        }

        /// <summary>
        /// TC_CALC_2: Купе — коэффициент 1.1 (+10%).
        /// Стоимость = 100 × 8 × 1 × 1.1 = 880 руб.
        /// </summary>
        [Test]
        public void Calculate_Coupe_ReturnsPricePlusTenPercent()
        {
            double result = TicketCalculator.Calculate(100, 1, TicketCalculator.CoefficientCoupe);
            Assert.That(result, Is.EqualTo(880.0).Within(0.001));
        }

        /// <summary>
        /// TC_CALC_3: Полулюкс — коэффициент 1.2 (+20%).
        /// Стоимость = 100 × 8 × 1 × 1.2 = 960 руб.
        /// </summary>
        [Test]
        public void Calculate_SemiLux_ReturnsPricePlusTwentyPercent()
        {
            double result = TicketCalculator.Calculate(100, 1, TicketCalculator.CoefficientSemiLux);
            Assert.That(result, Is.EqualTo(960.0).Within(0.001));
        }

        /// <summary>
        /// TC_CALC_4: Люкс — коэффициент 1.3 (+30%).
        /// Стоимость = 100 × 8 × 1 × 1.3 = 1040 руб.
        /// </summary>
        [Test]
        public void Calculate_Lux_ReturnsPricePlusThirtyPercent()
        {
            double result = TicketCalculator.Calculate(100, 1, TicketCalculator.CoefficientLux);
            Assert.That(result, Is.EqualTo(1040.0).Within(0.001));
        }

        // -------------------------------------------------------
        // TC_CALC_5 — TC_CALC_7: Несколько билетов
        // -------------------------------------------------------

        /// <summary>
        /// TC_CALC_5: Несколько билетов, плацкарт.
        /// Стоимость = 200 × 8 × 3 × 1.0 = 4800 руб.
        /// </summary>
        [Test]
        public void Calculate_MultipleTickets_Platzkart_ReturnsCorrectTotal()
        {
            double result = TicketCalculator.Calculate(200, 3, TicketCalculator.CoefficientPlatzkart);
            Assert.That(result, Is.EqualTo(4800.0));
        }

        /// <summary>
        /// TC_CALC_6: Несколько билетов, купе.
        /// Стоимость = 150 × 8 × 2 × 1.1 = 2640 руб.
        /// </summary>
        [Test]
        public void Calculate_MultipleTickets_Coupe_ReturnsCorrectTotal()
        {
            double result = TicketCalculator.Calculate(150, 2, TicketCalculator.CoefficientCoupe);
            Assert.That(result, Is.EqualTo(2640.0).Within(0.001));
        }

        /// <summary>
        /// TC_CALC_7: Несколько билетов, люкс.
        /// Стоимость = 500 × 8 × 4 × 1.3 = 20800 руб.
        /// </summary>
        [Test]
        public void Calculate_MultipleTickets_Lux_ReturnsCorrectTotal()
        {
            double result = TicketCalculator.Calculate(500, 4, TicketCalculator.CoefficientLux);
            Assert.That(result, Is.EqualTo(20800.0).Within(0.001));
        }

        // -------------------------------------------------------
        // TC_CALC_8 — TC_CALC_9: Граничные значения
        // -------------------------------------------------------

        /// <summary>
        /// TC_CALC_8: Минимально допустимые значения (1 км, 1 билет).
        /// Стоимость = 1 × 8 × 1 × 1.0 = 8 руб.
        /// </summary>
        [Test]
        public void Calculate_MinimumValues_ReturnsMinimalPrice()
        {
            double result = TicketCalculator.Calculate(1, 1, TicketCalculator.CoefficientPlatzkart);
            Assert.That(result, Is.EqualTo(8.0));
        }

        /// <summary>
        /// TC_CALC_9: Большое расстояние (10000 км, 10 билетов, люкс).
        /// Стоимость = 10000 × 8 × 10 × 1.3 = 1 040 000 руб.
        /// </summary>
        [Test]
        public void Calculate_LargeValues_ReturnsCorrectTotal()
        {
            double result = TicketCalculator.Calculate(10000, 10, TicketCalculator.CoefficientLux);
            Assert.That(result, Is.EqualTo(1_040_000.0).Within(0.001));
        }

        // -------------------------------------------------------
        // TC_CALC_10 — TC_CALC_12: Некорректные входные данные
        // -------------------------------------------------------

        /// <summary>
        /// TC_CALC_10: Нулевое расстояние — должно выбрасывать ArgumentException.
        /// </summary>
        [Test]
        public void Calculate_ZeroDistance_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() =>
                TicketCalculator.Calculate(0, 1, TicketCalculator.CoefficientPlatzkart));
        }

        /// <summary>
        /// TC_CALC_11: Отрицательное расстояние — должно выбрасывать ArgumentException.
        /// </summary>
        [Test]
        public void Calculate_NegativeDistance_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() =>
                TicketCalculator.Calculate(-50, 1, TicketCalculator.CoefficientPlatzkart));
        }

        /// <summary>
        /// TC_CALC_12: Нулевое количество билетов — должно выбрасывать ArgumentException.
        /// </summary>
        [Test]
        public void Calculate_ZeroTicketCount_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() =>
                TicketCalculator.Calculate(100, 0, TicketCalculator.CoefficientPlatzkart));
        }

        /// <summary>
        /// TC_CALC_13: Отрицательное количество билетов — должно выбрасывать ArgumentException.
        /// </summary>
        [Test]
        public void Calculate_NegativeTicketCount_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() =>
                TicketCalculator.Calculate(100, -2, TicketCalculator.CoefficientPlatzkart));
        }

        // -------------------------------------------------------
        // TC_CALC_14: Проверка константы ставки
        // -------------------------------------------------------

        /// <summary>
        /// TC_CALC_14: Константа RatePerKm должна быть равна 8.0 (по условию задачи).
        /// </summary>
        [Test]
        public void RatePerKm_Constant_IsEightRubles()
        {
            Assert.That(TicketCalculator.RatePerKm, Is.EqualTo(8.0));
        }

        // -------------------------------------------------------
        // TC_CALC_15: Параметризованный тест (NUnit TestCase)
        // -------------------------------------------------------

        /// <summary>
        /// TC_CALC_15: Параметризованный тест — проверяет несколько наборов данных.
        /// </summary>
        [TestCase(100, 1, 1.0, 800.0)]
        [TestCase(100, 1, 1.1, 880.0)]
        [TestCase(100, 1, 1.2, 960.0)]
        [TestCase(100, 1, 1.3, 1040.0)]
        [TestCase(50,  2, 1.0, 800.0)]
        [TestCase(250, 3, 1.1, 6600.0)]
        public void Calculate_ParameterizedCases_ReturnsExpected(
            int distance, int tickets, double coefficient, double expected)
        {
            double result = TicketCalculator.Calculate(distance, tickets, coefficient);
            Assert.That(result, Is.EqualTo(expected).Within(0.001));
        }
    }
}
