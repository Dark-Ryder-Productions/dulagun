using Godot;
using System;
using Dulagun.Shared.Enums;

namespace Dulagun.Weapons {
    /// <summary>
    /// Base class to define shared traits and methods of weapons
    /// </summary>
    public abstract class BaseWeapon : Node2D {
        # region Animation Constants

        public const string FRONT_IDLE_ANIM = "front-idle";
        public const string BACK_IDLE_ANIM = "back-idle";

        # endregion

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
        /// Interval between sequential firings in seconds
        /// </summary>
        public float fireRate = 0;

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
