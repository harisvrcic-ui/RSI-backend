using eParking.Helper.BaseClasses;
using System;

namespace eParking.Data.Models
{
    public class Colors : BaseEntity
    {  
       public string Name { get; set; } = string.Empty;
       public string HexCode { get; set; } = string.Empty;
        
    }
}
