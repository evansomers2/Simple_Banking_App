using System;
using System.Collections.Generic;
using System.Text;

namespace LoginDAL
{
    public enum UpdateStatus
    {
        Ok = 1,
        Failed = -1,
        Stale = -2
    };
}
