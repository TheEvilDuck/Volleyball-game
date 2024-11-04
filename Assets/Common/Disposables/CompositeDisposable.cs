using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Disposables
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Register(IDisposable disposable)
        {
            if (disposable == this)
            {
                Debug.LogError("Composite disposable must not contain itself, operation will be ignored");
                return;
            }

            if (_disposables.Contains(disposable))
            {
                Debug.LogWarning($"Composite disposable already contains {disposable}, operation will be ignored");
                return;
            }

            _disposables.Add(disposable);
        }

        public void Remove(IDisposable disposable)
        {
            _disposables.Remove(disposable);
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable?.Dispose();

            _disposables.Clear();
        }
    }
}
