using Ambient.Interfaces;
using Ambient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambient.Implementation
{
    public sealed class AmbientScopeFactory  : IAmbientScopeFactory 
    {
        private readonly IAmbientContextFactory  ContextFactory;  
        public AmbientScopeFactory(IAmbientContextFactory  contextFactory)
        {
            ContextFactory = contextFactory;
        } 
        public IAmbientScope  GetScope(ScopeMode mode)
        {
          var scope =  new AmbientScope ( ContextFactory.CreateContext() , mode);
            ScopeContainer.SetCurrent(scope, scope.instanceIdentifier);
            return scope; 
        }  
    }
}
