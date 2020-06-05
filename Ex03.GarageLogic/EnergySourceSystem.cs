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
    abstract public class EnergySourceSystem
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
        public abstract override string ToString();

        //public abstract Dictionary<int, string> GetParams();

    }
}
