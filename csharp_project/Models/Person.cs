using System;

namespace csharp_project.Models
{
    public class Person
    {
        // Властивості
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        // Попередньо обчислені значення
        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;

        // Конструктор з усіма параметрами
        public Person(string firstName, string lastName, string email, DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                throw new ArgumentNullException(nameof(birthDate));

            if (birthDate > DateTime.Today)
                throw new FutureBirthDateException();

            if (birthDate < DateTime.Today.AddYears(-135))
                throw new TooOldBirthDateException();

            if (!IsValidEmail(email))
                throw new InvalidEmailException();

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate.Value;

            _isAdult = CalculateIsAdult(BirthDate);
            _sunSign = Zodiac.GetZodiac(BirthDate);
            _chineseSign = Zodiac.GetChineseZodiac(BirthDate);
            _isBirthday = CheckIsBirthday(BirthDate);
        }

        // Конструктор без дати народження
        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, null) { }

        // Конструктор без email
        public Person(string firstName, string lastName, DateTime? birthDate)
            : this(firstName, lastName, string.Empty, birthDate) { }

        // Перевірка email
        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   email.Contains('@') &&
                   email.IndexOf('@') > 0 &&
                   email.IndexOf('@') < email.Length - 1;
        }

        // Властивості лише для читання
        public bool IsAdult => _isAdult;
        public string SunSign => _sunSign;
        public string ChineseSign => _chineseSign;
        public bool IsBirthday => _isBirthday;

        // Вік
        public int Age => DateTime.Today.Year - BirthDate.Year -
                          (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

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
