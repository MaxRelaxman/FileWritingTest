using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.models.Interfaces
{
    internal interface IBasePerson
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }

        public bool ReqPermissionOverride();
        public bool IsMinimumAge();

        public validation_result ValidateFirstName();
        public validation_result ValidateSurName();
        public validation_result ValidateDateOfBirth();        
    }
}
