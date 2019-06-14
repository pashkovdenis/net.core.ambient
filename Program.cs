using Ambient.Example;
using Ambient.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var locator = new AmbientScopeLocator();
            var factory = new AmbientScopeFactory(new SiFactory());
             
           await Task.Factory.StartNew(async () => {
                Console.WriteLine("Starting   Task 1 ");
                using var scope = factory.GetScope(Models.ScopeMode.WRITE);
                await DoJob();
                    Console.WriteLine("JobCompleted");
                    await DoJob(); 
                    await scope.SaveChangesAsync(); 
                Console.WriteLine(scope);

            } );


           await Task.Factory.StartNew(async () => {

                Console.WriteLine("Starting   Task 2 ");
                using var scope = factory.GetScope(Models.ScopeMode.WRITE);
               
                    await DoJob(); 
                    await DoJob(); 
                    await scope.SaveChangesAsync();
                    Console.WriteLine(scope);
                 

            } );



            Console.ReadLine(); 
        } 



        private static  async Task DoJob()
        {
            var scope = ScopeContainer.GetCurrentScope();
            Console.WriteLine("Current Scope job " + scope);
            await Task.Delay(1000); 
        }


    }
}
