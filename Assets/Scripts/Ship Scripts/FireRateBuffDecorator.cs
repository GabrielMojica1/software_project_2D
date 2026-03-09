using UnityEngine;
using System.Collections.Generic;
public class FireRateBuffDecorator : WeaponDecorator
{
    public FireRateBuffDecorator(IWeapon weapon) : base(weapon)
    {
        
    }
    public override float GetCooldown()
    {
        return base.GetCooldown() * 0.25f;
    }
}