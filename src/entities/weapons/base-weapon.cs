using Godot;
using System;
using Dulagun.Shared.Enums;

namespace Dulagun.Weapons {
    /// <summary>
    /// Base class to define shared traits and methods of weapons
    /// </summary>
    public abstract class BaseWeapon : Node2D {
        public abstract WeaponEnum GetWeaponEnum();

        /// <summary>
        /// Tracks if this weapon is wielded by the left hand or not (assumed right hand if not)
        /// </summary>
        public bool isLeft = false;

        /// <summary>
        /// Is the weapon currently firing?
        /// </summary>
        public bool isFiring = false;

        /// <summary>
        /// Default weapon behavior should have it point towards the mouse position
        /// </summary>
        public override void _Process(float delta)
        {
            LookAt(GetGlobalMousePosition());
        }

        /// <summary>
        /// Fire the weapon
        /// </summary>
        public abstract void Fire();

        /// <summary>
        /// Load the packed bullet scene
        /// </summary>
        public PackedScene GetBulletScene() {
            return GD.Load<PackedScene>("res://entities/weapons/bullet/bullet.tscn");
        }

        /// <summary>
        /// Get an instance of a bullet
        /// </summary>
        public bullet GetBulletInstance() {
            return GetBulletScene().Instance() as bullet;
        }
    }
}
