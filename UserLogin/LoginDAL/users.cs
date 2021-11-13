using System;
using System.Collections.Generic;

namespace LoginDAL
{
    public partial class users : LoginEntity
    {
        public users()
        {
            bankaccounts = new HashSet<bankaccounts>();
        }

        public string username { get; set; }
        public string pass { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime dob { get; set; }
        public string email { get; set; }

        public virtual ICollection<bankaccounts> bankaccounts { get; set; }
    }
}
