namespace Dulagun.Shared.Enums {
    /// <summary>
    /// Enum for each player weapon
    /// </summary>
    public enum WeaponEnum {
        Unarmed = 0,
        Pistol = 1,
        Shotgun = 2
    }

    static class WeaponEnumExtensions {
        /// <summary>
        /// Get the resource path at which the weapon scene can be loaded
        /// </summary>

        public static string GetResourcePath(this WeaponEnum weapon) {
            switch (weapon) {
                case WeaponEnum.Pistol: return "res://entities/weapons/pistol/pistol.tscn";
			    case WeaponEnum.Shotgun: return "res://entities/weapons/shotgun/shotgun.tscn";
			    default: return "res://entities/weapons/unarmed/unarmed.tscn";
            }
        }

        /// <summary>
        /// Get the type of weapon (gun, melee, etc)
        /// </summary>
        public static WeaponTypeEnum GetWeaponType(this WeaponEnum weapon) {
            switch(weapon) {
                case WeaponEnum.Pistol:
                case WeaponEnum.Shotgun:
                    return WeaponTypeEnum.SemiAutoGun;
                default:
                    return WeaponTypeEnum.None;
            }
        }
    }
}