using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        public const int k_MinCharsForTireManufacturerName = 4;
        public const int k_MaxCharsForTireManufacturerName = 50;
        private readonly string m_ManufacturerName;
        private float m_CurrentPSI;
        private readonly float m_MaxValidPSI;

        public Tire (string i_ManufacturerName, float i_CurrentPSI, float i_MaxValidPSI) // c'tor
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentPSI = i_CurrentPSI;
            m_MaxValidPSI = i_MaxValidPSI;
        }

        public void InflateTire(float i_AmountOfPsiToAdd)
        {
            if(m_MaxValidPSI - m_CurrentPSI < i_AmountOfPsiToAdd)
            {
                //throw exepction????
            }
            else
            {
                m_CurrentPSI += i_AmountOfPsiToAdd;
            }
        }

        //properties
        public float CurrentPSI
        {
            get
            {
                return m_CurrentPSI;
            }
            set
            {
                m_CurrentPSI = value;
            }
        }

        public float MaxPSI
        {
            get
            {
                return m_MaxValidPSI;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
        }
    }
}
