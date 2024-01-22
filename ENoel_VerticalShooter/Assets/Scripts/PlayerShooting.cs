using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;
    [SerializeField] GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        // Shoot bullet
        Instantiate(bulletPrefab, spawnLocation.position, spawnLocation.rotation);
    }
}