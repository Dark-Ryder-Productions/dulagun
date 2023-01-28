using Godot;
using System;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;

namespace Dulagun.Weapons {
    public class bullet : Area2D {
        public int speed = 1400;

        public int damage = 20;

        public override void _PhysicsProcess(float delta)
        {
            Position += Transform.x * speed * delta;
        }
    }
}