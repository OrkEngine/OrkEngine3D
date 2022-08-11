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
using OrkEngine3D.Core;
using OrkEngine3D.Core.Components;
using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Physics;

public class CharacterController : Component
{
    private PhysicsSystem _physics;

    [JsonIgnore]
    public BEPUphysics.Character.CharacterController Controller { get; private set; }

    public void SetMotionDirection(Vector2 motion)
    {
        Controller.HorizontalMotionConstraint.MovementDirection = motion;
    }

    protected override void Attached(SystemRegistry registry)
    {
        _physics = registry.GetSystem<PhysicsSystem>();
        Controller = new BEPUphysics.Character.CharacterController(
            Transform.Position,
            jumpSpeed: 8f,
            tractionForce: 1500
            );
    }

    protected override void Removed(SystemRegistry registry)
    {
    }

    protected override void OnEnabled()
    {
        _physics.AddObject(Controller);
        Transform.SetPhysicsEntity(Controller.Body);
    }

    protected override void OnDisabled()
    {
        _physics.RemoveObject(Controller);
        Transform.RemovePhysicsEntity();
    }
}
