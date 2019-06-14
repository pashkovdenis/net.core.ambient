using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ambient.Interfaces
{
    public interface IAmbientScope : IDisposable
    {
        void AttachToParentScope();
        IAmbientContext Context { get; }
        ValueTask<bool> SaveChangesAsync();
        ValueTask RevertChanges(); 
    }
}
