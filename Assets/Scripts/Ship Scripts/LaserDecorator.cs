using UnityEngine;
using System.Collections.Generic;
public class LaserDecorator : WeaponDecorator
{
    public LaserDecorator(IWeapon weapon) : base(weapon)
    {
        
    }
    public override IHitStrategy GetHitStrategy()
    {
        return new LaserHitStrategy();
    }
}