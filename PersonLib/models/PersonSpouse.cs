using PersonLib.models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.models
{
    public class PersonSpouse : BasePerson, IPersonSpouse
    {
        public string BuildOutputString()
        {
            return $"{Firstname}|{Surname}|{DateOfBirth}";
        }

        public string GenerateFilename()
        {
            Guid spouseGuid = Guid.NewGuid(); //just to generate a unique value
            string FileName = $"{spouseGuid}-{Surname}-{Firstname}.txt";

            return Path.Combine(Settings.SpouseFilePath, FileName);
        }
    }
}
