using System;
using System.Collections.Generic;
using System.Text;

namespace crud
{

    public class activity
    {
        public int aktivitet { get; set; }
        public string type { get; set; }

        public string hotel_nr { get; set; }

        public override string ToString()
        {
            return $"ID: {aktivitet}, Type: {type}, Hotel_nr: {hotel_nr}";
        }
      
      
    }
}