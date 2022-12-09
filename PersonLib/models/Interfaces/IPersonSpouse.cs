using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.models.Interfaces
{
    internal interface IPersonSpouse : IBasePerson
    {
        public string GenerateFilename();
        public string BuildOutputString();
    }
}
