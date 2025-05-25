using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private const float REFERENCE_BULLET_SPEED = 20;
    //This is the default speed from whcih our mass formula is derived.

    private Player player;

    [SerializeField] private Weapon currentWeapon;

    [Header("Bullet details")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunPoint;


    [Header("Inventory")]
    [SerializeField] private int maxSlots = 2;
    [SerializeField] private List<Weapon> weaponSlots;



    [SerializeField] private Transform weaponHolder;

    public Weapon CurrenWeapon()=> currentWeapon;

    private void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();

        currentWeapon.bulletsInMagazine = currentWeapon.totalReserverAmmo;
    }

    private void AssignInputEvents()
    {
        PlayerControls controls = player.controls;
        controls.Character.Fire.performed += context => Shoot();

        controls.Character.EquipSlot1.performed += context => EquipWeapon(0);
        controls.Character.EquipSlot2.performed += context => EquipWeapon(1);
        controls.Character.DropCurrentWeapon.performed += context => DropWeapon();
        controls.Character.Reload.performed += context =>
        {
            if (currentWeapon.CanReload())
            {
                player.weaponVisuals.PlayReloadAnimation();
            }
        };
    }


    public void PickupWeapon(Weapon newWeapon)
    {
        if(weaponSlots.Count >= maxSlots)
        {
            return;
        }
        weaponSlots.Add(newWeapon);
    }
    private void DropWeapon()
    {
        if(weaponSlots.Count <= 1)
        {
            return;
        }
        weaponSlots.Remove(currentWeapon);
        currentWeapon = weaponSlots[0];
    }
    private  void EquipWeapon (int i)
    {
        currentWeapon = weaponSlots[i];
    }
    private void Shoot()
    {
        if(currentWeapon.CanShoot()== false)
        {
            return;
        }
        GameObject newBullet =
            Instantiate(bulletPrefab, gunPoint.position, Quaternion.LookRotation(gunPoint.forward));

        Rigidbody rbNewBullet = newBullet.GetComponent<Rigidbody>();

        rbNewBullet.mass = REFERENCE_BULLET_SPEED / bulletSpeed;
        rbNewBullet.velocity = BulletDirection() * bulletSpeed;

        Destroy(newBullet, 10);
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }

    public Vector3 BulletDirection()
    {
        Transform aim = player.aim.Aim();

        Vector3 direction = (aim.position - gunPoint.position).normalized;

        if (player.aim.CanAimPrecisly() == false && player.aim.Target() == null)
            direction.y = 0;

        //weaponHolder.LookAt(aim);
        //gunPoint.LookAt(aim); TODO: find a better place for it. 

        return direction;
    }

    public Transform GunPoint() => gunPoint;
}
