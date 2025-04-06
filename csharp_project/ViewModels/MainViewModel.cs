using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using csharp_project.Models;

namespace csharp_project.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private DateTime? _birthDate;
        private string _sunSign = string.Empty;
        private string _chineseSign = string.Empty;
        private string _ageStatus = string.Empty;
        private string _birthdayMessage = string.Empty;
        private bool _isProcessing;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                ValidateProceedButton();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                ValidateProceedButton();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ValidateProceedButton();
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
                ValidateProceedButton();
            }
        }

        public string SunSign
        {
            get => _sunSign;
            private set
            {
                _sunSign = value;
                OnPropertyChanged(nameof(SunSign));
            }
        }

        public string ChineseSign
        {
            get => _chineseSign;
            private set
            {
                _chineseSign = value;
                OnPropertyChanged(nameof(ChineseSign));
            }
        }

        public string AgeStatus
        {
            get => _ageStatus;
            private set
            {
                _ageStatus = value;
                OnPropertyChanged(nameof(AgeStatus));
            }
        }

        public string BirthdayMessage
        {
            get => _birthdayMessage;
            private set
            {
                _birthdayMessage = value;
                OnPropertyChanged(nameof(BirthdayMessage));
            }
        }

        private int CalculateAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return 0;

            int age = DateTime.Today.Year - birthDate.Value.Year;
            if (birthDate.Value.Date > DateTime.Today.AddYears(-age))
            {
                age--; // Якщо ще не було дня народження в цьому році
            }
            return age;
        }


        private async Task ProceedAsync()
        {
            IsProcessing = true;

            try
            {
                var person = new Person(FirstName, LastName, Email, BirthDate);

                SunSign = $"Знак Зодіаку: {person.SunSign}";
                ChineseSign = $"Китайський знак: {person.ChineseSign}";
                AgeStatus = $"Статус: {(person.IsAdult ? "Дорослий" : "Неповнолітній")} ({CalculateAge(person.BirthDate)})";
                BirthdayMessage = person.IsBirthday ? "🎉 З Днем Народження! 🎉" : "";
            }
            catch (FutureBirthDateException ex)
            {
                AgeStatus = ex.Message;
                SunSign = ChineseSign = BirthdayMessage = string.Empty;
            }
            catch (TooOldBirthDateException ex)
            {
                AgeStatus = ex.Message;
                SunSign = ChineseSign = BirthdayMessage = string.Empty;
            }
            catch (InvalidEmailException ex)
            {
                AgeStatus = ex.Message;
                SunSign = ChineseSign = BirthdayMessage = string.Empty;
            }
            catch (Exception ex)
            {
                AgeStatus = $"Невідома помилка: {ex.Message}";
                SunSign = ChineseSign = BirthdayMessage = string.Empty;
            }
            finally
            {
                IsProcessing = false;
            }
        }



        public bool IsProcessing
        {
            get => _isProcessing;
            private set
            {
                _isProcessing = value;
                OnPropertyChanged(nameof(IsProcessing));
            }
        }

        public ICommand ProceedCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
            ProceedCommand = new CommandHandler(async () => await ProceedAsync(), () => CanProceed);

            ClearCommand = new CommandHandler(ClearFields);
        }


        private void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            BirthDate = null;
            AgeStatus = string.Empty;
            SunSign = string.Empty;
            ChineseSign = string.Empty;
            BirthdayMessage = string.Empty;
        }

        private bool CanProceed => !string.IsNullOrWhiteSpace(FirstName) &&
                                   !string.IsNullOrWhiteSpace(LastName) &&
                                   !string.IsNullOrWhiteSpace(Email) &&
                                   BirthDate.HasValue;

        private void ValidateProceedButton()
        {
            (ProceedCommand as CommandHandler)?.RaiseCanExecuteChanged();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CommandHandler : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public CommandHandler(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
