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
    }
}
