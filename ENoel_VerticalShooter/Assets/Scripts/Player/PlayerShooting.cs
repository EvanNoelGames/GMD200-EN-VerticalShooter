using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnLocation, bulletSpawnLocation2, bulletSpawnLocation3;
    [SerializeField] GameObject bulletPrefab;

    public AudioSource laserSound;

    private bool maxShotsReached = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !maxShotsReached)
        {
            StartCoroutine(Co_ShootRoutine());
        }
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
    }

    private void ShotgunFire()
    {
        Instantiate(bulletPrefab, bulletSpawnLocation2.position, bulletSpawnLocation.rotation);
        Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        Instantiate(bulletPrefab, bulletSpawnLocation3.position, bulletSpawnLocation.rotation);
    }

    IEnumerator Co_ShootRoutine()
    {
        if (!maxShotsReached)
        {
            laserSound.Play();
            maxShotsReached = true;
            if (!PlayerWeaponsManager.shotgun)
            {
                Fire();
            }
            else
            {
                ShotgunFire();
            }
            yield return new WaitForSeconds(PlayerWeaponsManager.GetTimeBetweenShots());
            maxShotsReached = false;
        }
    }
}