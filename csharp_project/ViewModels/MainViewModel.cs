using System;
using System.ComponentModel;
using System.Windows.Input;
using csharp_project.Models;

namespace csharp_project.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime? _selectedDate;
        private string _zodiacSign = string.Empty;
        private string _chineseZodiac = string.Empty;
        private string _ageText = string.Empty;
        private string _birthdayMessage = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (value.HasValue && (value > DateTime.Today || value < DateTime.Today.AddYears(-135)))
                {
                    _selectedDate = null;
                    AgeText = "Некоректна дата";
                    ZodiacSign = string.Empty;
                    ChineseZodiac = string.Empty;
                    BirthdayMessage = string.Empty;
                }
                else
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    UpdateZodiacSigns();
                }
            }
        }

        public string ZodiacSign
        {
            get => _zodiacSign;
            private set
            {
                _zodiacSign = value;
                OnPropertyChanged(nameof(ZodiacSign));
            }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            private set
            {
                _chineseZodiac = value;
                OnPropertyChanged(nameof(ChineseZodiac));
            }
        }

        public string AgeText
        {
            get => _ageText;
            private set
            {
                _ageText = value;
                OnPropertyChanged(nameof(AgeText));
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

        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
            ClearCommand = new CommandHandler(ClearFields);
        }

        private void UpdateZodiacSigns()
        {
            if (SelectedDate.HasValue)
            {
                ZodiacSign = Zodiac.GetZodiac(SelectedDate);
                ChineseZodiac = Zodiac.GetChineseZodiac(SelectedDate);

                int age = DateTime.Today.Year - SelectedDate.Value.Year;
                if (SelectedDate.Value.Date > DateTime.Today.AddYears(-age))
                {
                    age--;
                }

                AgeText = $"Вік: {age} років";

                BirthdayMessage = (SelectedDate.Value.Day == DateTime.Today.Day &&
                                   SelectedDate.Value.Month == DateTime.Today.Month)
                    ? "З Днем народження! 🎉"
                    : string.Empty;
            }
            else
            {
                AgeText = string.Empty;
                BirthdayMessage = string.Empty;
            }
        }

        private void ClearFields()
        {
            SelectedDate = null;
            AgeText = string.Empty;
            ZodiacSign = string.Empty;
            ChineseZodiac = string.Empty;
            BirthdayMessage = string.Empty;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CommandHandler : ICommand
    {
        private readonly Action _execute;

        public CommandHandler(Action execute) => _execute = execute;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _execute();
    }
}
