using Godot;
using System;
using System.Linq;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;

namespace Dulagun.Weapons {
    /// <summary>
    /// Semi auto shotgun 
    /// </summary>
    public class shotgun : BaseWeapon {
        public shotgun() {
            fireRate = 0.7F;
        }

        public override WeaponEnum GetWeaponEnum()
        {
            return WeaponEnum.Shotgun;
        }

        public override void Fire()
        {
            // Placeholder until pellets are added
            PackedScene bulletScene = GetBulletScene();
            Position2D muzzle = GetNode<Position2D>("Muzzle");
            GetNode<AudioStreamPlayer2D>("Gunshot").Play();

            muzzle.RotationDegrees = 0;
            // Spread pellets
            foreach(int degrees in  new int[] { -5, -3, 0, 3, 5 }) {
                bullet bullet = bulletScene.Instance() as bullet;
                GetParent().AddChild(bullet);

                muzzle.RotationDegrees = degrees;
                bullet.GlobalTransform = muzzle.GlobalTransform;
            }
        }
    }
}

