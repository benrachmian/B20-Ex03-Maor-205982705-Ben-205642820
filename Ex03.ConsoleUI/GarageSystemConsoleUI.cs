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
       
        private GarageSystem m_GarageSystem;

        public GarageSystemConsoleUI() // c'tor
        {
            m_GarageSystem = new GarageSystem();
        }

        public void Menu()
        {
            Console.WriteLine("Hello!{0}Welcome to the garage system!{0}What would you like to do?", Environment.NewLine);
        }

        public void GetAllDetails()
        {
            string licenseNumber, modelNumber;
            VehiclesInGarage vehicleToPresent;
            Car carVehicle = vehicleToPresent.VehicleInfo as Car;
            Motorcycle motorcycleVehicle;
            Truck truckVehicle;

            Console.WriteLine("Insert vehicle's license number:");
            getLicenseNumber(out licenseNumber);
            try
            {
                vehicleToPresent = getVehicleByLicenseNumber(licenseNumber);
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



            }
            catch
            {

            }
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
                vehicleToCharge = getVehicleByLicenseNumber(licenseNumber);
                if(vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem is BatterySystem)
                {
                    maxAmount = vehicleToCharge.VehicleInfo.VehicleEnergySourceSystem.GetMaxEnergyPossible();
                    Console.WriteLine("Please insert how many hours of battery you would like to charge:");
                    hoursAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxAmount);
                }
                else
                {
                    //throw excpetion
                }
            }
            catch
            {
                "max "
            }
            
        }

        private float getMaxBatteryAmount(VehiclesInGarage i_vehicle)
        {
            BatterySystem batterySystem = i_vehicle.VehicleInfo.VehicleEnergySourceSystem as BatterySystem;
            if (batterySystem == null)
            {
                // throw exception
            }

            return batterySystem.MaxBatteryTime;
        }

        private VehiclesInGarage getVehicleByLicenseNumber(string i_LicenseNumber)
        {
            VehiclesInGarage foundVehicle;
            if (!m_GarageSystem.VehiclesInTheGarage.TryGetValue(i_LicenseNumber, out foundVehicle))
            {
                // throw exec
            }

            return foundVehicle;
        }

        public void Refuel()
        {
            string licenseNumber;
            eFuelType fuelType;
            float fuelAmountToAdd, maxAmount;
            VehiclesInGarage vehicleToRefuel;

            Console.WriteLine("Insert vehicle's license number:");
            getLicenseNumber(out licenseNumber);

            vehicleToRefuel = getVehicleByLicenseNumber(licenseNumber);
            if (vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem is FuelSystem)
            {
                fuelType = getFuelType();
                maxAmount = vehicleToRefuel.VehicleInfo.VehicleEnergySourceSystem.GetMaxEnergyPossible();
                Console.WriteLine("Please insert how many liters of fuel you would like to refuel:");
                fuelAmountToAdd = GetValidInputs.GetValidInputNumber(0, maxAmount);
                m_GarageSystem.ProvideSourceEnergyToVehicle(licenseNumber, fuelAmountToAdd, fuelType);
            }
            else
            {
                //throw excpetion
            }


           
        }

        public void InflateTires()
        {
            string licenseNumber;

            Console.WriteLine("Insert vehicle's license number:");
            getLicenseNumber(out licenseNumber);
            m_GarageSystem.InflateTiresToMax(licenseNumber);
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
        }

        public void PresentVehicleLicenseNumberInGarage()
        {
            Console.WriteLine(
@"Which vehicle's license number do you want to see:
1. In repair status
2. Repaired status
3. Paid status
4. All");
            int Option = GetValidInputs.GetValidInputNumber(1, 4);
            foreach (VehiclesInGarage vehicle in m_GarageSystem.VehiclesInTheGarage.Values)
            {
                if (Option == 4 || vehicle.VehicleStatus == (eVehicleStatuses)Option)
                {
                    Console.WriteLine(vehicle.VehicleInfo.LicenseNumber);
                }
            }
        }

        public void AddNewVehicleToGarage()
        {
            Console.WriteLine("Please insert the vehicle's owner's name:");
            string ownersName = GetValidInputs.GetValidString("vehicle's owner's name");
            Console.WriteLine("Please insert vehicle's owner's phone number:");
            string ownersPhoneNumber = GetValidInputs.GetValidPhoneNumber();
            Vehicle vehicleToAdd = getNewVehicleInfo();
            VehiclesInGarage newVehicle = new VehiclesInGarage(ownersName, ownersPhoneNumber, vehicleToAdd);
            m_GarageSystem.AddNewVehicleToGarage(newVehicle);
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
            getCurrEnergyLeft(typeOfEnergy, out currEnergyLeft);
            getNumOfWheelsMaxPsiTypeOfEnergyTypeOfFuelMaxEnergyUseTiresCreateEnergySourceSystem
                (typeOfVehicle,
                out numOfWheels, 
                out maxPSI,
                out typeOfEnergy, 
                out typeOfFuel,
                out maxEnergyPossible,
                out vehicleTires,
                out vehicleEnergySourceSystem,
                currEnergyLeft);

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

        private void getNumOfWheelsMaxPsiTypeOfEnergyTypeOfFuelMaxEnergyUseTiresCreateEnergySourceSystem
            (eTypeOfVehicle i_TypeOfVehicle,
            out int o_NumOfWheels,
            out float o_MaxPSI,
            out eTypeOfEnergy o_TypeOfEnergy,
            out eFuelType? o_TypeOfFuel,
            out float o_MaxEnergyToUse,
            out Tire[] o_Tires,
            out EnergySourceSystem o_EnergySourseSystem,
            float i_CurrEnergyLeft)
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
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new BatterySystem(i_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
                case eTypeOfVehicle.ElectricMotorcycle:
                    {
                        o_NumOfWheels = Motorcycle.k_NumOfWheelsInMotorcycle;
                        o_MaxPSI = Motorcycle.k_MaxPsiInElectricMotorcycle;
                        o_TypeOfEnergy = eTypeOfEnergy.Electic;
                        o_TypeOfFuel = null;
                        o_MaxEnergyToUse = Motorcycle.k_MaxHoursInElectricMotorcycle;
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new BatterySystem(i_CurrEnergyLeft, o_MaxEnergyToUse);
                        break;
                    }
                case eTypeOfVehicle.RegularCar:
                    {
                        o_NumOfWheels = Car.k_NumOfWheelsInCar;
                        o_MaxPSI = Car.k_MaxPsiInFuelCar;
                        o_TypeOfEnergy = eTypeOfEnergy.Fuel;
                        o_TypeOfFuel = eFuelType.Octan96;
                        o_MaxEnergyToUse = Car.k_MaxLitersInFuelCar;
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new FuelSystem((eFuelType)o_TypeOfFuel,i_CurrEnergyLeft,o_MaxEnergyToUse);
                        break;
                    }
                case eTypeOfVehicle.RegularMotorcycle:
                    {
                        o_NumOfWheels = Motorcycle.k_NumOfWheelsInMotorcycle;
                        o_MaxPSI = Motorcycle.k_MaxPsiInElectricMotorcycle;
                        o_TypeOfEnergy = eTypeOfEnergy.Fuel;
                        o_TypeOfFuel = eFuelType.Octan95;
                        o_MaxEnergyToUse = Motorcycle.k_MaxLitersInFuelMotorcycle;
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new FuelSystem((eFuelType)o_TypeOfFuel,i_CurrEnergyLeft,o_MaxEnergyToUse);
                        break;
                    }
                default: //eTypeOfVehicle.Truck:
                    {
                        o_NumOfWheels = Truck.k_NumOfWheelsInTruck;
                        o_MaxPSI = Truck.k_MaxPsiInTruck;
                        o_TypeOfEnergy = eTypeOfEnergy.Fuel;
                        o_TypeOfFuel = eFuelType.Diesel;
                        o_MaxEnergyToUse = Truck.k_MaxLitersInFuelTruck;
                        getTires(o_NumOfWheels, o_MaxPSI, out o_Tires);
                        o_EnergySourseSystem = new FuelSystem((eFuelType)o_TypeOfFuel,i_CurrEnergyLeft,o_MaxEnergyToUse);
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
{0}.Black
{1}.Red
{2}.Silver
{3}.White",
            (int)eCarColors.Black,
            (int)eCarColors.Red,
            (int)eCarColors.Silver,
            (int)eCarColors.White);
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
            Console.Write("Please enter the tires manufacturer");
            string tireManufacturer = GetValidInputs.GetValidTireManufacturer();
            Console.Write("Please enter the tires current PSI");
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

        private void getCurrEnergyLeft(eTypeOfEnergy i_TypeOfEnergy, out float o_CurrEnergyLeft)
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
            while (!(float.TryParse(leftEnergyInStr, out o_CurrEnergyLeft)) || o_CurrEnergyLeft < 0)
            {
                Console.WriteLine("You must enter only possitive numbers!");
                leftEnergyInStr = Console.ReadLine();
            }
        }
    }
}
