﻿using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IValidatorPodataka
    {
        bool Validator(List<Potrosnja> list);
    }
}
