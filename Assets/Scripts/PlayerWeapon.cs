using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] BlasterShot blasterShotPrefab;
    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] Transform firePoint;

    [SerializeField] float delay = .25f;
    float nextFireTime = 1f;
    List<PowerUp> powerUps  = new List<PowerUp>();

    public void AddPowerUp(PowerUp powerUp) => powerUps.Add(powerUp);

    public void RemovePowerUp(PowerUp powerUp) => powerUps.Remove(powerUp);

    void Update()
    {
        AimTowardMouse();

        if(ReadToFire())
        {
            Fire();
        }
    }

    void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    bool ReadToFire() => Time.time >= nextFireTime;

    private void Fire()
    {
        float _delay = delay;
        foreach (var item in powerUps)
        {
            _delay *= item.DelayMultiplier;
        }

        nextFireTime = Time.time + _delay;
        var shot = Instantiate(blasterShotPrefab, firePoint.position, transform.rotation);

        shot.Launch(transform.forward);
    }

}
