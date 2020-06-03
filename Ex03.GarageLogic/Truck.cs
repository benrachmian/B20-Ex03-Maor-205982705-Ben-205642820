using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public const int k_NumOfWheelsInTruck = 16;
        public const float k_MaxPsiInTruck = 28;
        public const float k_MaxLitersInFuelTruck = 120;
        public const float k_MaxTrunkVolume = 2000;
        private bool m_IsCarryingDangerousMaterials;
        private float m_TrunkVolume;

        public Truck(string i_LicenseNumber, EnergySourceSystem i_EnergySourceSystem, Tire[] i_Tires) 
            : base(i_LicenseNumber, i_EnergySourceSystem, i_Tires)
        {
            m_IsCarryingDangerousMaterials = false;
            m_TrunkVolume = 0;
        }
        //public Truck(string i_Model, string i_LicenseNumber, Tire[] i_Tires, EnergySourceSystem i_EnergySourceSystem, bool i_IsCarryingDangerousMaterials, float i_TrunkVolume)
        //     : base(i_Model, i_LicenseNumber, i_Tires, i_EnergySourceSystem)
        //{
        //    m_IsCarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
        //    m_TrunkVolume = i_TrunkVolume;
        //}

        //public properties
        public bool IsCarryingDangerousMaterials
        {
            get
            {
                return m_IsCarryingDangerousMaterials;
            }
            set
            {
                m_IsCarryingDangerousMaterials = value;
            }
        }

        public float TrunkVolume
        {
            get
            {
                return m_TrunkVolume;
            }
            set
            {
                m_TrunkVolume = value;
            }
        }

        //public override string GetModelName()
        //{
        //    return m_Model;
        //}

        public override void GetParams(List<eVehiclesParameters> i_VehiclesParameters)
        {
            base.GetParams(i_VehiclesParameters);
            i_VehiclesParameters.Add(eVehiclesParameters.IsCarryingDangerousMaterials);
            i_VehiclesParameters.Add(eVehiclesParameters.TrunkVolume);
        }
    }
}
