using PersonLib.models;

namespace FileWritingTest
{
    internal class GetPerson
    {
        public void Start()
        {
            List<Person> peopleList = GetPeopleFromUser();

            if (peopleList.Count() > 0)
                PersonLib.FileHandling.Writer.WritePeople(peopleList);
        }

        private List<Person> GetPeopleFromUser()
        {
            List<Person> peopleList = new List<Person>();

            Console.WriteLine("Person Entry System.");

            while (true)
            {
                Person newPerson = new Person();

                bool personIsValid = GetBasicPersonInfo(newPerson);

                if (personIsValid)
                {
                    GetSpouse(newPerson);
                    peopleList.Add(newPerson);
                }

                if (AddAnotherPersonCheck())
                    continue;
                else
                    break;
                
            }

            return peopleList;
        }

        private bool GetBasicPersonInfo(BasePerson newPerson) 
        {
            GetFirstName(newPerson);
            GetSurname(newPerson);
            GetDOB(newPerson);

            if (!newPerson.IsMinimumAge())
            {
                Console.WriteLine("\r\nPerson is below minimum age.");
                //check if user wants to continue with a new person
                return false;
            }

            if (newPerson.ReqPermissionOverride())
            {
                if (!GetParentalOverride(newPerson))
                {
                    Console.WriteLine("Parental permission not given.");
                    //check if user wants to continue with a new person
                    return false;
                }
            }

            return true;
        }

        private bool AddAnotherPersonCheck()
        {
            Console.WriteLine("Would you like to add another person? Y/N ");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                return true;
            else
                return false;
        }

        private void GetFirstName(BasePerson newPerson)
        {
            Console.Write("\r\nEnter Person's First Name: ");
            newPerson.Firstname = Console.ReadLine() ?? string.Empty;

            PersonLib.validation_result validationResult = newPerson.ValidateFirstName();

            switch(validationResult)
            {
                case PersonLib.validation_result.EmptyValue:
                    Console.WriteLine("A value is required");
                    GetFirstName(newPerson);
                break;
                case PersonLib.validation_result.InvalidCharacters:
                    Console.WriteLine("Invalid characters in name. Please use only letters or a single quote (')");
                    GetFirstName(newPerson);
                break;
                case PersonLib.validation_result.OutOfRange:
                    Console.WriteLine($"Name is too long, please only enter {PersonLib.Settings.MaxFirstNameLen} characters");
                    GetFirstName(newPerson);
                    break;
                case PersonLib.validation_result.IsValid:
                    return;
            }
        }

        private void GetSurname(BasePerson newPerson)
        {
            Console.Write("Enter Person's surname: ");
            newPerson.Surname = Console.ReadLine() ?? string.Empty;

            PersonLib.validation_result validationResult = newPerson.ValidateSurName();

            switch (validationResult)
            {
                case PersonLib.validation_result.EmptyValue:
                    Console.WriteLine("A value is required");
                    GetSurname(newPerson);
                    break;
                case PersonLib.validation_result.InvalidCharacters:
                    Console.WriteLine("Invalid characters in name. Please use only letters or a single quote (')");
                    GetSurname(newPerson);
                    break;
                case PersonLib.validation_result.OutOfRange:
                    Console.WriteLine($"Name is too long, please only enter {PersonLib.Settings.MaxSurNameLen} characters");
                    GetSurname(newPerson);
                    break;
                case PersonLib.validation_result.IsValid:
                    return;
            }
        }

        private void GetDOB(BasePerson newPerson)
        {
            Console.Write("Enter Person's Date of Birth (mm/dd/yyyy): ");
            newPerson.DateOfBirth = Console.ReadLine() ?? string.Empty;

            PersonLib.validation_result validationResult = newPerson.ValidateDateOfBirth();

            switch (validationResult)
            {
                case PersonLib.validation_result.EmptyValue:
                    Console.WriteLine("Date of birth is required");
                    GetDOB(newPerson);
                    break;
                case PersonLib.validation_result.OutOfRange:
                case PersonLib.validation_result.InvalidCharacters:
                    Console.WriteLine($"Invalid date entered.");
                    GetDOB(newPerson);
                    break;
                case PersonLib.validation_result.IsValid:
                    return;
            }
        }

        private bool GetParentalOverride(BasePerson newPerson)
        {
            Console.WriteLine("Does this person have parental permission? (Y/N) ");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                return true;
            else
                return false;
        }   
        
        private void GetSpouse(Person newPerson)
        {
            Console.WriteLine("Is this person married? (Y/N) ");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.KeyChar == 'y' || key.KeyChar == 'Y')
            {
                newPerson.MaritalStatus = "Married";
                newPerson.spouse = new PersonSpouse();
                GetBasicPersonInfo(newPerson.spouse);
            }
            else
            {
                newPerson.MaritalStatus = "Single";
            }
        }
    }
}
