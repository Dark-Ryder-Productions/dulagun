using Godot;
using System;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;

namespace Dulagun.Weapons {
    /// <summary>
    /// "weapon" representing no equiped weapons
    /// </summary>
    public class unarmed : BaseWeapon {
        public override WeaponEnum GetWeaponEnum()
        {
            return WeaponEnum.Unarmed;
        }

        /// <summary>
        /// Unarmed shouldn't look to mouse position
        /// </summary>
        public override void _Process(float delta)
        {
            return;
        }

        public override void Fire()
        {
            return;
        }
    }
}
