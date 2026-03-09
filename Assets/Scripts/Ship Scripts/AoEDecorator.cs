using UnityEngine;
using System.Collections.Generic;
public class AoEDecorator : WeaponDecorator
{
    public AoEDecorator(IWeapon weapon) : base(weapon)
    {
        
    }
    public override IHitStrategy GetHitStrategy()
    {
        return new AoEExplosionStrategy();
    }
}