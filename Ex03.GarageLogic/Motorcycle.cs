using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenseTypes
    {
        A = 1,
        A1,
        AA,
        B
    }

    public class Motorcycle : Vehicle
    {
        public const int k_NumOfLicenseTypesOptions = 4;
        public const int k_NumOfWheelsInMotorcycle = 2;
        public const int k_MaxPsiInElectricMotorcycle = 30;
        public const int k_MaxPsiInRegularMotorcycle = 30;
        public const float k_MaxLitersInFuelMotorcycle = 7f;
        public const float k_MaxHoursInElectricMotorcycle = 1.2f;
        public const int k_MinEngineVolume = 50;
        public const int k_MaxEngineVolume = 1200;
        private eLicenseTypes m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnerySourceSystem, eLicenseTypes i_LicenseType, int i_EngineVolume)
            : base(i_Model,i_LicenseNumber,i_Tires,i_EnerySourceSystem)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        public override string GetModelName()
        {
            return m_Model;
        }
    }
}
