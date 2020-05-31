using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan98 = 1,
        Octan96,
        Octan95,
        Diesel
    }

    public class FuelSystem : EnergySourceSystem
    {
        public const int k_NumOfFuelTypes = 4;
        private readonly eFuelType m_FuelType;
        private float m_CurrFuelInLiters;
        private float m_MaxFuelInLiters;

        public FuelSystem(eFuelType i_FuelType, float i_CurrFuelInLiters, float i_MaxFuelInLiters)
        {
            m_FuelType = i_FuelType;
            m_CurrFuelInLiters = i_CurrFuelInLiters;
            m_MaxFuelInLiters = i_MaxFuelInLiters;
        }

        // public properties
        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public override void ProvideSourceEnergy(float i_HoursToAdd)
        {
            throw new NotImplementedException();
        }

        public override float GetMaxEnergyPossible()
        {
            return m_MaxFuelInLiters;
        }

        public override float GetEnergyLeftInPrecents()
        {
            return (m_CurrFuelInLiters / m_MaxFuelInLiters) * 100;
        }

        public override void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType == m_FuelType)
            {
                if (m_MaxFuelInLiters - m_CurrFuelInLiters <= i_FuelToAdd)
                {
                    m_CurrFuelInLiters += i_FuelToAdd;
                }
                else
                {
                    //THRWO EXPECTION TOO MUCH FUEL
                }
            }
            else
            {
                //THROW EXEPTION DIFFERENT TYPE
            }
        }
    }
}
