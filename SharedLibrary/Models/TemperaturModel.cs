using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class TemperaturModel
    {
        public class Main
        {
            public double Temp { get; set; }
            public double Humidity { get; set; }
        }

        public class Root
        {
            public string Name { get; set; }
            public Main Main { get; set; } 

        }

    }

    
    
}
