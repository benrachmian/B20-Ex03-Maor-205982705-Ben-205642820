using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eCarColors
    {
        Red = 1,
        White,
        Black,
        Silver
    }

    public class Car : Vehicle
    {
        public const uint k_NumOfColorsOptions = 4;
        public const int k_MinNumOfDoors = 2;
        public const int k_MaxNumOfDoors = 5;
        public const int k_NumOfWheelsInCar = 4;
        public const int k_MaxPsiInFuelCar = 32;
        public const int k_MaxPsiInElectricCar = 32;
        public const float k_MaxLitersInFuelCar = 60;
        public const float k_MaxHoursInElectricCar = 2.1f;

        private eCarColors m_CarCoor;
        private uint m_NumOfDoors;

        public Car(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem, eCarColors i_CarColor, uint i_NumOfDoors)
            : base(i_Model,i_LicenseNumber,i_Tires, i_EnergySourceSystem)
        {
            m_CarCoor = i_CarColor;
            m_NumOfDoors = i_NumOfDoors;
        }

        public override string GetModelName()
        {
            return m_Model;
        }
    }
}
