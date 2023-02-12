using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGranade : MonoBehaviour
{
    [SerializeField] private float throwForce = 40;
    [SerializeField] private GameObject granadePrefab;
    [SerializeField] private Transform shootPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out var hit))
            {
                GameObject granade = Instantiate(granadePrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
                var rigidbody = granade.GetComponent<Rigidbody>();
                rigidbody.AddForce(shootPoint.forward * throwForce, ForceMode.VelocityChange);
            }
        }
    }
}
