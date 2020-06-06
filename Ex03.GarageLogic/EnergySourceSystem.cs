using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eTypeOfEnergy
    {
        Fuel,
        Electic
    }

    public abstract class EnergySourceSystem
    {
        public abstract void ProvideSourceEnergy(float i_FuelToAdd, eFuelType i_FuelType);

        public abstract void ProvideSourceEnergy(float i_HoursToAdd);

        public abstract float GetEnergyLeftInPrecents();

        public abstract float MaxEnergyPossible
        {
            get;
        }

        public abstract float CurrEnergy
        {
            get;
            set;
        }

        public abstract string EnergyType
        {
            get;
        }

        public abstract override string ToString();
    }
}
