using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;

namespace jsWorld.jsEngine
{
    class jsWrapper
    {
        private IJsEngine core;
        private readonly IJsEngineSwitcher engineSwitch = JsEngineSwitcher.Current;

        public jsWrapper()
        {
            setup();
        }

        public void setup()
        {
            engineSwitch.EngineFactories.Add(new V8JsEngineFactory());
            core = engineSwitch.CreateEngine(V8JsEngine.EngineName);
            addClasses();
        }

        public object executeLine(string code, string message, bool isFile = false, string[] libs = null)
        {

            string libCode = null;
            StringBuilder sb = new StringBuilder();
            if (libs != null)
            {
                foreach (var lib in libs)
                {
                    string data = File.ReadAllText(lib);
                    sb.AppendLine(data);
                }

                libCode = sb.ToString();
            }

            if (!isFile)
            {

                sb.AppendLine("function main(info) {");
                sb.AppendLine("returnInfo = ''");
                sb.AppendLine(code);
                sb.AppendLine("return returnInfo");
                sb.AppendLine("}");
                code = sb.ToString();
            }

            code = libCode + code;
            core.Evaluate(code);
            var obj = core.CallFunction("main", message);
            return obj;

            
        }

        public bool addClasses()
        {
            try
            {
                //core.EmbedHostType("", typeof());
                core.EmbedHostType("log", typeof(Console));
                core.EmbedHostType("file", typeof(File));
                core.EmbedHostType("directory", typeof(Directory));
                core.EmbedHostType("webClient", typeof(WebClient));
                
                return true;
            }
            catch (Exception i)
            {
                Console.WriteLine(i);
                return false;
            }
        }
    }
}
