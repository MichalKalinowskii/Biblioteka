using Library.Domain.Authors.Errors;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Authors.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath {  get; set; }

        public Author(string name, string lastName, DateTime dateOfBirth, DateTime dateOfDeath, string description)
        {
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
            Description = description;
        }

        public Result ChangeDateOfDeath(DateTime dateOfDeath)
        {
            DateOfDeath = dateOfDeath;
            return Result.Success();
        }

        public Result ChangeLastName(string lastName) 
        {
            if (string.IsNullOrWhiteSpace(LastName)) 
            {
                return Result.Failure(AuthorErrors.AuthorLastNameMissing);
            }

            LastName = lastName;
            return Result.Success();
        }
    }
}
