using Godot;
using System;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;

namespace Dulagun.Weapons {
    /// <summary>
    /// Semi auto pistol weapon
    // </summary>
    public class pistol : BaseWeapon {
        public override WeaponEnum GetWeaponEnum()
        {
            return WeaponEnum.Pistol;
        }

        public override void Fire()
        {
            bullet bullet = GetBulletInstance();

            GetParent().AddChild(bullet);
            GetNode<AudioStreamPlayer2D>("Gunshot").Play();
            bullet.GlobalTransform = GetNode<Position2D>("Muzzle").GlobalTransform;
        }
    }
}
