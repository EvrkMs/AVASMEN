using System;

namespace System.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyPathAttribute : Attribute
    {
        public string Path { get; }

        public AssemblyPathAttribute(string path)
        {
            Path = path;
        }
    }
}