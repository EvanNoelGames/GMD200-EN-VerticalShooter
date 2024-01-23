using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnLocation;
    [SerializeField] GameObject bulletPrefab;

    public float timeBetweenShots = 0.25f;
    private bool maxShotsReached = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !maxShotsReached)
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
            maxShotsReached = true;
            Fire();
            yield return new WaitForSeconds(timeBetweenShots);
            maxShotsReached = false;
        }
    }
}