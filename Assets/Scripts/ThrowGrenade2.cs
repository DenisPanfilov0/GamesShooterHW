using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    [SerializeField] public float delay = 3f;
    [SerializeField] public float radius = 5f;
    [SerializeField] public float force = 700f;
    [SerializeField] public float damage = 80f;


    public GameObject explosionEffect;

    private float countdown;
    private bool hasExploded = false;






    [SerializeField] private float throwForce = 2;
    [SerializeField] private GameObject granadePrefab;
    [SerializeField] private Transform shootPoint;
    public GameObject expGrenade;

    void Start()
    {

        countdown = delay;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out var hit))
            {
                GameObject granade = Instantiate(granadePrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
                var rigidbody = granade.GetComponent<Rigidbody>();
                rigidbody.AddForce(shootPoint.forward * throwForce, ForceMode.VelocityChange);
                Explode();

                
            }
            
        }
    }

    private void Explode()
    {
        
        countdown -= Time.deltaTime;
        if (countdown <= 0f )//&& !hasExploded)
        {
            Explode();
            //hasExploded = true;
        }
        //Спавн эффекта взрыва
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //Получение ближайших объектов
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            //Находим на объектах Rigidbody
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Если находим, добавляем силу направленную от гранаты
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

