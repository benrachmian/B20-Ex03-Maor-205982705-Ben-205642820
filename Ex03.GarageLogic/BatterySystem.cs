using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class BatterySystem : EnergySourceSystem
    {
        private float m_BatteryTimeRemaining;
        private readonly float m_MaxBatteryTime;

        public BatterySystem(float i_MaxBatteryTime) // c'tor
        {
            m_MaxBatteryTime = i_MaxBatteryTime;
            m_BatteryTimeRemaining = 0;
        }

        // public properties
        public override float MaxEnergyPossible
        {
            get
            {
                return m_MaxBatteryTime;
            }
        }
        public override float CurrEnergy
        {
            get
            {
                return m_BatteryTimeRemaining;
            }
            set
            {
                m_BatteryTimeRemaining = value;
            }
        }

        public override string EnergyType
        {
            get
            {
                return "Battery System";
            }
        }

        public override void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType)
        {
            throw new ArgumentException("You tried to charge an elctric vehicle with fuel!");
        }

        public override void ProvideSourceEnergy(float i_HoursToAdd)
        {
            if(m_MaxBatteryTime - m_BatteryTimeRemaining >= i_HoursToAdd)
            {
                m_BatteryTimeRemaining += i_HoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaxBatteryTime - m_BatteryTimeRemaining, 0, eOutOfRangeTypes.Number);
            }
        }

        public override float GetEnergyLeftInPrecents()
        {
            return (m_BatteryTimeRemaining / m_MaxBatteryTime) * 100;
        }

        public override string ToString()
        {
            return string.Format("Battery gauge: {0} hours", m_BatteryTimeRemaining);
        }

        //public override float GetMaxEnergyPossible()
        //{
        //    return m_MaxBatteryTime;
        //}

        //public override void GetParams(List<eVehiclesParameters> i_VehiclesParameters)
        //{
        //    i_VehiclesParameters.Add(eVehiclesParameters.BatteryTimeRemaining);
        //    i_VehiclesParameters.Add(eVehiclesParameters.MaxBatteryTime);
        //}
    }
}
