using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnLocation;
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
        // Shoot bullet
        Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
    }

    IEnumerator Co_ShootRoutine()
    {
        if (!maxShotsReached)
        {
            laserSound.Play();
            maxShotsReached = true;
            Fire();
            yield return new WaitForSeconds(PlayerWeaponsManager.GetTimeBetweenShots());
            maxShotsReached = false;
        }
    }
}