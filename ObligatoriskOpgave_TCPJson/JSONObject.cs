using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave_TCPJson
{
    public class JSONObject
    {
        public string Method {  get; set; }
        public int[] nums;
        public JSONObject() { 
            nums = new int[2];
        }
    }
}
