using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float speed, attackSpeed, range, cost, damage, rangeRadius;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
  
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    public float Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float RangeRadius
    {
        get { return rangeRadius; }
        set { rangeRadius = value; }
    }

    public void DetectRange()
    {
        if (Physics.CheckSphere(transform.position,RangeRadius))
        {
            print("Hi");
        }
        else
        {
            return;
        }
    }
    private void Update()
    {
        DetectRange();
    }

}
