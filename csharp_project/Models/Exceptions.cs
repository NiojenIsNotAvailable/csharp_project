using System;

namespace csharp_project.Models
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException() : base("Дата народження не може бути в майбутньому.") { }
    }

    public class TooOldBirthDateException : Exception
    {
        public TooOldBirthDateException() : base("Дата народження надто далека в минуле.") { }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Невірна адреса електронної пошти.") { }
    }
}
