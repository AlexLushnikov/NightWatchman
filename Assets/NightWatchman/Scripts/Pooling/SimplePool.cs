using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx.Toolkit;
using UnityEngine;
using Object = UnityEngine.Object;

public class SimplePool<T> : AsyncObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;

    public SimplePool(T prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    protected override IObservable<T> CreateInstanceAsync()
    {
        return InstantiateAsync().ToObservable();
    }
    
    private async UniTask<T> InstantiateAsync()
    {
        var handle = Object.InstantiateAsync(_prefab, _parent);

        await handle.ToUniTask();

        if (handle.Result == null)
        {
            throw new UnityException("Instantiation failed");
        }

        return handle.Result.First();
    }
}