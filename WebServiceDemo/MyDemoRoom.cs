namespace WebServiceDemo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DemoRoom
    {
        public override string ToString()
        {
            return String.Format("Hotel_No: {0} , Room_No: {1}, Types: {2}, Price: {3} ", Hotel_No, Room_No, Types, Price);
        }

        public DemoRoom NoDeepNoParentCopy()
        {
            DemoRoom other = (DemoRoom)this.MemberwiseClone();
            other.DemoHotel = null;
            return other;
        }
    }
}