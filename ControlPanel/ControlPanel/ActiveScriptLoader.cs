using System;
using System.IO;
using System.Reflection;

namespace ControlPanel
{
    public class ActiveScriptLoader : MarshalByRefObject
    {
        private Assembly mAssembly;
        
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void LoadAssembly(String path)
        {
            try
            {
                mAssembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
            }
            catch(FileNotFoundException)
            {

            }
            catch(BadImageFormatException)
            {

            }
        }

        public object ExecuteStaticMethod(String typeName, String methodName, params object[] parameters)
        {
            if(null == mAssembly)
            {
                return null;
            }

            Type type = mAssembly.GetType(typeName);

            if(null == type)
            {
                return null;
            }

            MethodInfo method = type.GetMethod(methodName);

            if(null == method)
            {
                return null;
            }

            return method.Invoke(null, parameters);
        }
    }
}
