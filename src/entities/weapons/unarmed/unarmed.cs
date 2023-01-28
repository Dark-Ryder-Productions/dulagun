using Godot;
using System;
using entities.weapons;

public class unarmed : BaseWeapon {
    public override string GetWeaponName()
    {
        return "unarmed";
    }

    public override string GetWeaponType()
    {
        return "melee";
    }
}