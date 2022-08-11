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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OrkEngine3D.Core.Behaviour;

public class BehaviorUpdateSystem : GameSystem
{
    private readonly SystemRegistry _registry;
    private readonly List<IUpdateable> _updateables = new List<IUpdateable>();

    private readonly BlockingCollection<IUpdateable> _newUpdateables = new BlockingCollection<IUpdateable>();
    private readonly BlockingCollection<IUpdateable> _removedUpdateables = new BlockingCollection<IUpdateable>();
    private readonly List<Behavior> _newStarts = new List<Behavior>();
    private Boolean _needsFlushing;

    private const uint MaxAllowedFlushRecursion = 1;

    public IEnumerable<IUpdateable> Updateables => _updateables;

    public BehaviorUpdateSystem(SystemRegistry sr)
    {
        _registry = sr;
        sr.GetSystem<SceneLoaderSystem>().AfterSceneLoaded += () => _needsFlushing = true;
    }

    protected override void UpdateCore(float deltaSeconds)
    {
        FlushNewAndRemovedUpdateables();

        foreach (var behavior in _updateables)
        {
            behavior.Update(deltaSeconds);
        }
        if (_needsFlushing)
        {
            FlushNewAndRemovedUpdateables();
            _needsFlushing = false;
        }
    }

    public void FlushNewAndRemovedUpdateables()
    {
        int recursion = 0;
        while (CoreFlushNewAndRemovedUpdateables() && recursion < MaxAllowedFlushRecursion)
        {
            recursion += 1;
        }
    }

    // Returns true when items were flushed. False if nothing was done.
    private bool CoreFlushNewAndRemovedUpdateables()
    {
        bool flushedAnything = false;
        _newStarts.Clear();

        IUpdateable updateable;
        while (_newUpdateables.TryTake(out updateable))
        {
            _updateables.Add(updateable);
            Behavior behavior = updateable as Behavior;
            if (behavior != null)
            {
                _newStarts.Add(behavior);
            }

            flushedAnything = true;
        }

        while (_removedUpdateables.TryTake(out updateable))
        {
            _updateables.Remove(updateable);
            Behavior behavior = updateable as Behavior;
            if (behavior != null)
            {
                _newStarts.Remove(behavior);
            }

            flushedAnything = true;
        }

        foreach (Behavior behavior in _newStarts)
        {
            behavior.StartInternal(_registry);
            flushedAnything = true;
        }

        return flushedAnything;
    }

    public void Register(IUpdateable updateable)
    {
        _newUpdateables.Add(updateable);
    }

    public void Remove(IUpdateable behavior)
    {
        _removedUpdateables.Add(behavior);
    }
}
