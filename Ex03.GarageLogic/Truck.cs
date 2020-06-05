using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eTruckParams
    {
        IsCarryingDangerousMaterials,
        TrunkVolume
    }

    public class Truck : Vehicle
    {
        public const int k_NumOfWheelsInTruck = 16;
        public const float k_MaxPsiInTruck = 28;
        public const float k_MaxLitersInFuelTruck = 120;
        public const float k_MaxTrunkVolume = 2000;
        public const float k_MinTrunkVolume = 200;
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

        public override string VehilceType
        {
            get
            {
                return "Truck";
            }
        }

        public override Dictionary<int, string> GetParams()
        {
            Dictionary<int, string> truckParams = new Dictionary<int, string>();

            truckParams.Add((int)eTruckParams.IsCarryingDangerousMaterials, string.Format
                ("Is carrying dangerous materials? {0}1. Yes {0}2. No", Environment.NewLine));
            truckParams.Add((int)eTruckParams.TrunkVolume, "Trunk volume:");

            return truckParams;
        }

        public override void SetSpecificTypeParams(int i_indexInEnum, string i_value)
        {
            switch (i_indexInEnum)
            {
                case (int)eTruckParams.TrunkVolume:
                    {
                        TrunkVolume = Validation.CheckNumberValidation(i_value, k_MinTrunkVolume, k_MaxTrunkVolume);
                        break;
                    }
                case (int)eTruckParams.IsCarryingDangerousMaterials:
                    {
                        int option = Validation.CheckNumberValidation(i_value, 1, 2);

                        if(option == 1)
                        {
                            IsCarryingDangerousMaterials = true;
                        }
                        else
                        {
                            IsCarryingDangerousMaterials = false;
                        }

                        break;
                    }
            }
        }

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

        public override string ToString()
        {
            StringBuilder truckToString = new StringBuilder();
            truckToString.Append(base.ToString());
            truckToString.Append(string.Format(
@"Is carrying dangerous matirials: {0}
Trunk volume: {1}",
                      m_IsCarryingDangerousMaterials ? "Yes" : "No",
                      m_TrunkVolume));

            return truckToString.ToString();
        }
    }
}
