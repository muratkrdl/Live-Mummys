using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] float speed = 15f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Launch(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

}
