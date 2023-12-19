using System.Reflection;

namespace Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
        private static void Reference()
        {
            var reference = Domain.AssemblyReference.Assembly;
        }
    }
}