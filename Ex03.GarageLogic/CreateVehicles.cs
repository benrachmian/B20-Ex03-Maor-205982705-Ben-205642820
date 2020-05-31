using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eTypeOfVehicle
    {
        RegularMotorcycle = 1,
        ElectricMotorcycle,
        RegularCar,
        ElectricCar,
        Truck
    }
    public class CreateVehicles
    {
        public static Vehicle CreateMotorcycle(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnerySourceSystem, eLicenseTypes i_LicenseType, int i_EngineVolume)
        {
            return new Motorcycle(i_Model, i_LicenseNumber, i_Tires, i_EnerySourceSystem, i_LicenseType, i_EngineVolume); 
        }

        public static Vehicle CreateCar (string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem, eCarColors i_CarColor, uint i_NumOfDoors)
        {
            return new Car(i_Model, i_LicenseNumber, i_Tires, i_EnergySourceSystem, i_CarColor, i_NumOfDoors);
        }

        public static Vehicle CreateTruck(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem, bool i_IsCarryingDangerousMaterials, float i_TrunkVolume)
        {
            return new Truck(i_Model, i_LicenseNumber, i_Tires, i_EnergySourceSystem, i_IsCarryingDangerousMaterials, i_TrunkVolume);
        }
    }
}
