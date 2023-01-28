using Godot;
using System;

namespace entities.weapons {
    /// <summary>
    // Base class to define shared traits and methods of weapons
    /// </summary>
    public abstract class BaseWeapon : Node2D {
        public abstract string GetWeaponName();

        public abstract string GetWeaponType();

        public bool isLeft = false;
    }
}
