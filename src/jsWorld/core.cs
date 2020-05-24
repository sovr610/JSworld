using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jsWorld.jsEngine;

namespace jsWorld
{
    public class core
    {
        public static void commandLine()
        {
            while (true)
            {
                try
                {

                    Console.Write("> ");
                    string cmd = Console.ReadLine();
                    jsWrapper js = new jsWrapper();
                    Console.WriteLine(js.executeLine(cmd, null));
                }
                catch (Exception i)
                {
                    Console.WriteLine(i);
                }
            }
        }

        public static void runFile(string[] args)
        {
            try
            {
                string file = null;
                if (!args[0].Contains('-'))
                {
                    file = args[0];
                }

                string code = File.ReadAllText(file);
                jsWrapper js = new jsWrapper();
                js.executeLine(code, null, true, null);
            }
            catch (Exception i)
            {
                Console.WriteLine(i);
            }
        }
    }
}
