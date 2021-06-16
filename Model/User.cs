using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipliqueV2.API.Model
{
    public class User
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public UserType Type { get; set; }
    }

    public enum  UserType
    {
        Customer = 1, 
        Manager = 2
    }

}
