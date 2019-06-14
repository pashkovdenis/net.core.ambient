using Ambient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambient.Interfaces
{
    public interface IAmbientScopeFactory
    {
        IAmbientScope GetScope(ScopeMode mode);
    }
}
