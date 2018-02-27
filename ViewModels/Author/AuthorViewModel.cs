using System;

namespace ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public string FullName
        {
            get
            {
                return $"{SecondName}" +
                    (string.IsNullOrEmpty(FirstName) ? " " : $" {FirstName.TrimStart()[0]}.") +
                    (string.IsNullOrEmpty(Patronymic) ? " " : $" {Patronymic.TrimStart()[0]}.");
            }
        }
        public string DateofBirthFormated
        {
            get
            {
                return $"{DateOfBirth:dd.MM.yyyy}";
            }
        }
        public string DateOfDeathFormated
        {
            get
            {
                return DateOfDeath != null ?
                 $"{DateOfDeath:dd.MM.yyyy}" :
                 "still alive :)";
            }
        }
        public string LifeTime
        {
            get
            {
                return $"{DateOfBirth:dd.MM.yyyy} - {(DateOfDeath != null ? $"{DateOfDeath:dd.MM.yyyy}" : "now")}";
            }
        }
    }
}
