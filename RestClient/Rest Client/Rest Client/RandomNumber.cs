using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rest_Client
{
    class RandomNumber
    {
        public RandomNumber(Task<string> json)
        {
            JObject jObject = JObject.Parse(json.Result.ToString());    
            HigherNumber = Convert.ToInt32(jObject["Higherbound"].ToString());
            LowerNumber = Convert.ToInt32(jObject["Lowerbound"].ToString());
            Random = Convert.ToInt32(jObject["RandomNumber"].ToString());
        }
        
        public int LowerNumber { get; set; }
 
        public int HigherNumber { get; set; }
        
        public int Random{ get; set; }
    }
}
