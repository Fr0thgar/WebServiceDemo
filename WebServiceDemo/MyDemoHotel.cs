namespace WebServiceDemo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DemoHotel
    {
        public override string ToString()
        {
            return String.Format("Hotel_No: {0} , Name: {1}, Address: {2}", Hotel_No, Name, Address);
        }
    }
}