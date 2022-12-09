using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.models.Interfaces
{
    internal interface IPerson
    {
        public string MaritalStatus { get; set; }
        public string SpouseFileName { get; set; }

        public PersonSpouse? spouse { get; set; }

        public validation_result ValidateMaritalStatus();
        public string BuildOutputString();
    }
}
