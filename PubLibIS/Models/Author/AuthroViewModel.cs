using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PubLibIS.Models.Author
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }

        public string FullName
        {
            get
            {
                return $"{SecondName}" +
                    (string.IsNullOrEmpty(FirstName) ? " " : $" {FirstName.TrimStart()[0]}.") +
                    (string.IsNullOrEmpty(Patronymic) ? " " : $" {Patronymic.TrimStart()[0]}.");
            }
        }
    }
}