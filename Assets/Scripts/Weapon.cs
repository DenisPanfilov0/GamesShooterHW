using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private GameObject impactPrefab;
    [SerializeField] private Transform shootPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out var hit))
            {
                print(hit.transform.gameObject.name);

                Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));

                var destructible = hit.transform.GetComponent<DestructibleObject>();
                if (destructible != null)
                {
                    destructible.ReceiveDamage(damage);
                }

                var rigidbody = hit.transform.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddForce(shootPoint.forward * force, ForceMode.Impulse);
                    //hit.transform.gameObject.SetActive(false);
                }
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootPoint.position, shootPoint.forward * 9999);
    }
}
