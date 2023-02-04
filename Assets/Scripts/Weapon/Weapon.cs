using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, Shootable
{
    [SerializeField] protected float damage;
    public void Shoot()
    {
        Debug.Log(name+ ": " + damage); 
    }
}
