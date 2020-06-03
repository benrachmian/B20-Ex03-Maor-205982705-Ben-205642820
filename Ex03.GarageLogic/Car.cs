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
        public const float k_MaxBatteryHoursInElectricCar = 2.1f;

        //public override void getDetails(List<string> i_dataMembersName, List<object> i_DataMembers)
        //{
            
        //}

        private eCarColors? m_CarColor;
        private uint m_NumOfDoors;

        public Car(string i_LicenseNumber, EnergySourceSystem i_EnergySourceSystem, Tire[] i_Tires) 
            : base(i_LicenseNumber, i_EnergySourceSystem, i_Tires)
        {
            m_CarColor = null;
            m_NumOfDoors = 0;
        }
        //public Car(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem, eCarColors i_CarColor, uint i_NumOfDoors)
        //    : base(i_Model,i_LicenseNumber,i_Tires, i_EnergySourceSystem) //c'tor
        //{
        //    m_CarColor = i_CarColor;
        //    m_NumOfDoors = i_NumOfDoors;
        //}

        //public properties
        public eCarColors CarColor
        {
            get
            {
                return (eCarColors)m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public uint NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }
            set
            {
                m_NumOfDoors = value;
            }
        }

        //public override string GetModelName()
        //{
        //    return m_Model;
        //}

        public override void GetParams(List<eVehiclesParameters> i_VehiclesParameters)
        {
            base.GetParams(i_VehiclesParameters);
            i_VehiclesParameters.Add(eVehiclesParameters.CarColor);
            i_VehiclesParameters.Add(eVehiclesParameters.NumOfDoors);
        }
    }
}
