using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageSystemConsoleUI
    {
        private const int k_NumTypesOfVehicles = 5;
        private const int k_ExitOption = 8;

        private GarageSystem m_GarageSystem;

        public GarageSystemConsoleUI() // c'tor
        {
            m_GarageSystem = new GarageSystem();
        }

        public void Menu()
        {
            int option;
            Console.WriteLine(
@"Hello!
Welcome to the garage system!");

            do
            {
                Console.WriteLine(
@"What would you like to do?
1. Insert a new car to garage
2. Present all of the vehicles license numbers in garage
3. Update status to vehicle in garage
4. Inflate vehicle tires to max
5. Refuel vehicle
6. Recharge vehicle
7. Print full details of specific vehicle in garage
8. Exit");
                option = GetValidInputs.GetValidInputNumber(1, 8);
                Console.Clear();
                try
                {
                    switch (option)
                    {
                        case 1:
                            {
                                AddNewVehicleToGarage();
                                break;
                            }
                        case 2:
                            {
                                PresentVehicleLicenseNumberInGarageToConsole();
                                break;
                            }
                        case 3:
                            {
                                ChangeVehicleStatus();
                                break;
                            }
                        case 4:
                            {
                                InflateTires();
                                break;
                            }
                        case 5:
                            {
                                Refuel();
                                break;
                            }
                        case 6:
                            {
                                Recharge();
                                break;
                            }
                        case 7:
                            {
                                printAllDetailsToConsole();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Bye-bye!");
                                Environment.Exit(0);
                                break;
                            }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("----------------Returning to menu----------------");
                    System.Threading.Thread.Sleep(1000);
                }
            } while (option != k_ExitOption);
        }

        public void printAllDetailsToConsole()
        {
            string licenseNumber;
            VehiclesInGarage vehicleToPresent;
            
            getLicenseNumber(out licenseNumber);
            try
            {
                vehicleToPresent = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                Console.WriteLine(vehicleToPresent.ToString());
                Console.WriteLine(vehicleToPresent.VehicleInfo.ToString());
                
            }
            catch(KeyNotFoundException i_KeyNotFoundException)
            {
                Console.WriteLine(i_KeyNotFoundException.Message.ToString());
            }
            Console.WriteLine("----------------Returning to menu----------------");
        }

        private void printTruckDetailsToConsole(Truck i_TruckToPrint)
        {
            Console.WriteLine(
@"Is carrying dangerous matirials: {0}
Trunk volume: {1}",
           i_TruckToPrint.IsCarryingDangerousMaterials ? "Yes" : "No",
           i_TruckToPrint.TrunkVolume);
        }

        private void printMotorcycleDetailsToConsole(Motorcycle i_MotorcycleToPrint)
        {
            Console.WriteLine(
@"License type: {0}
Engine volume: {1}",
            i_MotorcycleToPrint.LicenseType,
            i_MotorcycleToPrint.EngineVolume);
        }

        private void printCarDetailsToConsole(Car i_CarToPrint)
        {
            Console.WriteLine(
@"Car color: {0}
Number of doors: {1}",
            i_CarToPrint.CarColor,
            i_CarToPrint.NumOfDoors);
        }

        private void printBatterySystemDetailsToConsole(BatterySystem i_VehicleBatterySystem)
        {
            Console.WriteLine(
@"Battery left: {0} hours",
            i_VehicleBatterySystem.CurrEnergy);
        }

        private void printFuelSystemDetailsToConsole(FuelSystem i_VehicleFuelSystem)
        {
            Console.WriteLine(
@"Fuel left: {0} liters
Fuel type:{1}",
            i_VehicleFuelSystem.CurrEnergy,
            i_VehicleFuelSystem.FuelType);
        }
        
        public void Recharge()
        {
            string licenseNumber;
            float hoursAmountToAdd, maxAmount;
            VehiclesInGarage vehicleToCharge;

            getLicenseNumber(out licenseNumber);
            try
            {
                vehicleToCharge = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                if(vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem is BatterySystem)
                {
                    maxAmount = vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem.MaxEnergyPossible;
                    Console.WriteLine("Please insert how many hours of battery you would like to charge:");
                    hoursAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxAmount);
                    m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber, hoursAmountToAdd);
                }
                else
                {
                    Console.WriteLine("You tried to charge an elctric vehicle with fuel!");
                }
            }
            catch
            {
            }
            Console.WriteLine("----------------Returning to menu----------------");
        }

        public void Refuel()
        {
            string licenseNumber;
            eFuelType fuelType;
            float fuelAmountToAdd, maxAmount;
            VehiclesInGarage vehicleToRefuel;

            try
            {
                getLicenseNumber(out licenseNumber);
                vehicleToRefuel = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                if (vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem is FuelSystem)
                {

                    fuelType = getFuelType();
                    maxAmount = vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.MaxEnergyPossible;
                    Console.WriteLine(
@"You can refuel up to {0} liters.
Please insert how many liters of fuel you would like to refuel:",
                    (vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.MaxEnergyPossible - vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.CurrEnergy));
                    fuelAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxAmount);
                    m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber,fuelAmountToAdd,null);
                    m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber, fuelAmountToAdd, fuelType);
                }
            }
            catch(ArgumentNullException)
            {
                Console.WriteLine("SNDJASDASDASDD");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            //else
            //{
            //    //throw excpetion
            //}
            Console.WriteLine("----------------Returning to menu----------------");
        }

        public void InflateTires()
        {
            string licenseNumber;

            getLicenseNumber(out licenseNumber);
            m_GarageSystem.InflateTiresToMax(licenseNumber);
            Console.WriteLine("The tires were inflated successfully!");
            Console.WriteLine("----------------Returning to menu----------------");
            System.Threading.Thread.Sleep(2000);
        }

        public void ChangeVehicleStatus()
        {
            string licenseNumber;

            getLicenseNumber(out licenseNumber);
            Console.WriteLine(
@"Which of the following statuses you'd like to set the vehicle to:
{0}. In repair 
{1}. Repaired 
{2}. Paid ",
            (int)eVehicleStatuses.InRepair,
            (int)eVehicleStatuses.Repaired,
            (int)eVehicleStatuses.Paid);
            int Option = GetValidInputs.GetValidInputNumber(1, 3);
            try
            {
                m_GarageSystem.ChangeVehicleStatus(licenseNumber, (eVehicleStatuses)Option);
                Console.WriteLine("The vehicle status has changed successfully!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("----------------Returning to menu----------------");
            System.Threading.Thread.Sleep(2000);
        }

        public void PresentVehicleLicenseNumberInGarageToConsole()
        {
            int counter = 0;
            Console.WriteLine(
@"Which status filter would you like to present?
1. In repair status
2. Repaired status
3. Paid status
4. No filter - Present all");
            int Option = GetValidInputs.GetValidInputNumber(1, 4);
            Console.WriteLine("----------------The vehicles are:----------------");
            foreach (VehiclesInGarage vehicle in m_GarageSystem.VehiclesInTheGarage.Values)
            {
                if (Option == 4 || vehicle.VehicleStatus == (eVehicleStatuses)Option)
                {
                    Console.WriteLine(vehicle.VehicleInfo.LicenseNumber);
                    counter++;
                }
            }
            if(counter == 0)
            {
                Console.WriteLine("There are no vehicles in that status right now!");
            }
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("---------------Returning to menu-----------------");
            System.Threading.Thread.Sleep(500);
        }

        public void AddNewVehicleToGarage()
        {
            string ownersName = getOwnersName();
            Console.WriteLine("Please insert vehicle's owner's phone number: ");
            string ownersPhoneNumber = GetValidInputs.GetValidPhoneNumber();
            Vehicle vehicleToAdd = getNewVehicleInfo();
            VehiclesInGarage newVehicle = new VehiclesInGarage(ownersName, ownersPhoneNumber, vehicleToAdd);
            m_GarageSystem.AddNewVehicleToGarage(newVehicle);
            Console.WriteLine("----------------Adding new vehicle to the system----------------");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("----------------The vehicle was added successfully!-------------");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }

        private string getOwnersName()
        {
            string firstName,lastName;
            StringBuilder ownersName = new StringBuilder();
            Console.WriteLine("Please insert the vehicle's owner's first name:");
            firstName = GetValidInputs.GetValidStringOnlyLetters(VehiclesInGarage.k_MinCharactersForOwnersName, VehiclesInGarage.k_MaxCharactersForOwnersName);
            Console.WriteLine("Please insert the vehicle's owner's last name:");
            lastName = GetValidInputs.GetValidStringOnlyLetters(VehiclesInGarage.k_MinCharactersForOwnersName, VehiclesInGarage.k_MaxCharactersForOwnersName);
            ownersName.Append(firstName);
            ownersName.Append(" ");
            ownersName.Append(lastName);

            return ownersName.ToString();
        }

        private Vehicle getNewVehicleInfo()
        {
            Vehicle newVehicle;
            eTypeOfVehicle typeOfVehicle;
            string licenseNumber;

            getVehiliceType(out typeOfVehicle);
            getLicenseNumber(out licenseNumber);
            newVehicle = CreateVehicles.CreateVehicle(typeOfVehicle, licenseNumber);
            Console.WriteLine("Please enter the following details: ");
            PrintMessageAndGetValuesForAllTypeVehicle(newVehicle);
            PrintMessageAndGetValuesForSpecificTypeVehicle(newVehicle);

            return newVehicle;
        }

        private void PrintMessageAndGetValuesForAllTypeVehicle(Vehicle i_NewVehicle)
        {
            string input;
            bool isValid;
            Dictionary<int, string> vehicleParams = i_NewVehicle.GetVehicleParams();

            foreach (KeyValuePair<int, string> param in vehicleParams)
            {
                do
                {
                    isValid = true;
                    Console.WriteLine(param.Value);
                    input = Console.ReadLine();
                    try
                    {
                        i_NewVehicle.SetVehicleParameters(param.Key, input);
                    }
                    catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                    {
                        Console.WriteLine(i_ValueOutOfRangeException.Message);
                        isValid = false;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine(i_ArgumentException.Message);
                        isValid = false;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine(i_FormatException.Message);
                        isValid = false;
                    }

                } while (!isValid);
            }
        }

        private void PrintMessageAndGetValuesForSpecificTypeVehicle(Vehicle i_NewVehicle)
        {
            string input;
            bool isValid;
            Dictionary<int, string> specificTypeParams = i_NewVehicle.GetParams();

            foreach (KeyValuePair<int, string> param in specificTypeParams)
            {
                do
                {
                    isValid = true;
                    Console.WriteLine(param.Value);
                    input = Console.ReadLine();
                    try
                    {
                        i_NewVehicle.SetSpecificTypeParams(param.Key, input);
                    }
                    catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                    {
                        Console.WriteLine(i_ValueOutOfRangeException.Message);
                        isValid = false;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine(i_ArgumentException.Message);
                        isValid = false;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine(i_FormatException.Message);
                        isValid = false;
                    }

                } while (!isValid);
            }
        }

        private eFuelType getFuelType()
        {
            Console.WriteLine(
@"Please insert vehicle's type fuel:
{0}.Octan98
{1}.Octan96
{2}.Octan95
{3}.Diesel",
            (int)eFuelType.Octan98,
            (int)eFuelType.Octan96,
            (int)eFuelType.Octan95,
            (int)eFuelType.Soler);
            return (eFuelType)GetValidInputs.GetValidInputNumber(1, FuelSystem.k_NumOfFuelTypes);
        }

        private void getVehiliceType(out eTypeOfVehicle o_TypeOfVehicle)
        {
            Console.WriteLine(
@"Please insert vehicle's type:
{0}.Regular motorcycle
{1}.Electric motorcycle
{2}.Regular car
{3}.Electric car
{4}.Truck",
            (int)eTypeOfVehicle.RegularMotorcycle,
            (int)eTypeOfVehicle.ElectricMotorcycle,
            (int)eTypeOfVehicle.RegularCar,
            (int)eTypeOfVehicle.ElectricCar,
            (int)eTypeOfVehicle.Truck);
            o_TypeOfVehicle = (eTypeOfVehicle)GetValidInputs.GetValidInputNumber(1, k_NumTypesOfVehicles);
        }

        private void getLicenseNumber(out string o_LicenseNumber)
        {
            Console.WriteLine("Please insert the vehicle's license number:");
            o_LicenseNumber = GetValidInputs.GetValidLicenseNumber();
        }

        
    }
}

