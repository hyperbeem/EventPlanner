using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace EPlib.Util
{
    public static class AsmInfo
    {
        /// <summary>
        /// Gets all classes in namespace
        /// Angreas Grech
        /// https://stackoverflow.com/questions/1136565/is-there-a-way-to-go-over-all-types-in-a-namespace-in-a-foreach-loop
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesFromNamespace(string SearchNameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes().Where(type => type.Namespace == SearchNameSpace);
        }
    }
}
