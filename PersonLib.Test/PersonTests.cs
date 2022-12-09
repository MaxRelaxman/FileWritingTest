using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.Test
{
    internal class PersonTests
    {
        [SetUp]
        public void Setup()
        {
            //set configs to known values for testing
            PersonLib.Settings.MaxSurNameLen = 15;
            PersonLib.Settings.MaxFirstNameLen = 15;
            PersonLib.Settings.MaxAgeOverrideReq = 18;
            PersonLib.Settings.MinAge = 10;
        }

        [Test]
        public void TestMaritalStatus_Empty()
        {
            Person testPerson = new Person();
            validation_result result;

            //empty
            testPerson.MaritalStatus = string.Empty;
            result = testPerson.ValidateMaritalStatus();
            Assert.That(result == validation_result.EmptyValue, Is.True, "An empty marital status incorrectly passed validation.");
        }

        [Test]
        public void TestMaritalStatus_InvalidValues()
        {
            Person testPerson = new Person();
            validation_result result;

            //test invalid state
            testPerson.MaritalStatus = "xxxxxx123";
            result = testPerson.ValidateMaritalStatus();
            Assert.That(result == validation_result.InvalidCharacters, Is.True, "An invalid marital status incorrectly passed validation.");
        }

        [Test]
        [TestCase("married")]
        [TestCase("SinGle")]
        public void TestMaritalStatus_ValidValues(string value)
        {
            Person testPerson = new Person();
            validation_result result;

            //test valid states
            testPerson.MaritalStatus = value;
            result = testPerson.ValidateMaritalStatus();
            Assert.That(result == validation_result.IsValid, Is.True, "An valid marital status did not pass validation");

            testPerson.MaritalStatus = value;
            result = testPerson.ValidateMaritalStatus();
            Assert.That(result == validation_result.IsValid, Is.True, "An valid marital status did not pass validation");
        }
    }
}
