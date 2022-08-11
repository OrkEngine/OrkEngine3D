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

using BEPUphysics;
using BEPUphysics.CollisionRuleManagement;
using BEPUutilities.Threading;
using OrkEngine3D.Core;
using OrkEngine3D.Mathematics;
using System;
using System.Collections.Concurrent;

namespace OrkEngine3D.Physics;

public class PhysicsSystem : GameSystem
{
    private const float MaxFrameUpdateTime = 30f / 60f;

    private static readonly Vector3 s_defaultGravity = new Vector3(0, -9.81f, 0);

    private PhysicsCollisionGroups _collisionGroups;
    private readonly ParallelLooper _looper;

    private BlockingCollection<ISpaceObject> _additions = new BlockingCollection<ISpaceObject>();
    private BlockingCollection<ISpaceObject> _removals = new BlockingCollection<ISpaceObject>();

    public Space Space { get; }
    public int LayerCount => _collisionGroups.GetLayerCount();
    public int GetLayerByName(string name) => _collisionGroups.GetLayerByName(name);

    public PhysicsSystem(PhysicsLayersDescription layers)
    {
        _collisionGroups = new PhysicsCollisionGroups(layers);
        CollisionRules.CollisionRuleCalculator = CustomCollisionRuleCalculator;
        _looper = new ParallelLooper();
        for (int g = 0; g < Environment.ProcessorCount - 1; g++)
        {
            _looper.AddThread();
        }

        Space = new Space(_looper);
        Space.ForceUpdater.Gravity = s_defaultGravity;
        Space.BufferedStates.Enabled = true;
        Space.BufferedStates.InterpolatedStates.Enabled = true;
    }

    public CollisionGroup GetCollisionGroup(int layer)
    {
        return _collisionGroups.GetCollisionGroup(layer);
    }

    public void SetPhysicsLayerRules(PhysicsLayersDescription layers)
    {
        _collisionGroups = new PhysicsCollisionGroups(layers);
    }

    protected override void UpdateCore(float deltaSeconds)
    {
        FlushAdditionsAndRemovals();

        deltaSeconds = Math.Min(deltaSeconds, MaxFrameUpdateTime);
        Space.Update(deltaSeconds);
    }

    public void AddObject(ISpaceObject spaceObject)
    {
        _additions.Add(spaceObject);
    }

    public void RemoveObject(ISpaceObject spaceObject)
    {
        _removals.Add(spaceObject);
    }

    protected override void OnNewSceneLoadedCore()
    {
        Space.ForceUpdater.Gravity = s_defaultGravity;
    }

    private CollisionRule CustomCollisionRuleCalculator(ICollisionRulesOwner aOwner, ICollisionRulesOwner bOwner)
    {
        CollisionRules a = aOwner.CollisionRules;
        CollisionRules b = bOwner.CollisionRules;
        CollisionRule groupRule = CollisionRules.GetGroupCollisionRuleDefault(a, b);
        if (groupRule == CollisionRule.NoBroadPhase)
        {
            return groupRule;
        }

        return CollisionRules.GetCollisionRuleDefault(aOwner, bOwner);
    }

    private void FlushAdditionsAndRemovals()
    {
        ISpaceObject addition;
        while (_additions.TryTake(out addition))
        {
            Space.Add(addition);
        }

        ISpaceObject removal;
        while (_removals.TryTake(out removal))
        {
            Space.Remove(removal);
        }
    }
}
