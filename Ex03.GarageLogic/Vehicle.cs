using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehiclesParameters
    {
        Model,
        LicenseNumber,
        EnergyLeftInPrecents,
        Tires,
        EnergySourceSystem,
        CarColor,
        NumOfDoors,
        IsCarryingDangerousMaterials,
        TrunkVolume,
        LicenseType,
        EngineVolume,
        ManufacturerName,
        CurrentPSI,
        MaxValidPSI,
        FuelType,
        CurrFuelInLiters,
        MaxFuelInLiters,
        BatteryTimeRemaining,
        MaxBatteryTime
    }
    abstract public class Vehicle
    {
        public const int k_MinLicenseNumber = 6;
        public const int k_MaxLicenseNumber = 8;
        public const int k_MinModelCharacters = 3;
        public const int k_MaxModelCharacters = 10;
        protected readonly string m_LicenseNumber;
        protected string m_Model;
        protected float m_EnergyLeftInPrecents;
        protected Tire[] m_Tires;
        protected EnergySourceSystem m_EnergySourceSystem;

        public Vehicle(string i_LicenseNumber, EnergySourceSystem i_EnergySourceSystem, Tire[] i_Tires)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_Tires = i_Tires;
            m_EnergySourceSystem = i_EnergySourceSystem;
            m_Model = null;
            m_EnergyLeftInPrecents = 0;
        }
        //public Vehicle(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem)
        //{
        //    m_Model = i_Model;
        //    m_LicenseNumber = i_LicenseNumber;
        //    m_Tires = i_Tires;
        //    m_EnergySourceSystem = i_EnergySourceSystem;
        //    m_EnergyLeftInPrecents = m_EnergySourceSystem.GetEnergyLeftInPrecents();
        //}

        /// public properties
        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public Tire[] Tires
        {
            get
            {
                return m_Tires;
            }
        }

        public string Model
        {
            get
            {
                return m_Model;
            }
            set
            {
                m_Model = value;
            }
        }

        public EnergySourceSystem VehicleEnergySourceSystem
        {
            get
            {
                return m_EnergySourceSystem;
            }
        }

        public float EnergyLeftInPrecents
        {
            get
            {
                return m_EnergyLeftInPrecents;
            }
            set
            {
                m_EnergyLeftInPrecents = value;
            }
        }

        public virtual void GetParams(List<eVehiclesParameters> i_VehiclesParameters)
        {
            i_VehiclesParameters.Add(eVehiclesParameters.LicenseNumber);
            i_VehiclesParameters.Add(eVehiclesParameters.Model);
            i_VehiclesParameters.Add(eVehiclesParameters.EnergyLeftInPrecents);
            i_VehiclesParameters.Add(eVehiclesParameters.EnergySourceSystem);
            i_VehiclesParameters.Add(eVehiclesParameters.Tires);
            i_VehiclesParameters.Add(eVehiclesParameters.ManufacturerName);
            i_VehiclesParameters.Add(eVehiclesParameters.CurrentPSI);
            i_VehiclesParameters.Add(eVehiclesParameters.MaxValidPSI);
        }

       // public abstract string GetModelName();
    }
}
