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

using OrkEngine3D.Core.Components;

namespace OrkEngine3D.Core.Behaviour;

public abstract class Behavior : Component, IUpdateable
{
    private BehaviorUpdateSystem _bus;

    protected sealed override void Attached(SystemRegistry registry)
    {
        _bus = registry.GetSystem<BehaviorUpdateSystem>();
        PostAttached(registry);
    }

    protected sealed override void Removed(SystemRegistry registry)
    {
        PostRemoved(registry);
    }

    protected sealed override void OnEnabled()
    {
        _bus.Register(this);
        PostEnabled();
    }

    protected sealed override void OnDisabled()
    {
        _bus.Remove(this);
        PostDisabled();
    }

    public abstract void Update(float deltaSeconds);

    internal void StartInternal(SystemRegistry registry) => Start(registry);
    protected virtual void Start(SystemRegistry registry) { }
    protected virtual void PostEnabled() { }
    protected virtual void PostDisabled() { }
    protected virtual void PostAttached(SystemRegistry registry) { }
    protected virtual void PostRemoved(SystemRegistry registry) { }
}
