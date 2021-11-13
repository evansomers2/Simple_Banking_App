using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoginDAL
{
    public class LoginEntity
    {
        public int id { get; set; }
        [Timestamp]
        public byte[] timer { get; set; }
    }
}
