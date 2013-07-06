using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace ControlPanel
{
    public static class ActiveScriptCompiler
    {
        public static void CompileScript(FileInfo sourceFile, DirectoryInfo targetDirectory)
        {
            if(null == sourceFile)
            {
                return;
            }

            if(null == targetDirectory)
            {
                return;
            }

            CompileScript(File.ReadAllText(sourceFile.FullName), targetDirectory);
        }

        public static void CompileScript(String source, DirectoryInfo targetDirectory)
        {
            if (null == targetDirectory)
            {
                return;
            }

            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                CompilerParameters compilerParameters = new CompilerParameters();
                compilerParameters.OutputAssembly = Path.Combine(targetDirectory.FullName, "script.dll");
                compilerParameters.GenerateExecutable = false;
                compilerParameters.GenerateInMemory = false;

                compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");

                CompilerResults results = compiler.CompileAssemblyFromSource(compilerParameters, source);
            }
        }
    }
}
