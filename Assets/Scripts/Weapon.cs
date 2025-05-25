public enum WeaponType
{
    Pistol,
    Revolver,
    AutoRifle,
    Shotgun,
    Rifle
}

[System.Serializable]
public class Weapon
{
    public WeaponType weaponType;
    public int bulletsInMagazine;
    public int magazineCapacity;
    public int totalReserverAmmo;

    public bool CanShoot()
    {
        return HaveEnoughBullets();
    }

    public bool HaveEnoughBullets()
    {
        if (bulletsInMagazine > 0)
        {
            bulletsInMagazine--;
            return true;
        }
        return false;
    }

    public bool CanReload()
    {
        if (bulletsInMagazine == magazineCapacity)
            return false;


        if (totalReserverAmmo > 0)
        {
            return true;
        }
        return false;
    }

    public void RefillBullets()
    {
        int bulletsToReload = magazineCapacity;

        if (bulletsToReload > totalReserverAmmo)
        {
            bulletsToReload = totalReserverAmmo;
        }
        totalReserverAmmo -= bulletsToReload;
        bulletsInMagazine = bulletsToReload;
        if (totalReserverAmmo < 0)
        {
            totalReserverAmmo = 0;
        }
    }
}
