using System;
using UnityEngine;

public interface IUpdatable
{
    void Tick();
}

public class UpdateManager : MonoBehaviour
{
    private IUpdatable[] _updatables;
    private int _current;

    private void Start()
    {
        _current = 0;
    }
    
    private void Update()
    {
        if (_updatables == null) return;
        foreach (var updatable in _updatables)
        {
            if (updatable != null)
            {
                updatable.Tick();
            }
        }
    }

    public void Initialize(int size)
    {
        _updatables = new IUpdatable[size];
    }

    private void Resize()
    {
        if (_updatables == null) return;
        Array.Resize(ref _updatables, _updatables.Length * 2);
    }

    public void Add(IUpdatable element)
    {
        _updatables[_current++] = element;
        if (_current > _updatables.Length)
        {
            Resize();
        }
    }

    public void RemoveElement(IUpdatable element)
    {
        for (int i = 0; i < _updatables.Length; i++)
        {
            if (_updatables[i] == element)
            {
                _updatables[i] = null;
            }
        }
    }
}