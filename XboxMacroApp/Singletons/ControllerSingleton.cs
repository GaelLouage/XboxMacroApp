using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxMacroApp.Singletons
{

    public sealed class ControllerSingleton
    {
        private static readonly object _lock = new object();
        private static ControllerSingleton instance = null;
        public bool ProgramRunnerTaskIsRunning { get; set; } = false;
        public bool ControllerTestTaskIsRunning { get; set; } = false;

        public Controller Controller { get; private set; }
        ControllerSingleton()
        {
            Controller = new Controller(UserIndex.One);
            
        }
        public static ControllerSingleton Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ControllerSingleton();
                    }
                    return instance;
                }
            }
        }
    }
}
