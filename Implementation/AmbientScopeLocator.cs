using Ambient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambient.Implementation
{
    public sealed class AmbientScopeLocator : IScopeLocator
    {
        public IAmbientScope GetCurrentScope()
        {
            return ScopeContainer.GetCurrentScope();
        }
    }
}
