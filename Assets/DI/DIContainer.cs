using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public class DIContainer: IDIContainer
    {
        private readonly Dictionary<(Type type, string tag), DIObjectData> _entries;
        private readonly IDIContainer _parentContainer;
        private readonly HashSet<(Type type, string tag)> _cyclicCash;

        public DIContainer(IDIContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
            _cyclicCash = new HashSet<(Type type, string tag)>();
            _entries = new Dictionary<(Type type, string tag), DIObjectData>();
        }

        public DIContainerBuilder<T> Register<T>(Func<T> fabricMethod, string tag = "")
        {
            if (ContainsRegistration<T>(tag))
            {
                Debug.LogError($"{typeof(T)} with tag {tag} is already registered!");
                return null;
            }

            (Type type, string tag) tuple = (typeof(T), tag);
            _entries.Add(tuple, new DIObjectData<T>(fabricMethod));
            return new DIContainerBuilder<T>(this, tag);
        }

        public bool ContainsRegistration<T>(string tag = "")
        {
            (Type type, string tag) tuple = (typeof(T), tag);

            if (_entries.ContainsKey(tuple))
                return true;

            if (_parentContainer == null)
                return false;

            return _parentContainer.ContainsRegistration<T>(tag);
        }

        public T Get<T>(string tag = "")
        {
            (Type type, string tag) tuple = (typeof(T), tag);

            if (_cyclicCash.Contains(tuple))
                throw new Exception($"Cyclic dependency accured with type of {tuple} and tag {tag}");

            _cyclicCash.Add(tuple);

            try
            {
                if (_entries.TryGetValue(tuple, out var objectData))
                    return objectData.Get<T>();

                if (_parentContainer != null)
                    return _parentContainer.Get<T>(tag);
            }
            finally
            {
                 _cyclicCash.Remove(tuple);
            }

            throw new Exception($"DI container doesn't know how to create {tuple} and tag {tag}, do you forget to register entity?");
        }

        public class DIContainerBuilder<T>
        {
            private readonly DIContainer _container;
            private readonly string _tag;

            public DIContainerBuilder(DIContainer container, string tag = "")
            {
                _container = container;
                _tag = tag;
            }

            public DIContainerBuilder<T> NonLazy()
            {
                _container.Get<T>(_tag);
                return this;
            }

            public DIContainer Build() => _container;
        }
    }
}
