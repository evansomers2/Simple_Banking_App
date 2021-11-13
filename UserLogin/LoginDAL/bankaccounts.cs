using System;
using System.Collections.Generic;

namespace LoginDAL
{
    public partial class bankaccounts : LoginEntity
    {
        public int customerid { get; set; }
        public decimal balance { get; set; }

        public virtual users customer { get; set; }
    }
}
