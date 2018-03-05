using PubLibIS.ViewModels.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PubLibIS.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        [CustomDataDisplayFormat]
        public DateTime DateOfBirth { get; set; }

        [CustomDataDisplayFormat]
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
        
        public string LifeTime
        {
            get
            {   //(48) xx.xx.1902 - yy.yy.1950
                return $"{(DateOfDeath.HasValue? ($"({DateOfDeath.Value.Year - DateOfBirth.Year})"): ($"({DateTime.Now.Year - DateOfBirth.Year})"))} {DateOfBirth:dd.MM.yyyy} - {(DateOfDeath != null ? $"{DateOfDeath:dd.MM.yyyy}" : "now")}";
            }
        }
    }
}
