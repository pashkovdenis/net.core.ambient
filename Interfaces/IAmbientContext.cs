using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ambient.Interfaces
{
    public interface IAmbientContext: IDisposable { 
        ValueTask<bool> SaveChangesAsync();
        ValueTask RevertChanges();
    }
}
