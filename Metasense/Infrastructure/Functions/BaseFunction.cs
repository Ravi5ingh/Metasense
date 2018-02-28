
namespace Metasense.Infrastructure.Functions
{
    public abstract class BaseFunction<T> : IFunction<T>
    {
        /// <summary>
        /// Th function type
        /// </summary>
        public Enums.FunctionType FunctionType { get; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="functionType"></param>
        protected BaseFunction(Enums.FunctionType functionType)
        {
            FunctionType = functionType;
        }

        /// <inheritdoc />
        public abstract void ResolveInputs();

        /// <inheritdoc />
        public abstract T Calculate();

        /// <inheritdoc />
        public virtual object Render(T resultObject)
        {
            return resultObject;
        }
    }
}
