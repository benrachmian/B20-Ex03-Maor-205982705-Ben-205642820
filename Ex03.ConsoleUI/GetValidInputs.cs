using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GetValidInputs
    {
        private const int k_MinCharsForTireManufacturerName = 4;
        private const int k_MaxCharsForTireManufacturerName = 50;
        private const int k_MinLicenseNumber = 6;
        private const int k_MaxLicenseNumber = 8;

        public static float GetPositiveNumber()
        {
            float inputNum;

            while (!float.TryParse(Console.ReadLine(), out inputNum) || inputNum < 0)
            {
                Console.WriteLine("You must insert positive number! Try again!");
            }

            return inputNum;
        }

        public static int GetValidInputNumber(int i_MinRange, int i_MaxRange)
        {
            string inputStr;
            int inputNum;
            bool parseSuccessed;

            inputStr = Console.ReadLine();
            parseSuccessed = int.TryParse(inputStr, out inputNum);
            while (!parseSuccessed || !isInNumberRange(i_MinRange, i_MaxRange, inputNum))
            {
                if (!parseSuccessed)
                {
                    Console.WriteLine("You must enter digits only! Try again!");
                }

                inputStr = Console.ReadLine();
                parseSuccessed = int.TryParse(inputStr, out inputNum);
            }

            return inputNum;
        }

        public static int GetValidInputNumber(float i_MinRange, float i_MaxRange)
        {
            string inputStr;
            int inputNum;
            bool parseSuccessed;

            inputStr = Console.ReadLine();
            parseSuccessed = int.TryParse(inputStr, out inputNum);
            while (!parseSuccessed || !isInNumberRange(i_MinRange, i_MaxRange, inputNum))
            {
                if (!parseSuccessed)
                {
                    Console.WriteLine("You must enter digits only! Try again!");
                }

                inputStr = Console.ReadLine();
                parseSuccessed = int.TryParse(inputStr, out inputNum);
            }

            return inputNum;
        }

        private static bool isInNumberRange(int i_MinRange, int i_MaxRange, int i_Input)
        {
            bool isValid = i_Input >= i_MinRange && i_Input <= i_MaxRange;

            if (!isValid)
            {
                Console.WriteLine("You must enter a number between {0} and {1}. Please try again!", i_MinRange, i_MaxRange);
            }

            return isValid;
        }

        private static bool isInNumberRange(float i_MinRange, float i_MaxRange, float i_Input)
        {
            bool isValid = i_Input >= i_MinRange && i_Input <= i_MaxRange;

            if (!isValid)
            {
                Console.WriteLine("You must enter a number between {0} and {1}. Please try again!", i_MinRange, i_MaxRange);
            }

            return isValid;
        }

        public static string GetValidTireManufacturer()
        {
            return GetValidString("tire manufacturer");
        }

        public static string GetValidPhoneNumber()
        {
            int phoneNumberInInt;
            StringBuilder phoneNumber = new StringBuilder(11);
            string tempPhoneNumber = Console.ReadLine();

            while(tempPhoneNumber.Length!=10 || tempPhoneNumber[0]!='0' || !int.TryParse(tempPhoneNumber, out phoneNumberInInt))
            {
                Console.WriteLine(
@"The phone number must contain exactly 10 digits that begin with '0'. No other characters are allowed! Please try again!");
                tempPhoneNumber = Console.ReadLine();
            }
            phoneNumber.Append(tempPhoneNumber);
            phoneNumber.Insert(3, '-');
            return phoneNumber.ToString();
        }

        public static string GetValidString(string i_TargetName)
        {
            string inputString;
            bool isValid = false;

            do
            {
                inputString = Console.ReadLine();
                if (inputString.Length < k_MinCharsForTireManufacturerName || inputString.Length > k_MaxCharsForTireManufacturerName)
                {
                    Console.WriteLine("A {0} name must be at least {1} and maximum {2} characters. Please try again!", i_TargetName, k_MinCharsForTireManufacturerName, k_MaxCharsForTireManufacturerName);
                }
                else if (!doesContainOnlyLettersAndNumbers(inputString))
                {
                    Console.WriteLine("A {0} name must contain only letters or numbers.Please try again!", i_TargetName);
                }
                else
                {
                    isValid = true;
                }
            } while (!isValid);

            return inputString;
        }

        private static bool doesContainOnlyLettersAndNumbers(string i_Str)
        {
            bool isOnlyLetters = true;

            foreach (char c in i_Str)
            {
                if (!(c >= 'A' && c <= 'Z') && !(c >= 'a' && c <= 'z') && !(c >= '0' && c <= '9'))
                {
                    isOnlyLetters = false;
                }
            }

            return isOnlyLetters;
        }

        public static float GetValidPSI()
        {
            // string PsiInput;
            float validPSI;

            while(!float.TryParse(Console.ReadLine(), out validPSI))
            {

            }

            return ;
        }

        public static string GetValidModel()
        {
            return GetValidString("model");
        }

        public static string GetValidLicenseNumber()
        {
            return GetValidInputNumber(k_MinLicenseNumber,k_MaxLicenseNumber).ToString();
        }
    }
}
