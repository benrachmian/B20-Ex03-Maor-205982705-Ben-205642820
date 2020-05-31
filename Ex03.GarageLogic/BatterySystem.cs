using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class BatterySystem : EnergySourceSystem
    {
        float m_BatteryTimeRemaining;
        readonly float m_MaxBatteryTime;

        public BatterySystem(float i_BatteryTimeRemaining, float i_MaxBatteryTime)
        {
            m_BatteryTimeRemaining = i_BatteryTimeRemaining;
            m_MaxBatteryTime = i_MaxBatteryTime;
        }

        // public properties
        public float MaxBatteryTime
        {
            get
            {
                return m_MaxBatteryTime;
            }
        }


        public override void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType)
        {
            throw new NotImplementedException();
        }

        public override float GetEnergyLeftInPrecents()
        {
            return (m_BatteryTimeRemaining / m_MaxBatteryTime) * 100;
        }

        public override void ProvideSourceEnergy(float i_HoursToAdd)
        {
            if(m_MaxBatteryTime - m_BatteryTimeRemaining <= i_HoursToAdd)
            {
                m_BatteryTimeRemaining += i_HoursToAdd;
            }
            else
            {
                // THROW EXPECTION TOO MUCH HOURS TO ADD throw (m_MaxBatteryTime - m_BatteryTimeRemaining)
            }
        }

        public override float GetMaxEnergyPossible()
        {
            return m_MaxBatteryTime();
        }
    }
}
