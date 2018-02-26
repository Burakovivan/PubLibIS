using System;
using System.Collections.Generic;

namespace PubLibIS_BLL.Model
{
    public class Author
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
        
    }
}
