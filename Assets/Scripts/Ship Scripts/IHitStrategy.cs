using UnityEngine;
using System.Collections.Generic;
public interface IHitStrategy
{
    bool Execute(Collider2D enemy, int dmgAmt);
}