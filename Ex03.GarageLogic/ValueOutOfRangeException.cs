using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eOutOfRangeTypes
    {
        StringLength,
        Number
    }

    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;
        private string m_Msg;

        public ValueOutOfRangeException
            (float i_MaxValue,
            float i_MinValue,
            eOutOfRangeTypes iOutOfRangeType)
        {
            switch (iOutOfRangeType)
            {
                case eOutOfRangeTypes.Number:
                    {
                        m_Msg = string.Format("You must enter a number between {0} - {1} , Please try again!", i_MinValue, i_MaxValue);
                        break;
                    }
                case eOutOfRangeTypes.StringLength:
                    {
                        m_Msg = string.Format("You must minimum {0} chars and maximum {1} chars. Please try again!", i_MinValue, i_MaxValue);
                        break;
                    }
            }
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
       
        //public properties
        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }
        public override string Message
        {
            get
            {
                return m_Msg;
            }
        }
    }
}
