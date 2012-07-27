using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ControlPanel
{
    class ScriptLoader : MarshalByRefObject
    {
        private Assembly mAssembly;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void LoadAssembly(string path)
        {
            mAssembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
        }

        public object ExecuteStaticMethod(string typeName, string methodName, params object[] parameters)
        {
            Type type = mAssembly.GetType(typeName);
            // TODO: this won't work if there are overloads available
            MethodInfo method = type.GetMethod(methodName);

            return method.Invoke(null, parameters);
        }
    }
}
