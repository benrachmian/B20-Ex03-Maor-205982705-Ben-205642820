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
        public void AddNewVehicleToGarage(VehiclesInGarage i_NewVehicleToGarage) // maybe to do exeption if the vehicle is already in garage.
        {
            VehiclesInGarage existVehicleInGarage;

            if (m_VehiclesInGarage.TryGetValue(i_NewVehicleToGarage.VehicleInfo.LicenseNumber, out existVehicleInGarage))
            {
                existVehicleInGarage.VehicleStatus = eVehicleStatuses.InRepair;
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
                    //THROW EXPETION SAME STATUS 
                    //MAYBE IT DOESNT MATTER. UPDATE ONLY IF DIFFERENT
                }
                else
                {
                    VehicleToUpdate.VehicleStatus = i_UpdatedStatus;
                }
            }
            else
            {
                //THROW EXPECTION NOT IN GARAGE
            }
        }

        public void InflateTiresToMax(string i_VehicleLicenseNumber)
        {
            VehiclesInGarage VehicleToUpdate;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out VehicleToUpdate))
            {
                foreach (Tire tire in VehicleToUpdate.VehicleInfo.Tires)
                {
                    tire.CurrentPSI = tire.MaxPSI;
                }
            }
            else
            {
                //THROW EXPECTION NOT IN GARAGE
            }
        }

        public void ProvideSourceEnergyToVehicle(string i_VehicleLicenseNumber, float i_FuelToAdd, eFuelType i_FuelType)
        {
            VehiclesInGarage vehicleToUpdate;
            FuelSystem sourceEnergyTypeSystem;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out vehicleToUpdate))
            {
                sourceEnergyTypeSystem = vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem as FuelSystem;
                if (sourceEnergyTypeSystem != null)
                {
                    if (sourceEnergyTypeSystem.FuelType == i_FuelType)
                    {
                        vehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem.ProvideSourceEnergy(i_FuelToAdd, i_FuelType);
                    }
                    else
                    {
                        // THROW EXPTION IT'S another type fuel   
                    }
                }
                else
                {
                    // THROW EXPTION IT'S bATTERYsYSTEM   
                }
            }
            else
            {
                //THROW EXPECTION NOT IN GARAGE
            }
        }
        public void ProvideSourceEnergyToVehicle(string i_VehicleLicenseNumber, float i_HoursToAdd)
        {
            VehiclesInGarage VehicleToUpdate;

            if (m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out VehicleToUpdate))
            {
                if (VehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem is BatterySystem)
                {
                    VehicleToUpdate.VehicleInfo.VehicleEnergySourceSystem.ProvideSourceEnergy(i_HoursToAdd);
                }
                else
                {
                    // THROW EXPTION IT'S FUEL SYSTEM   
                }
            }
            else
            {
                //THROW EXPECTION NOT IN GARAGE
            }
        }
            
    }
}
