using Godot;
using System;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;

public class unarmed : BaseWeapon {
    public override WeaponEnum GetWeaponEnum()
    {
        return WeaponEnum.Unarmed;
    }
}