using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    public int GetDamage()
    {
        return damage;
    }

    // if hit something, then destroy itself
    public void Hit()
    {
        Destroy(gameObject);
    }
}
