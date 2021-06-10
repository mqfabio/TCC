﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Interfaces
{
    public interface IUnidadeEnsino
    {

        Task<IEnumerable<UnidadeEnsino>> BuscarTodosAsync();


    }
}
