using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _velocity;
    [SerializeField] Rigidbody _rb;
    [SerializeField] bool _doesExplode;
    [SerializeField] float _explodeForce;
    [SerializeField] int _explodeDamage;
    [SerializeField] float _explodeRadius;
    [SerializeField] GameObject _explodeParticles;

    void Start()
    {
        _rb.velocity = -transform.up * _velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (_doesExplode)
        {
            foreach (var collider in Physics.OverlapSphere(transform.position, _explodeRadius))
            {
                if (collider.tag == MyTags.Villager)
                    collider.GetComponent<Villager>().ExplosiveDamage(transform.position, _explodeDamage, _explodeForce, _explodeRadius);

                if (collider.tag == MyTags.ThePlayer)
                    collider.GetComponent<PlayerMove>().ExplosiveDamage(transform.position, _explodeDamage, _explodeForce, _explodeRadius);
            }

            Destroy(gameObject);
            return;
        }

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
