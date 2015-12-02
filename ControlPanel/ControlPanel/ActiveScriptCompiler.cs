using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace ControlPanel
{
    public static class ActiveScriptCompiler
    {
        public static CompilerResults CompileScript(FileInfo sourceFile, DirectoryInfo targetDirectory)
        {
            if(null == sourceFile)
            {
                return null;
            }

            if(null == targetDirectory)
            {
                return null;
            }

            return CompileScript(File.ReadAllText(sourceFile.FullName), targetDirectory);
        }

        public static CompilerResults CompileScript(String source, DirectoryInfo targetDirectory)
        {
            CompilerResults results = null;

            if(null != targetDirectory)
            {
                using (CSharpCodeProvider compiler = new CSharpCodeProvider())
                {
                    CompilerParameters compilerParameters = new CompilerParameters();
                    compilerParameters.OutputAssembly = Path.Combine(targetDirectory.FullName, "script.dll");
                    compilerParameters.GenerateExecutable = false;
                    compilerParameters.GenerateInMemory = false;

                    compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");

                    results = compiler.CompileAssemblyFromSource(compilerParameters, source);
                }
            }

            return results;
        }
    }
}
