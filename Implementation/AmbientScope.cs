using Ambient.Interfaces;
using Ambient.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ambient.Implementation
{
    internal static class ScopeContainer
    {
        private static readonly ConditionalWeakTable<InstanceIdentifier, IAmbientScope> Scopes 
            = new ConditionalWeakTable<InstanceIdentifier, IAmbientScope>();

        private static readonly AsyncLocal<InstanceIdentifier> CurrentIdentifier = new AsyncLocal<InstanceIdentifier>(); 
        public static void SetCurrent(IAmbientScope scope, InstanceIdentifier key)
        { 
            CurrentIdentifier.Value = key;
            if (!Scopes.TryGetValue(key, out _))
            {
                Scopes.Add(key, scope);
            }
        } 
        public static IAmbientScope  GetCurrentScope ()
        {
            if (Scopes.TryGetValue(CurrentIdentifier.Value,  out IAmbientScope result ))
            {
                return result as IAmbientScope ;
            }
            return default;
        }

    }
    internal class InstanceIdentifier : MarshalByRefObject { }

    public sealed class AmbientScope  : IAmbientScope 
    {
        public AmbientScope(IAmbientContext context, ScopeMode mode)
        {
            Context = context;
            Mode = mode;
        }
        private ScopeMode Mode; 

        public IAmbientContext Context { get; private set; }

        private IAmbientScope  Parent;

        private bool IsDisposed = false;

        private bool IsCompleted = false;

        public readonly string Identifier = $"Sope_{Guid.NewGuid()}";

        internal InstanceIdentifier instanceIdentifier = new InstanceIdentifier();
         
        public void AttachToParentScope()
        { 
            Parent = ScopeContainer.GetCurrentScope ();
            ScopeContainer.SetCurrent(this, instanceIdentifier);
            Context = Parent.Context; 
        } 
        public async ValueTask RevertChanges()
        {
            await Context.RevertChanges();
        }

        public async ValueTask<bool> SaveChangesAsync()
        {
            if (Mode == ScopeMode.READ)
            {
                throw new InvalidOperationException("Scoped is read only");
            }

            if (!IsCompleted )
            { 
                IsCompleted = true;
                return await Context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Scope is completed. create new scope");
            }
        } 
        public override string ToString() => Identifier;
        public void Dispose()
        {
            if (!IsDisposed)
            {
                Context.Dispose();
                IsDisposed = true;
                if (Parent !=  null)
                {
                    ScopeContainer.SetCurrent(Parent, instanceIdentifier);
                }

            }else
            {
                throw new InvalidOperationException("Sсope is Already Disposed.");
            }
        }

     
    }
}
