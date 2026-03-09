using UnityEngine;
using System.Collections.Generic;
public class DamageBuffDecorator : WeaponDecorator
{
    public DamageBuffDecorator(IWeapon weapon) : base(weapon)
    {
        
    }
    public override int GetCurDmg()
    {
        return base.GetCurDmg() + 2;
    }
}