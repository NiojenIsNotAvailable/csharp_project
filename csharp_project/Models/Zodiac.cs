using System;

namespace csharp_project.Models
{
    public static class Zodiac
    {
        public static string GetZodiac(DateTime? birthDate)
        {
            if (!birthDate.HasValue) return string.Empty;

            return (birthDate.Value.Month, birthDate.Value.Day) switch
            {
                (1, >= 20) or (2, <= 18) => "Водолій",
                (2, >= 19) or (3, <= 20) => "Риби",
                (3, >= 21) or (4, <= 19) => "Овен",
                (4, >= 20) or (5, <= 20) => "Телець",
                (5, >= 21) or (6, <= 20) => "Близнюки",
                (6, >= 21) or (7, <= 22) => "Рак",
                (7, >= 23) or (8, <= 22) => "Лев",
                (8, >= 23) or (9, <= 22) => "Діва",
                (9, >= 23) or (10, <= 22) => "Терези",
                (10, >= 23) or (11, <= 21) => "Скорпіон",
                (11, >= 22) or (12, <= 21) => "Стрілець",
                (12, >= 22) or (1, <= 19) => "Козеріг",
                _ => string.Empty
            };
        }

        public static string GetChineseZodiac(DateTime? birthDate)
        {
            if (!birthDate.HasValue) return string.Empty;

            string[] chineseZodiacs = {
                "Мавпа", "Півень", "Собака", "Свиня", "Щур",
                "Бик", "Тигр", "Кіт (Кролик)", "Дракон", "Змія",
                "Кінь", "Коза"
            };

            return chineseZodiacs[birthDate.Value.Year % 12];
        }
    }
}
