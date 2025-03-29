using System;
using System.Threading.Tasks;
using csharp_project.Models;

namespace csharp_project.Models
{
    public class Person
    {
        // Властивості
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime? BirthDate { get; }

        // Поля для кешування значень
        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;

        // Конструктор з усіма параметрами
        public Person(string firstName, string lastName, string email, DateTime? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;

            // Попередньо обчислені значення
            if (birthDate.HasValue)
            {
                _isAdult = CalculateIsAdult(birthDate.Value);
                _sunSign = Zodiac.GetZodiac(birthDate);
                _chineseSign = Zodiac.GetChineseZodiac(birthDate);
                _isBirthday = CheckIsBirthday(birthDate.Value);
            }
        }

        // Конструктор без дати народження
        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, null) { }

        // Конструктор без email
        public Person(string firstName, string lastName, DateTime? birthDate)
            : this(firstName, lastName, string.Empty, birthDate) { }

        // Властивості лише для читання
        public bool IsAdult => _isAdult;
        public string SunSign => _sunSign;
        public string ChineseSign => _chineseSign;
        public bool IsBirthday => _isBirthday;

        // Методи обчислення
        private static bool CalculateIsAdult(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }
            return age >= 18;
        }

        private static bool CheckIsBirthday(DateTime birthDate)
        {
            return birthDate.Day == DateTime.Today.Day &&
                   birthDate.Month == DateTime.Today.Month;
        }
    }
}
