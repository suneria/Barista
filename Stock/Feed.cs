﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Stock
{
    public interface Feed<T>
    {
        void request(RequestForm requestForm, ITargetBlock<T> target);
    }
}
