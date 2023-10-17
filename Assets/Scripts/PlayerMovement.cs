using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] float moveSpeed = 15f;


    
    Animator animator;

    void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement *= moveSpeed * Time.deltaTime;

        transform.Translate(movement, Space.World);

        animator.SetFloat("VelocityX", horizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityZ", vertical, 0.1f, Time.deltaTime);
    }



}
