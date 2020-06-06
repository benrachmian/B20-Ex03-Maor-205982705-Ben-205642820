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

        public bool IsIsGarage(string i_LicenseNumber)
        {
            bool inGarage = false;

            foreach(string vehicleLicenseNumber in m_VehiclesInGarage.Keys)
            {
                if(vehicleLicenseNumber == i_LicenseNumber)
                {
                    inGarage = true;
                    break;
                }
            }

            return inGarage;
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

        public void ProvideSourceEnergyToVehicle(string i_VehicleLicenseNumber, float i_AmountToAdd, eFuelType? i_FuelType)
        {
            VehiclesInGarage vehicleToUpdate;
            FuelSystem fuelSourceEnergyTypeSystem;
            BatterySystem batterySourceEnergyTypeSystem;

            if (i_AmountToAdd <= 0)
            {
                throw new ArgumentException("You must provide energy with possitive number!");
            }
            else
            {
                if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out vehicleToUpdate))
                {
                    if (i_FuelType != null)
                    {
                        fuelSourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as FuelSystem;
                        if (fuelSourceEnergyTypeSystem != null)
                        {
                            if (fuelSourceEnergyTypeSystem.FuelType == i_FuelType)
                            {
                                fuelSourceEnergyTypeSystem.ProvideSourceEnergy(i_AmountToAdd, (eFuelType)i_FuelType);
                                vehicleToUpdate.VehicleInfo.UpdateEnergyLeftInPrecents();
                            }
                            else
                            {
                                throw new ArgumentException(
                                string.Format(
    @"You tried to refuel with different type fuel of that vehicle!
The vehicle type fuel is: {0}",
                                fuelSourceEnergyTypeSystem.FuelType));
                            }
                        }
                        else
                        {
                            throw new ArgumentException("You tried to refuel a fuel vehicle with electricity!");
                        }
                    }
                    else
                    {
                        batterySourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as BatterySystem;
                        if (batterySourceEnergyTypeSystem != null)
                        {
                            batterySourceEnergyTypeSystem.ProvideSourceEnergy(i_AmountToAdd);
                            vehicleToUpdate.VehicleInfo.EnergyLeftInPrecents = batterySourceEnergyTypeSystem.CurrEnergy / batterySourceEnergyTypeSystem.MaxEnergyPossible;
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
        }
    }
}
