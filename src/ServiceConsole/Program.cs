using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ServiceConsole
{
    public class Program : ServiceBase
    {
        private readonly EventLog _log =
            new EventLog("Application") { Source = "Application" };

        public static void Main(string[] args)
        {
            try
            {
                if (args.Contains("--windows-service"))
                {
                    Run(new Program());
                    Debug.WriteLine("Exiting");
                    return;
                }

                var program = new Program();
                program.OnStart(null);

                // Will exit on keypress
                Console.ReadLine();
                
                program.OnStop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        protected override void OnStart(string[] args)
        {
            _log.WriteEntry("UP service started");

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _log.WriteEntry("UP service stopped");

            base.OnStop();
        }
    }
}
