
namespace Metasense.Infrastructure
{
    public static class Enums
    {
        /// <summary>
        /// Different types of functions
        /// </summary>
        public enum FunctionType
        {
            /// <summary>
            /// A light function will always be executed
            /// </summary>
            Light = 1,

            /// <summary>
            /// A heavy function will only be executed outside the function wizard
            /// </summary>
            Heavy = 2,

            /// <summary>
            /// A result of a sticky function will be cached. The cached result is returned. The only way to force recalc is to open the function wizard
            /// </summary>
            Sticky = 4
        }
    }
}
