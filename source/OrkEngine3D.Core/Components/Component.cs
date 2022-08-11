/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Newtonsoft.Json;
using System.Diagnostics;

namespace OrkEngine3D.Core.Components;

public abstract class Component
{
    [JsonIgnore]
    private GameObject _attachedGO;
    [JsonIgnore]
    private Transform _transform;

    [JsonIgnore]
    public GameObject GameObject => _attachedGO;

    [JsonIgnore]
    public Transform Transform => _transform;

    internal void AttachToGameObject(GameObject go, SystemRegistry registry)
    {
        _attachedGO = go;
        _transform = _attachedGO.Transform;
        InternalAttached(registry);
    }

    private bool _enabled = true;
    private bool _enabledInHierarchy = false;

    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            if (value != _enabled)
            {
                _enabled = value;
                HierarchyEnabledStateChanged();
            }
        }
    }

    public bool EnabledInHierarchy => _enabledInHierarchy;

    internal void HierarchyEnabledStateChanged()
    {
        bool newState = _enabled && GameObject.Enabled;
        if (newState != _enabledInHierarchy)
        {
            CoreHierarchyEnabledStateChanged(newState);
        }
    }

    private void CoreHierarchyEnabledStateChanged(bool newState)
    {
        Debug.Assert(newState != _enabledInHierarchy);
        _enabledInHierarchy = newState;
        if (newState)
        {
            OnEnabled();
        }
        else
        {
            OnDisabled();
        }
    }

    internal void InternalAttached(SystemRegistry registry)
    {
        Attached(registry);
        HierarchyEnabledStateChanged();
    }

    internal void InternalRemoved(SystemRegistry registry)
    {
        if (_enabledInHierarchy)
        {
            OnDisabled();
        }

        Removed(registry);
    }

    protected abstract void OnEnabled();
    protected abstract void OnDisabled();

    protected abstract void Attached(SystemRegistry registry);
    protected abstract void Removed(SystemRegistry registry);

    public override string ToString()
    {
        return $"{GetType().Name}, {GameObject.Name}";
    }
}