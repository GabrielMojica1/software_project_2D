public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon Weapon;
    public WeaponDecorator(IWeapon weapon)
    {
        Weapon = weapon;
    }

    int GetCurDmg()
    {
        Weapon.GetCurDmg();  
    }
    float GetCooldown()
    {
        Weapon.GetCooldown();
    }
    List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab)
    {
        Weapon.Fire(spawnPos, bulletPrefab);
    }
    IHitStrategy GetHitStrategy()
    {
        Weapon.GetHitStrategy();
    }

}