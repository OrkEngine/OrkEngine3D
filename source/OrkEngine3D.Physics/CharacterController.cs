using Newtonsoft.Json;
using OrkEngine3D.Components;
using OrkEngine3D.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Physics
{
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
}
