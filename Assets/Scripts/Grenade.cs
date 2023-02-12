using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Grenade : MonoBehaviour
{
    [SerializeField] public float delay = 3f;
    [SerializeField] public float radius = 5f;
    [SerializeField] public float force = 700f;
    [SerializeField] public float damage = 80f;


    [SerializeField] private float throwForce = 40;
    /*[SerializeField] public GameObject grenadePrefab;
    [SerializeField] private Transform shootPoint;
    public GameObject expGrenade;*/

    public GameObject explosionEffect;

    private float countdown;
    private bool hasExploded = false;

    void Start()
    {
        countdown = delay;
    }


    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

    }

    /*public void ThrowBobm ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out var hit))
            {
                GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
                var rigidbody = grenade.GetComponent<Rigidbody>();
                rigidbody.AddForce(shootPoint.forward * throwForce);
            }
        }
    }*/




    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);

            }

            var destructible = nearbyObject.GetComponent<DestructibleObject>();
            if (destructible != null)
            {
                destructible.ReceiveDamage(damage);
            }


        }

        Destroy(gameObject);
    }
}
