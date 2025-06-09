using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GrabType { SideGrab, BackGrab };

public enum HoleType {CommonHold = 1, LowHold, HighHold };
public class WeaponModel : MonoBehaviour
{
    public WeaponType weaponType;
    public GrabType grabType;
    public HoleType holeType;

    public Transform gunPoint;
    public Transform holdPoint;
}
