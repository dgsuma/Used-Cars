// used as a parent class for car propeties
// hold name values as pairs in two seperate, parallel lists

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Used_cars.Business
{

    class D_Types
    {
        protected List<int> id = new List<int>();               // list to hold id/index
        protected List<String> val = new List<string>();        // list to hold value /name (ie: Toyota)
       
        public int Get_Id(int index)                            // get method for id : returns id for the given index of list
        {
            return id[index];
        }

        public string Get_Value(int rid)                      // get method for val/name : returns val/name for the given id
        {
            int index = id.IndexOf(rid);
            return val[index];
        }

        public List<string> Get_List()                          // retun all names/values
        {
            return val;             //this list uses in Presentation layer's Add_cars and Car_List
        }

    }
}
