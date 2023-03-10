using Godot;
using System;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;
using Dulagun.Enemies;

namespace Dulagun.Weapons {
    public class bullet : Area2D {
        public int speed = 1400;

        public int damage = 20;

        public override void _PhysicsProcess(float delta)
        {
            Position += Transform.x * speed * delta;
        }

        /// <summary>
        /// Handle bullet collision with a body
        /// </summary>
        public void _onArea2DBodyEntered(Node body) {
            if (body is BaseEnemy) {
                (body as BaseEnemy).TakeDamage(damage);
            }

            QueueFree();
        }

        /// <summary>
        /// Remove bullet when it leaves the screen
        /// </summary>
        public void _onScreenExit() {
            QueueFree();
        }
    }
}