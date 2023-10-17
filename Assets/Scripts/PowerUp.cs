using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _delayMultiplier = 0.5f;
    [SerializeField] float duration = 5f;
    [SerializeField] float coolDown = 10f;

    public float DelayMultiplier => _delayMultiplier;

    void OnTriggerEnter(Collider other) 
    {
        var playerWeapon = other.GetComponent<PlayerWeapon>();
        if(playerWeapon)
        {
            playerWeapon.AddPowerUp(this);
            StartCoroutine(DisableAfterDelay(playerWeapon));
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }    
    }

    IEnumerator DisableAfterDelay(PlayerWeapon playerWeapon)
    {
        yield return new WaitForSeconds(duration);
        playerWeapon.RemovePowerUp(this);
        yield return new WaitForSeconds(coolDown);
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }
}
