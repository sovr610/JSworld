using System;
using JavaScriptEngineSwitcher.Core;
using jsWorld.jsEngine;

namespace jsWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                core.runFile(args);
            }
            else
            {
                core.commandLine();
            }
        }
    }
}
