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
using System.Collections.Generic;
using BEPUphysics.CollisionRuleManagement;

namespace OrkEngine3D.Physics;

public class PhysicsLayersDescription
{
    public List<string> LayerNames { get; set; } = new List<string>();
    public List<uint> LayerCollisionRules { get; set; } = new List<uint>();

    public static PhysicsLayersDescription Default
    {
        get
        {
            var defaultDescription = new PhysicsLayersDescription();
            defaultDescription.LayerNames.Add("Standard");
            defaultDescription.LayerCollisionRules.Add(0xFFFFFFFF);
            return defaultDescription;
        }
    }

    public PhysicsLayersDescription() { }

    public int GetLayerCount() => LayerNames.Count;

    public string GetLayerName(int layer)
    {
        ValidateLayerIndex(layer);
        return LayerNames[layer];
    }

    public void SetLayerName(int layer, string value)
    {
        ValidateLayerIndex(layer);
        LayerNames[layer] = value;
    }

    public uint GetLayerCollisionRule(int layer)
    {
        ValidateLayerIndex(layer);
        return LayerCollisionRules[layer];
    }

    public bool GetDoLayersCollide(int layer1, int layer2)
    {
        ValidateLayerIndex(layer1, nameof(layer1));
        ValidateLayerIndex(layer2, nameof(layer2));

        return IsBitSet(GetLayerCollisionRule(layer1), layer2);
    }

    public void SetLayersCollide(int layer1, int layer2, bool value)
    {
        ValidateLayerIndex(layer1, nameof(layer1));
        ValidateLayerIndex(layer2, nameof(layer2));

        uint layerCollisionRule1 = LayerCollisionRules[layer1];
        layerCollisionRule1 = SetBit(layerCollisionRule1, layer2, value);
        LayerCollisionRules[layer1] = layerCollisionRule1;

        uint layerCollisionRule2 = LayerCollisionRules[layer2];
        layerCollisionRule2 = SetBit(layerCollisionRule2, layer1, value);
        LayerCollisionRules[layer2] = layerCollisionRule2;
    }

    public int GetLayerByName(string name)
    {
        for (int i = 0; i < LayerNames.Count; i++)
        {
            if (LayerNames[i] == name)
            {
                return i;
            }
        }

        throw new InvalidOperationException("There was no physics layer with the name " + name);
    }

    public void AddLayer(string name)
    {
        LayerNames.Add(name);
        LayerCollisionRules.Add(0xFFFFFFFF);
    }

    public void RemoveLastLayer()
    {
        LayerNames.RemoveAt(LayerNames.Count - 1);
        LayerCollisionRules.RemoveAt(LayerCollisionRules.Count - 1);
    }

    private uint SetBit(uint bits, int index, bool value)
    {
        uint mask = 1U << index;
        if (value)
        {
            return bits | mask;
        }
        else
        {
            return bits & ~mask;
        }
    }

    private bool IsBitSet(uint layerRule, int bit)
    {
        return ((layerRule >> bit) & 1) == 1;
    }

    private void ValidateLayerIndex(int layer, string argName = "layer")
    {
        if (layer < 0 || layer >= LayerNames.Count)
        {
            throw new ArgumentOutOfRangeException(argName);
        }
    }
}

public class PhysicsCollisionGroups
{
    private readonly CollisionGroup[] _collisionGroupsByLayer;
    private readonly PhysicsLayersDescription _layers;

    public PhysicsCollisionGroups(PhysicsLayersDescription layers)
    {
        _layers = layers;
        int layerCount = layers.GetLayerCount();
        _collisionGroupsByLayer = new CollisionGroup[layerCount];
        for (int i = 0; i < layerCount; i++)
        {
            _collisionGroupsByLayer[i] = new CollisionGroup();
        }

        for (int i = 0; i < layerCount; i++)
        {
            CollisionGroup group = _collisionGroupsByLayer[i];
            for (int g = i + 1; g < layerCount; g++)
            {
                bool collides = layers.GetDoLayersCollide(i, g);
                if (!collides)
                {
                    CollisionRules.CollisionGroupRules.Add(new CollisionGroupPair(group, _collisionGroupsByLayer[g]), CollisionRule.NoBroadPhase);
                }
            }
        }
    }

    public int GetLayerCount() => _layers.GetLayerCount();
    public CollisionGroup GetCollisionGroup(int layer)
    {
        if (layer < 0 || layer >= GetLayerCount())
        {
            throw new ArgumentOutOfRangeException(nameof(layer));
        }

        return _collisionGroupsByLayer[layer];
    }

    public int GetLayerByName(string name)
    {
        return _layers.GetLayerByName(name);
    }
}
