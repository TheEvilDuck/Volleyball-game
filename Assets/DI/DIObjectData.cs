using System;

namespace DI
{
    internal sealed class DIObjectData<T>: DIObjectData
    {
        private T _instance;
        private Func<T> _fabricMethod;

        public T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = _fabricMethod.Invoke();

                return _instance;
            }
        }

        public DIObjectData(T instance) => _instance = instance;
        public DIObjectData(Func<T> fabricMethod) => _fabricMethod = fabricMethod;
    }

    internal abstract class DIObjectData
    {
        public T Get<T>()
        {
            var casted = (DIObjectData<T>) this;
            return casted.Instance;
        }
    }
}
