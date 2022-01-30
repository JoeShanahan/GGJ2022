using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _velocity;
    [SerializeField] Rigidbody _rb;


    void Start()
    {
        _rb.velocity = -transform.up * _velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        bool isPlayer = other.tag == MyTags.ThePlayer;
        bool isVillager = other.tag == MyTags.Villager;

        if (isPlayer || isVillager)
        {
            Destroy(_rb);
            Destroy(GetComponent<Collider>());
            transform.SetParent(other.GetComponent<bodylocator>().body);
        }
        else
        {
            Destroy(_rb);
            Destroy(GetComponent<Collider>());
        }
    }
}
