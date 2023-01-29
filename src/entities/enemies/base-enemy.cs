using System;
using Godot;

namespace Dulagun.Enemies {
    /// <summary>
    /// Base class outlining enemy behavior
    /// </summary>
    public class BaseEnemy : KinematicBody2D {
        /// <summary>
        /// Total health of an enemy
        /// </summary>
        public int health = 100;

        /// <summary>
        /// Is the enemy currently alive
        /// </summary>
        public bool isAlive = true;

        // <summary>
        /// Can the enemy see the player?
        /// </summary>
        protected bool playerSpotted = false;
        
        // <summary>
        /// Enemy's reference to the player
        /// </summary>
        protected player playerRef;

        /// <summary>
        /// How fast can the enemy move?
        /// </summary>
        protected int speed = 100;

        /// <summary>
        /// How fast does the enemy fall?
        /// </summary>
        private const int GRAVITY = 80;

        /// <summary>
        /// Enemy's velocity
        /// </summary>
        protected Vector2 vel = new Vector2();

        public override void _Ready()
        {
            AddToGroup("enemies");
        }

        public override void _Process(float delta)
        {
            if (!isAlive) {
                QueueFree();
                return;
            }

            vel.y += GRAVITY * delta;
        }

        /// <summary>
        /// Handle bullet collision with a body
        /// </summary>
        public void TakeDamage(int dmg) {
            health -= dmg;
            if (health <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Handle bullet collision with a body
        /// </summary>
        private void Die() {
            // Animation handling will be added here
            // Set isAlive to false so that freeing the object will happen in the process loop, preventing a race condition
            // where the enemy is freed while the process loop is running for the next frame(s)
            isAlive = false;
        }
    }
}