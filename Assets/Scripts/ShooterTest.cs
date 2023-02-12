using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTest : MonoBehaviour
{
    [SerializeField] private int force = 10;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit))
            {
                print(hit.transform.gameObject.name);

                var rigidbody = hit.transform.GetComponent<Rigidbody>();
                if (rigidbody == null)
                {
                    //hit.transform.gameObject.SetActive(false);
                    return;
                }
                rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            }
        }
    }
}
