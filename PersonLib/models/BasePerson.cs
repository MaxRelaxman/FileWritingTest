using PersonLib.models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonLib.models
{
    public class BasePerson : IBasePerson
    {
        public string Firstname { get; set; } = string.Empty; 
        public string Surname { get; set; } = string.Empty; 
        public string DateOfBirth { get; set; } = string.Empty;

        /// <summary>
        /// Check for field length and null
        /// </summary>
        /// <returns>True if valid</returns>
        public validation_result ValidateFirstName()
        {
            return ValidateNameField(Firstname, Settings.MaxFirstNameLen);
        }

        /// <summary>
        /// Check for both field length and null/empty values
        /// </summary>
        /// <returns>True if valid</returns>
        public validation_result ValidateSurName()
        {
            return ValidateNameField(Surname, Settings.MaxSurNameLen);
        }

        /// <summary>
        /// Check validity of this person's date of birth
        /// </summary>
        /// <returns>True if valid</returns>
        public validation_result ValidateDateOfBirth()
        {
            DateTime DateTimeChecker;

            //null or empty DOB is always invalid
            if (string.IsNullOrEmpty(DateOfBirth))
            {
                return validation_result.EmptyValue;
            }

            //build a datetime object to test validity of person's dob
            if (DateTime.TryParse(DateOfBirth, out DateTimeChecker))
            {
                return validation_result.IsValid;
            }
            else
            {
                return validation_result.InvalidCharacters;
            }
        }

        /// <summary>
        /// Use Date of Birth to determine if person is of minimum age to be 
        /// entered into the system.
        /// An empty/null date will result in validation failure
        /// </summary>
        /// <returns></returns>
        public bool IsMinimumAge()
        {
            //check basic validity of DOB 
            if (ValidateDateOfBirth() != validation_result.IsValid)
                return false;

            DateTime DOB = stringToDate(DateOfBirth);

            int YearsDiff = CalculateYearDifference(DateTime.Now, DOB);

            if (YearsDiff >= Settings.MinAge)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check to see if parental override is required for this person
        /// Note that this should be used in combination with IsMinimumAge
        /// for complete validation
        /// </summary>
        /// <returns>True if parental permission override is required</returns>
        public bool ReqPermissionOverride()
        {
            //check basic validity of DOB 
            if (ValidateDateOfBirth() != validation_result.IsValid)
                return false;

            DateTime DOB = stringToDate(DateOfBirth);

            int YearsDiff = CalculateYearDifference(DateTime.Now, DOB);

            if (YearsDiff <= Settings.MaxAgeOverrideReq)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return a datetime based on a string value.
        /// NOTE: that validity of string->date conversion should be performed
        /// prior to executing this method
        /// </summary>
        /// <param name="value">string value to be converted to a date</param>
        /// <returns></returns>
        protected DateTime stringToDate(string value)
        {
            DateTime result;

            result = DateTime.Parse(value);
            return result;
        }

        protected int CalculateYearDifference(DateTime Value1, DateTime Value2)
        {
            TimeSpan ts = Value1.Subtract(Value2);

            return (int)(ts.TotalDays / 365);
        }

        protected bool IsStringAlphaOnly(string value)
        {
            //test for alpha and single quote
            return Regex.IsMatch(value, @"^[a-zA-Z']+$");
        }

        /// <summary>
        /// test a name string for length, validity and 
        /// empty strings
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected validation_result ValidateNameField(string value, int MaxLen)
        {
            //required field
            if (string.IsNullOrEmpty(value))
            {
                return validation_result.EmptyValue;
            }

            if (value.Length > MaxLen)
            {
                return validation_result.OutOfRange;
            }
            else
            {
                if (IsStringAlphaOnly(value))
                {
                    return validation_result.IsValid;
                }
                else
                {
                    return validation_result.InvalidCharacters;
                }
            }
        }
    }
}
