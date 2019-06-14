using Ambient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ambient.Example
{

    internal class SiFactory : IAmbientContextFactory
    {
        public IAmbientContext CreateContext()
        {
            return new SimpleContext();
        }
    }
    internal class SimpleContext : IAmbientContext
    { 

        public SimpleContext()
        {
            Console.WriteLine("new Context was created");

        }
         
        public void Dispose()
        {
            Console.WriteLine("Context Was Disposed");
        }

        public ValueTask RevertChanges()
        {
            Console.WriteLine("Revert Changes");
            return new ValueTask();
        }

        public ValueTask<bool> SaveChangesAsync()
        {
            Console.WriteLine("Saved Changes");
            return new ValueTask<bool>(true);
        }
    }
}
