﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Ordering
{
    public enum OrderStatus
    {
        Confirmed,
        Assembling,
        Delivering,
        Completed,
        Canceled
    }
}
