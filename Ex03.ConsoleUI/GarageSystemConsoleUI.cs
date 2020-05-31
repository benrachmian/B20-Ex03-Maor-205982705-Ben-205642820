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
2. Present vehicle's license number in garage
3. Update status to vehicle in garage
4. Inflate vehicle tires to max
5. Refuel vehicle
6. Recharge vehicle
7. Print full details of specific vehicle in garage
8. Exit");
                option = GetValidInputs.GetValidInputNumber(1, 8);
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
            } while (option != k_ExitOption);
        }

        public void printAllDetailsToConsole()
        {
            string licenseNumber;
            VehiclesInGarage vehicleToPresent;
            Car carVehicle;
            Motorcycle motorcycleVehicle;
            Truck truckVehicle;
            FuelSystem vehicleFuelSystem;
            BatterySystem vehicleBatterySystem;

            getLicenseNumber(out licenseNumber);
            try
            {
                vehicleToPresent = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                Console.WriteLine(
@"-----------Details-----------
License number: {0}
Model name: {1}
Vehicle owner's name: {2}
Vehicle status in garage: {3}
Tires manufacturer name: {4}
Tires PSI: {5}",
                licenseNumber, 
                vehicleToPresent.VehicleInfo.GetModelName(),
                vehicleToPresent.OwnerName,
                vehicleToPresent.VehicleStatus,
                vehicleToPresent.VehicleInfo.Tires[0].ManufacturerName,
                vehicleToPresent.VehicleInfo.Tires[0].CurrentPSI);
                m_GarageSystem.FindVehicleType(vehicleToPresent.VehicleInfo, out carVehicle, out motorcycleVehicle, out truckVehicle);
                m_GarageSystem.FindEnergySystemType(vehicleToPresent.VehicleInfo.VehicleEnergySourceSystem, out vehicleFuelSystem, out vehicleBatterySystem);
                if (vehicleFuelSystem != null)
                {
                    printFuelSystemDetailsToConsole(vehicleFuelSystem);
                }
                else
                {
                    printBatterySystemDetailsToConsole(vehicleBatterySystem);
                }

                if (carVehicle != null)
                {
                    printCarDetailsToConsole(carVehicle);
                }
                else if(motorcycleVehicle != null)
                {
                    printMotorcycleDetailsToConsole(motorcycleVehicle);
                }
                else
                {
                    printTruckDetailsToConsole(truckVehicle);
                }
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
            i_VehicleBatterySystem.BatteryTimeRemaining);
        }

        private void printFuelSystemDetailsToConsole(FuelSystem i_VehicleFuelSystem)
        {
            Console.WriteLine(
@"Fuel left: {0} liters
Fuel type:{1}",
            i_VehicleFuelSystem.CurrFuelInLiters,
            i_VehicleFuelSystem.FuelType);
        }
        
        public void Recharge()
        {
            string licenseNumber;
            float hoursAmountToAdd, maxAmount;
            VehiclesInGarage vehicleToCharge;

            Console.WriteLine("Insert vehicle's license number:");
            getLicenseNumber(out licenseNumber);
            try
            {
                vehicleToCharge = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                if(vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem is BatterySystem)
                {
                    maxAmount = vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem.GetMaxEnergyPossible();
                    Console.WriteLine("Please insert how many hours of battery you would like to charge:");
                    hoursAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxAmount);
                    m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber, hoursAmountToAdd);
                }
                else
                {
                    //throw excpetion
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

            Console.WriteLine("Insert vehicle's license number:");
            try
            {
                getLicenseNumber(out licenseNumber);
                vehicleToRefuel = m_GarageSystem.GetVehicleByLicenseNumber(licenseNumber);
                if (vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem is FuelSystem)
                {

                    fuelType = getFuelType();
                    maxAmount = vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.GetMaxEnergyPossible();
                    Console.WriteLine("Please insert how many liters of fuel you would like to refuel:");
                    fuelAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxAmount);
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
            Console.WriteLine("----------------Returning to menu----------------");
        }

        public void ChangeVehicleStatus()
        {
            string licenseNumber;

            Console.WriteLine("Insert vehicle's license number:");
            getLicenseNumber(out licenseNumber);
            Console.WriteLine(
@"Which status would you like to change:
{0}. In repair status
{1}. Repaired status
{2}. Paid status",
            (int)eVehicleStatuses.InRepair,
            (int)eVehicleStatuses.Repaired,
            (int)eVehicleStatuses.Paid);
            int Option = GetValidInputs.GetValidInputNumber(1, 4);
            m_GarageSystem.ChangeVehicleStatus(licenseNumber, (eVehicleStatuses)Option);
            Console.WriteLine("----------------Returning to menu----------------");
        }

        public void PresentVehicleLicenseNumberInGarageToConsole()
        {
            Console.WriteLine(
@"Which vehicle's license number do you want to see:
1. In repair status
2. Repaired status
3. Paid status
4. All");
            int Option = GetValidInputs.GetValidInputNumber(1, 4);
            Console.WriteLine("----------------The vehicles are:----------------");
            foreach (VehiclesInGarage vehicle in m_GarageSystem.VehiclesInTheGarage.Values)
            {
                if (Option == 4 || vehicle.VehicleStatus == (eVehicleStatuses)Option)
                {
                    Console.WriteLine(vehicle.VehicleInfo.LicenseNumber);
                }
            }
            Console.WriteLine("---------------------Returning to menu-----------------------------");
            //System.Threading.Thread.Sleep(500);
        }

        public void AddNewVehicleToGarage()
        {
            Console.WriteLine("Please insert the vehicle's owner's name:");
            string ownersName = GetValidInputs.GetValidStringOnlyLetters("vehicle's owner's name",VehiclesInGarage.k_MinCharactersForOwnersName,VehiclesInGarage.k_MaxCharactersForOwnersName);
            Console.WriteLine("Please insert vehicle's owner's phone number:");
            string ownersPhoneNumber = GetValidInputs.GetValidPhoneNumber();
            Vehicle vehicleToAdd = getNewVehicleInfo();
            VehiclesInGarage newVehicle = new VehiclesInGarage(ownersName, ownersPhoneNumber, vehicleToAdd);
            m_GarageSystem.AddNewVehicleToGarage(newVehicle);
            Console.WriteLine("----------------Adding new vehicle to the system----------------");
            //System.Threading.Thread.Sleep(2000);
            Console.WriteLine("----------------The vehicle was added successfully!----------------");
            //System.Threading.Thread.Sleep(1000);
            Console.WriteLine("---------------------Returning to menu-----------------------------");
            //System.Threading.Thread.Sleep(500);
        }

        private Vehicle getNewVehicleInfo()
        {
            Vehicle newVehicle;
            eTypeOfVehicle typeOfVehicle;
            string vehicleModel, licenseNumber;
            float currEnergyLeft,maxEnergyPossible, maxPSI;
            Tire[] vehicleTires;
            EnergySourceSystem vehicleEnergySourceSystem;
            eTypeOfEnergy typeOfEnergy;
            eFuelType? typeOfFuel;
            int numOfWheels;
            

            getLicenseNumber(out licenseNumber);
            getModel(out vehicleModel);
            getVehiliceType(out typeOfVehicle);
            getTypeOfEnergy(typeOfVehicle, out typeOfEnergy);
            
            getNumOfWheelsMaxPsiTypeOfEnergyTypeOfFuelMaxAndCurrEnergyTiresCreateEnergySourceSystem
                (typeOfVehicle,
                out numOfWheels, 
                out maxPSI,
                out typeOfEnergy, 
                out typeOfFuel,
                out maxEnergyPossible,
                out vehicleTires,
                out vehicleEnergySourceSystem,
                out currEnergyLeft);

            if (typeOfVehicle == eTypeOfVehicle.RegularMotorcycle || typeOfVehicle == eTypeOfVehicle.ElectricMotorcycle)
            {
                eLicenseTypes licenseType;
                int engineVolume;
                getLicenseType(out licenseType);
                getEngineVolume(out engineVolume);
                newVehicle = CreateVehicles.CreateMotorcycle(vehicleModel, licenseNumber, vehicleTires, vehicleEnergySourceSystem, licenseType, engineVolume);
            }
            else if(typeOfVehicle == eTypeOfVehicle.ElectricCar || typeOfVehicle == eTypeOfVehicle.RegularCar)
            {
                eCarColors carColor;
                uint numOfDoors;

                getCarColor(out carColor);
                getNumOfDoors(out numOfDoors);
                newVehicle = CreateVehicles.CreateCar(vehicleModel, licenseNumber, vehicleTires, vehicleEnergySourceSystem, carColor, numOfDoors);
            }
            else // truck
            {
                bool isCarryingDangerousMaterials;
                float trunkVolume;

                askIfCarryingDangerousMaterials(out isCarryingDangerousMaterials);
                getTrunkVolume(out trunkVolume);
                newVehicle = CreateVehicles.CreateTruck(vehicleModel, licenseNumber, vehicleTires, vehicleEnergySourceSystem, isCarryingDangerousMaterials, trunkVolume);
            }

            return newVehicle;
        }

        private void getTrunkVolume(out float trunkVolume)
        {
            Console.WriteLine("Please insert the trunk volume");
            trunkVolume = GetValidInputs.GetValidInputNumber(1,Truck.k_MaxTrunkVolume);
        }

        private void askIfCarryingDangerousMaterials(out bool o_IsCarryingDangerousMaterials)
        {
            int answerInInt;
            Console.WriteLine("Does the truck carry dangerous materials?{0}1.Yes{0}2.No", Environment.NewLine);
            answerInInt = GetValidInputs.GetValidInputNumber(1, 2);
            o_IsCarryingDangerousMaterials = answerInInt == 1 ? true : false;
        }

        private void getNumOfWheelsMaxPsiTypeOfEnergyTypeOfFuelMaxAndCurrEnergyTiresCreateEnergySourceSystem
            (eTypeOfVehicle i_TypeOfVehicle,
            out int o_NumOfWheels,
            out float o_MaxPSI,
            out eTypeOfEnergy o_TypeOfEnergy,
            out eFuelType? o_TypeOfFuel,
            out float o_MaxEnergyToUse,
            out Tire[] o_Tires,
            out EnergySourceSystem o_EnergySourseSystem,
            out float o_CurrEnergyLeft)
        {
            switch(i_TypeOfVehicle)
            {
                case eTypeOfVehicle.ElectricCar:
                    {
                        o_NumOfWheels = Car.k_NumOfWheelsInCar;
                        o_MaxPSI = Car.k_MaxPsiInElectricCar;
                        o_TypeOfEnergy = eTypeOfEnergy.Electic;
                        o_TypeOfFuel = null;
                        o_MaxEnergyToUse = Car.k_MaxHoursInElectricCar;
                        getCurrEnergyLeft(o_TypeOfEnergy,o_MaxEnergyToUse, out o_CurrEnergyLeft);
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new BatterySystem(o_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
                case eTypeOfVehicle.ElectricMotorcycle:
                    {
                        o_NumOfWheels = Motorcycle.k_NumOfWheelsInMotorcycle;
                        o_MaxPSI = Motorcycle.k_MaxPsiInElectricMotorcycle;
                        o_TypeOfEnergy = eTypeOfEnergy.Electic;
                        o_TypeOfFuel = null;
                        o_MaxEnergyToUse = Motorcycle.k_MaxHoursInElectricMotorcycle;
                        getCurrEnergyLeft(o_TypeOfEnergy, o_MaxEnergyToUse, out o_CurrEnergyLeft);
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new BatterySystem(o_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
                case eTypeOfVehicle.RegularCar:
                    {
                        o_NumOfWheels = Car.k_NumOfWheelsInCar;
                        o_MaxPSI = Car.k_MaxPsiInFuelCar;
                        o_TypeOfEnergy = eTypeOfEnergy.Fuel;
                        o_TypeOfFuel = eFuelType.Octan96;
                        o_MaxEnergyToUse = Car.k_MaxLitersInFuelCar;
                        getCurrEnergyLeft(o_TypeOfEnergy, o_MaxEnergyToUse, out o_CurrEnergyLeft);
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new FuelSystem((eFuelType)o_TypeOfFuel, o_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
                case eTypeOfVehicle.RegularMotorcycle:
                    {
                        o_NumOfWheels = Motorcycle.k_NumOfWheelsInMotorcycle;
                        o_MaxPSI = Motorcycle.k_MaxPsiInElectricMotorcycle;
                        o_TypeOfEnergy = eTypeOfEnergy.Fuel;
                        o_TypeOfFuel = eFuelType.Octan95;
                        o_MaxEnergyToUse = Motorcycle.k_MaxLitersInFuelMotorcycle;
                        getCurrEnergyLeft(o_TypeOfEnergy, o_MaxEnergyToUse, out o_CurrEnergyLeft);
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new FuelSystem((eFuelType)o_TypeOfFuel, o_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
                default: //eTypeOfVehicle.Truck:
                    {
                        o_NumOfWheels = Truck.k_NumOfWheelsInTruck;
                        o_MaxPSI = Truck.k_MaxPsiInTruck;
                        o_TypeOfEnergy = eTypeOfEnergy.Fuel;
                        o_TypeOfFuel = eFuelType.Diesel;
                        o_MaxEnergyToUse = Truck.k_MaxLitersInFuelTruck;
                        getCurrEnergyLeft(o_TypeOfEnergy, o_MaxEnergyToUse, out o_CurrEnergyLeft);
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new FuelSystem((eFuelType)o_TypeOfFuel, o_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
            }
        }

        private void getTypeOfEnergy(eTypeOfVehicle i_TypeOfVehicle, out eTypeOfEnergy o_TypeOfEnergy)
        {
            if(i_TypeOfVehicle == eTypeOfVehicle.ElectricCar || i_TypeOfVehicle == eTypeOfVehicle.ElectricMotorcycle)
            {
                o_TypeOfEnergy = eTypeOfEnergy.Electic;
            }
            else
            {
                o_TypeOfEnergy = eTypeOfEnergy.Fuel;
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
            (int)eFuelType.Diesel);
            return (eFuelType)GetValidInputs.GetValidInputNumber(1, FuelSystem.k_NumOfFuelTypes);
        }

        private void getNumOfDoors(out uint o_NumOfDoors)
        {
            Console.WriteLine("Please insert how many doors does the car have");
            o_NumOfDoors = (uint)GetValidInputs.GetValidInputNumber(Car.k_MinNumOfDoors, Car.k_MaxNumOfDoors);
        }

        private void getCarColor(out eCarColors o_CarColor)
        {
            Console.WriteLine(
@"Please insert vehicle's color:
{0}.Red
{1}.White
{2}.Black
{3}.Silver",
            (int)eCarColors.Red,
            (int)eCarColors.White,
            (int)eCarColors.Black,
            (int)eCarColors.Silver);
            o_CarColor = (eCarColors)GetValidInputs.GetValidInputNumber(1, Car.k_NumOfColorsOptions);
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

        private void getTires(int i_NumOfTires,float i_MaxPSI, out Tire[] o_Tires)
        {
            Console.WriteLine("Please enter the tires manufacturer");
            string tireManufacturer = GetValidInputs.GetValidTireManufacturer();
            Console.WriteLine("Please enter the tires current PSI");
            float currentPSI = GetValidInputs.GetValidPSI();

            o_Tires = new Tire[i_NumOfTires];
            for (int i = 0; i < i_NumOfTires; i++)
            {
                o_Tires[i] = new Tire(tireManufacturer, currentPSI, i_MaxPSI);
            }
        }

        private void getModel(out string o_VehicleModel)
        {
            Console.WriteLine("Please insert the vehicle's model:");
            o_VehicleModel = GetValidInputs.GetValidModel();
        }

        private void getLicenseNumber(out string o_LicenseNumber)
        {
            Console.WriteLine("Please insert the vehicle's license number:");
            o_LicenseNumber = GetValidInputs.GetValidLicenseNumber();
        }

        private void getEngineVolume(out int o_EngineVolume)
        {
            Console.WriteLine("Please enter the motorcycle's engine volume");
            o_EngineVolume = GetValidInputs.GetValidInputNumber(Motorcycle.k_MinEngineVolume, Motorcycle.k_MaxEngineVolume);
        }

        private void getLicenseType(out eLicenseTypes o_LicenseType)
        {
            Console.WriteLine(
@"Please insert your license type:
{0}.A
{1}.A1
{2}.AA
{3}.B",
            (int)eLicenseTypes.A,
            (int)eLicenseTypes.A1,
            (int)eLicenseTypes.AA,
            (int)eLicenseTypes.B);
            o_LicenseType = (eLicenseTypes)GetValidInputs.GetValidInputNumber(1, Motorcycle.k_NumOfLicenseTypesOptions);
        }

        private void getCurrEnergyLeft(eTypeOfEnergy i_TypeOfEnergy,float i_MaxEnergyPossible, out float o_CurrEnergyLeft)
        {
            string leftEnergyInStr;
            switch (i_TypeOfEnergy)
            {
                case eTypeOfEnergy.Fuel:
                    {
                        Console.WriteLine("Please insert how much fuel there is left in the vehicle");
                        break;
                    }
                case eTypeOfEnergy.Electic:
                    {
                        Console.WriteLine("Please insert how much battery there is left in the vehicle");
                        break;
                    }
            }

            leftEnergyInStr = Console.ReadLine(); 
            while (!(float.TryParse(leftEnergyInStr, out o_CurrEnergyLeft)) || o_CurrEnergyLeft < 0 || o_CurrEnergyLeft > i_MaxEnergyPossible)
            {
                Console.WriteLine("You must enter only possitive numbers that are smaller than {0}!",i_MaxEnergyPossible);
                leftEnergyInStr = Console.ReadLine();
            }
        }
    }
}
