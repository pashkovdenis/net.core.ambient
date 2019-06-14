using System;
using System.Collections.Generic;
using System.Text;

namespace Ambient.Interfaces
{
    public interface IScopeLocator
    {
        IAmbientScope GetCurrentScope();
    }
}
