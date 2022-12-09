

using PersonLib.models;

namespace PersonLib.Test
{
    public class BasePersonTests
    {
        [SetUp]
        public void Setup()
        {
            //set configs to known values for testing
            PersonLib.Settings.MaxSurNameLen = 15;
            PersonLib.Settings.MaxFirstNameLen= 15;
            PersonLib.Settings.MaxAgeOverrideReq = 18;
            PersonLib.Settings.MinAge = 10;
        }

        [Test]
        public void FirstName_TestEmptyValue()
        {
            BasePerson person = new BasePerson();
            //defaults to empty, assigning for clarity
            person.Firstname = string.Empty;

            validation_result result = person.ValidateFirstName();

            Assert.That(result == validation_result.EmptyValue, Is.True, "An empty first name incorrectly passed validation.");            
        }

        [Test]
        public void FirstName_TestLengthValidation()
        {
            BasePerson person = new BasePerson();

            //test using a repeating character 
            person.Firstname = new string('a', PersonLib.Settings.MaxFirstNameLen - 1);
            validation_result result = person.ValidateFirstName();
            Assert.That(result == validation_result.IsValid, Is.True, "Failed to detect a valid first name");

            //test invalid name len
            person.Firstname = new string('b', PersonLib.Settings.MaxFirstNameLen + 2);
            result = person.ValidateFirstName();
            Assert.That(result == validation_result.OutOfRange, Is.True, "Failed to detect out of bounds First Name");
        }

        [Test]
        public void FirstName_TestCharacterValidity()
        {
            BasePerson person = new BasePerson();
            validation_result result;

            //test invalid characters
            person.Firstname = new string('#', PersonLib.Settings.MaxSurNameLen - 1);
            result = person.ValidateFirstName();
            Assert.That(result == validation_result.InvalidCharacters, Is.True, "Failed to invalid characters in first name");

            //test for apostrophy
            person.Firstname = new string("b'b");
            result = person.ValidateFirstName();
            Assert.That(result == validation_result.IsValid, Is.True, "Failed to pass a valid first name");
        }

        [Test]
        public void SurName_TestEmptyValue()
        {
            BasePerson person = new BasePerson();
            //defaults to empty, assigning for clarity
            person.Surname = string.Empty;

            validation_result result = person.ValidateSurName();
            Assert.That(result == validation_result.EmptyValue, Is.True, "An empty sur name incorrectly passed validation.");
        }

        [Test]
        public void SurName_TestLengthValidation()
        {
            BasePerson person = new BasePerson();
            validation_result result;

            //test using a repeating character 
            person.Surname = new string('a', PersonLib.Settings.MaxSurNameLen - 1);
            result = person.ValidateSurName();
            Assert.That(result == validation_result.IsValid, Is.True, "Failed to detect a valid surname");

            //test invalid name len
            person.Surname = new string('b', PersonLib.Settings.MaxSurNameLen + 2);
            result = person.ValidateSurName();
            Assert.That(result == validation_result.OutOfRange, Is.True, "Failed to detect out of bounds surnameame");
        }

        [Test]
        public void SurName_TestCharacterValidity()
        {
            BasePerson person = new BasePerson();
            validation_result result;

            //test invalid characters
            person.Surname = new string('#', PersonLib.Settings.MaxSurNameLen - 1);
            result = person.ValidateSurName();
            Assert.That(result == validation_result.InvalidCharacters, Is.True, "Failed to invalid characters in surname");

            //test for apostrophy
            person.Surname = new string("b'b");
            result = person.ValidateSurName();
            Assert.That(result == validation_result.IsValid, Is.True, "Failed to detect out of range surname");
        }

        [Test]
        public void DateOfBirth_TestDateChecking()
        {
            BasePerson person = new BasePerson();
            validation_result result;

            //test empty string
            person.DateOfBirth = string.Empty;
            result = person.ValidateDateOfBirth();
            Assert.That(result == validation_result.EmptyValue, Is.True, "Failed to detect a blank DOB");

            //test bad date
            person.DateOfBirth = "13/1/2022";
            result = person.ValidateDateOfBirth();
            Assert.That(result == validation_result.InvalidCharacters, Is.True, "Failed to detect an invalid date");

            //test proper date
            person.DateOfBirth = DateTime.Now.ToShortDateString();
            result = person.ValidateDateOfBirth();
            Assert.That(result == validation_result.IsValid, Is.True, "Failed to pass a correct date");

        }

        [Test]
        public void TestMinimumAge_BelowMinimum()
        {
            BasePerson testPerson = new Person();
            bool result;

            //generate out of range DOB
            int Year = DateTime.Now.Year - (PersonLib.Settings.MinAge - 1);
            testPerson.DateOfBirth = new DateTime(Year, 1, 1).ToShortDateString();
            result = testPerson.IsMinimumAge();
            Assert.That(result, Is.False, "Age validition did not detect a below minimum valu");
        }

        [Test]
        public void TestMinimumAge_PastMinimum()
        {
            BasePerson testPerson = new Person();
            bool result;

            //generate out of range DOB
            int Year = DateTime.Now.Year - (PersonLib.Settings.MinAge + 1);
            testPerson.DateOfBirth = new DateTime(Year, 1, 1).ToShortDateString();
            result = testPerson.IsMinimumAge();
            Assert.That(result, Is.True, "Age validition did not detect a valid value");
        }

        [Test]
        public void Test_RequiresParentalAuthorization()
        {
            BasePerson testPerson = new Person();
            bool result;

            int Year = DateTime.Now.Year - (PersonLib.Settings.MaxAgeOverrideReq - 1);
            testPerson.DateOfBirth = new DateTime(Year, 1, 1).ToShortDateString();
            result = testPerson.ReqPermissionOverride();
            Assert.That(result, Is.True, "Failed to detect a person who requires parental authorization");
        }

        [Test]
        public void Test_DoesNotRequiresParentalAuthorization()
        {
            BasePerson testPerson = new Person();
            bool result;

            int Year = DateTime.Now.Year - (PersonLib.Settings.MaxAgeOverrideReq + 1);
            testPerson.DateOfBirth = new DateTime(Year, 1, 1).ToShortDateString();
            result = testPerson.ReqPermissionOverride();
            Assert.That(result, Is.False, "Failed to detect a person who does not require parental authorization");
        }
    }
}