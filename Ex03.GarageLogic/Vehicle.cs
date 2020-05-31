using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract public class Vehicle
    {
        public const int k_MinLicenseNumber = 6;
        public const int k_MaxLicenseNumber = 8;
        public const int k_MinModelCharacters = 3;
        public const int k_MaxModelCharacters = 10;
        protected readonly string m_Model;
        protected readonly string m_LicenseNumber;
        protected float m_EnergyLeftInPrecents;
        protected Tire[] m_Tires;
        protected EnergySourceSystem m_EnergySourceSystem;

        public Vehicle(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem)
        {
            m_Model = i_Model;
            m_LicenseNumber = i_LicenseNumber;
            m_Tires = i_Tires;
            m_EnergySourceSystem = i_EnergySourceSystem;
            m_EnergyLeftInPrecents = m_EnergySourceSystem.GetEnergyLeftInPrecents();
        }

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

        public EnergySourceSystem VehicleEnergySourceSystem
        {
            get
            {
                return m_EnergySourceSystem;
            }
        }

        public abstract string GetModelName();
    }
}
