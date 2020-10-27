using System;
using System.Collections.Generic;
using System.Text;

namespace TCP_Server
{
    
    class Sensordata
    {
        private double Value;
        private string Unit;
        public Sensordata (double data, string unit )
        {
            Value = data;
        }
        public string returnData()
        {
            return Value + Unit;
        }
    }
}
