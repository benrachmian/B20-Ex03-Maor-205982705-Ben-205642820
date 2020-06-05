using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageSystem
    {
        private Dictionary<string, VehiclesInGarage> m_VehiclesInGarage;

        public GarageSystem()
        {
            m_VehiclesInGarage = new Dictionary<string, VehiclesInGarage>();
        }

        // public properties
        public Dictionary<string, VehiclesInGarage> VehiclesInTheGarage
        {
            get
            {
                return m_VehiclesInGarage;
            }
        }

        public VehiclesInGarage GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            VehiclesInGarage foundVehicle;

            if (!m_VehiclesInGarage.TryGetValue(i_LicenseNumber, out foundVehicle))
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }

            return foundVehicle;
        }

        public void AddNewVehicleToGarage(VehiclesInGarage i_NewVehicleToGarage) 
        {
            VehiclesInGarage existVehicleInGarage;

            if (m_VehiclesInGarage.TryGetValue(i_NewVehicleToGarage.VehicleInfo.LicenseNumber, out existVehicleInGarage))
            {
                existVehicleInGarage.VehicleStatus = eVehicleStatuses.InRepair;
                throw new Exception("The vehicle is already in the garage!");
            }
            else
            {
                m_VehiclesInGarage.Add(i_NewVehicleToGarage.VehicleInfo.LicenseNumber, i_NewVehicleToGarage);
            }
        }

        public void ChangeVehicleStatus(string i_VehicleLicenseNumber, eVehicleStatuses i_UpdatedStatus)
        {
            VehiclesInGarage VehicleToUpdate;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out VehicleToUpdate))
            {
                if (VehicleToUpdate.VehicleStatus == i_UpdatedStatus)
                {
                    throw new ArgumentException("The vehicle is already in that status!");
                }
                else
                {
                    VehicleToUpdate.VehicleStatus = i_UpdatedStatus;
                }
            }
            else
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }
        }

        public void InflateTiresToMax(string i_VehicleLicenseNumber)
        {
            VehiclesInGarage VehicleToUpdate;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out VehicleToUpdate))
            {
                foreach (Tire tire in VehicleToUpdate.VehicleInfo.Tires)
                {
                    if(tire.CurrentPSI == tire.MaxPSI)
                    {
                        throw new ArgumentException("The tires PSI are already maximum!");
                    }
                    else
                    {
                        tire.CurrentPSI = tire.MaxPSI;
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }
        }

        public void ProvideSourceEnergyToVehicle(string i_VehicleLicenseNumber, float i_FuelToAdd, eFuelType i_FuelType)
        {
            VehiclesInGarage vehicleToUpdate;
            FuelSystem sourceEnergyTypeSystem;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out vehicleToUpdate))
            {
                if(i_FuelType != null)
                {
                        sourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as FuelSystem;
                        if (sourceEnergyTypeSystem != null)
                        {
                             if (sourceEnergyTypeSystem.FuelType == i_FuelType)
                             {
                        //vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem.
                                    sourceEnergyTypeSystem.ProvideSourceEnergy(i_FuelToAdd, i_FuelType);
                                    vehicleToUpdate.VehicleInfo.UpdateEnergyLeftInPrecents();
                       // vehicleToUpdate.VehicleInfo.EnergyLeftInPrecents = sourceEnergyTypeSystem.CurrEnergy / sourceEnergyTypeSystem.MaxEnergyPossible;
                             }
                             else
                             {
                                   throw new ArgumentException(
                                   string.Format("You tried to refuel with different type fuel of that vehicle!{0}The vehicle type fuel is: {1}",
                                   Environment.NewLine,
                                   sourceEnergyTypeSystem.FuelType));  

                             }
                        }
                        else
                        {
                            throw new ArgumentException("You tried to refuel a fuel vehicle with electricity!");
                        }
                }
                else
                {
                        sourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as BatterySystem;
                        if (sourceEnergyTypeSystem != null)
                        {
                            sourceEnergyTypeSystem.ProvideSourceEnergy(i_HoursToAdd);
                            vehicleToUpdate.VehicleInfo.EnergyLeftInPrecents = sourceEnergyTypeSystem.CurrEnergy / sourceEnergyTypeSystem.MaxEnergyPossible;
                        }
                         else
                         {
                             throw new ArgumentException("You tried to charge an elctric vehicle with fuel!");
                         }
                }
            }
            else
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }
        }

        public void ProvideSourceEnergyToVehicle(string i_VehicleLicenseNumber, float i_HoursToAdd)
        {
            VehiclesInGarage VehicleToUpdate;
            FuelSystem sourceEnergyTypeSystem;


            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out VehicleToUpdate))
            {
                sourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as BatterySystem;
                if (sourceEnergyTypeSystem != null)
                {
                    sourceEnergyTypeSystem.ProvideSourceEnergy(i_HoursToAdd);
                    vehicleToUpdate.VehicleInfo.EnergyLeftInPrecents = sourceEnergyTypeSystem.CurrEnergy / sourceEnergyTypeSystem.MaxEnergyPossible;
                }
                else
                {
                    throw new ArgumentException("You tried to charge an elctric vehicle with fuel!");
                }
            }
            else
            {
                throw new KeyNotFoundException("There is no such vehicle in the system!");
            }
        }

        public void FindEnergySystemType(EnergySourceSystem i_VehicleToCheckEnergySourseSystem, out FuelSystem o_VehicleFuelSystem, out BatterySystem o_VehicleBatterySystem)
        {
            o_VehicleFuelSystem = i_VehicleToCheckEnergySourseSystem as FuelSystem;
            if (o_VehicleFuelSystem == null)
            {
                o_VehicleBatterySystem = i_VehicleToCheckEnergySourseSystem as BatterySystem;
            }
            else
            {
                o_VehicleBatterySystem = null;
            }
        }

        public void FindVehicleType(Vehicle i_VehicleToCheck, out Car o_CarVehicle, out Motorcycle o_MotorcycleVehicle, out Truck o_TruckVehicle)
        {
            o_CarVehicle = i_VehicleToCheck as Car;
            if (o_CarVehicle == null)
            {
                o_MotorcycleVehicle = i_VehicleToCheck as Motorcycle;
                if (o_MotorcycleVehicle == null)
                {
                    o_TruckVehicle = i_VehicleToCheck as Truck;
                }
                else
                {
                    o_TruckVehicle = null;
                }
            }
            else
            {
                o_MotorcycleVehicle = null;
                o_TruckVehicle = null;
            }
        }

        //public float GetMaxBatteryAmount(VehiclesInGarage i_vehicle) // maybe not need at all
        //{
        //    BatterySystem batterySystem = i_vehicle.VehicleInfo.VehicleEnergySourceSystem as BatterySystem;
        //    if (batterySystem == null)
        //    {
        //        // throw exception
        //    }

        //    return batterySystem.MaxBatteryTime;
        //}


    }
}
