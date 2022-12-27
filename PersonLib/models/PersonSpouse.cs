using PersonLib.models.Interfaces;

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
