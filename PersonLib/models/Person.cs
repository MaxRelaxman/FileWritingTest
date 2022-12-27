using PersonLib.models.Interfaces;

namespace PersonLib.models
{
    public class Person : BasePerson, IPerson
    {
        public string MaritalStatus { get; set; } = string.Empty;
        public string SpouseFileName { get; set; } = string.Empty;

        public PersonSpouse? spouse { get; set; } = null;



        /// <summary>
        /// Case insensitive check of marital status.
        /// </summary>
        /// <returns>True if married or single</returns>
        public validation_result ValidateMaritalStatus()
        {
            if(MaritalStatus == string.Empty)
            {
                return validation_result.EmptyValue;
            }

            if (MaritalStatus.ToLower() == "married" || MaritalStatus.ToLower() == "single")
            {
                return validation_result.IsValid;
            }
            else
            {
                return validation_result.InvalidCharacters;
            }
        }

        public string BuildOutputString()
        {
            string Result = $"{Firstname}|{Surname}|{DateOfBirth}|{MaritalStatus}";

            if (MaritalStatus.ToLower() == "married")
                Result += $"|{spouse?.GenerateFilename()}";
            else
                Result += "|null";

            return Result;
        }
    }
}
