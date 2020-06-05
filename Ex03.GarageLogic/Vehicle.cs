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
        EnergySourceSystem,
        ManufacturerName,
        CurrentPSI,
        CurrentEnergyLeft
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

        public Dictionary<int, string> GetVehicleParams()
        {
            Dictionary<int, string> vehicleParams = new Dictionary<int, string>();

            vehicleParams.Add((int)eVehiclesParameters.Model, "vehicle's model:");
            vehicleParams.Add((int)eVehiclesParameters.ManufacturerName, "Manufacturer name:");
            vehicleParams.Add((int)eVehiclesParameters.CurrentPSI, "Current PSI:");
            vehicleParams.Add((int)eVehiclesParameters.CurrentEnergyLeft, "Current Energy Left:");

            return vehicleParams;
        }

        public void SetVehicleParameters(int i_indexInEnum, string i_value)
        {
            switch (i_indexInEnum)
            {

                case (int)eVehiclesParameters.Model:
                    {
                        Validation.CheckStringValid(i_value, k_MinModelCharacters, k_MaxModelCharacters);
                        m_Model = i_value;
                        break;
                    }
                case (int)eVehiclesParameters.ManufacturerName:
                    {
                        Validation.CheckStringValid(i_value, Tire.k_MinCharsForTireManufacturerName, Tire.k_MaxCharsForTireManufacturerName);
                        foreach (Tire tire in Tires)
                        {
                            tire.ManufacturerName = i_value;
                        }
                        break;
                    }
                case (int)eVehiclesParameters.CurrentPSI:
                    {
                        float currPSI = Validation.CheckNumberValidation(i_value, 0, m_Tires[0].MaxPSI);
                        foreach (Tire tire in Tires)
                        {
                            tire.CurrentPSI = currPSI;
                        }
                        break;
                    }
                case (int)eVehiclesParameters.CurrentEnergyLeft:
                    {
                        float currEnergy = Validation.CheckNumberValidation(i_value, 0, m_EnergySourceSystem.MaxEnergyPossible);
                        m_EnergySourceSystem.CurrEnergy = currEnergy; 
                        m_EnergyLeftInPrecents = currEnergy / m_EnergySourceSystem.MaxEnergyPossible; /// while printed it's multiplied by 100 
                        break;
                    }
            }
        }

        public abstract Dictionary<int, string> GetParams();
        public abstract void SetSpecificTypeParams(int i_indexInEnum, string i_value);
        public override string ToString()
        {
            StringBuilder vehicleToString = new StringBuilder();
            vehicleToString.Append(string.Format(
@"License number: {0}
Model name: {1}
Tires manufacturer name: {2}
Tires PSI: {3}
",
                m_LicenseNumber,
                m_Model,
                m_Tires[0].ManufacturerName,
                m_Tires[0].CurrentPSI));
            vehicleToString.Append(m_EnergySourceSystem.ToString());
            vehicleToString.Append(string.Format(" ({0:p}) {1}", m_EnergyLeftInPrecents, Environment.NewLine));

            return vehicleToString.ToString();
        }
    }
}
//public virtual void GetParams(List<eVehiclesParameters> i_VehiclesParameters)
//{
//    i_VehiclesParameters.Add(eVehiclesParameters.LicenseNumber);
//    i_VehiclesParameters.Add(eVehiclesParameters.Model);
//    i_VehiclesParameters.Add(eVehiclesParameters.EnergyLeftInPrecents);
//    i_VehiclesParameters.Add(eVehiclesParameters.EnergySourceSystem);
//    i_VehiclesParameters.Add(eVehiclesParameters.Tires);
//    i_VehiclesParameters.Add(eVehiclesParameters.ManufacturerName);
//    i_VehiclesParameters.Add(eVehiclesParameters.CurrentPSI);
//    i_VehiclesParameters.Add(eVehiclesParameters.MaxValidPSI);
//}