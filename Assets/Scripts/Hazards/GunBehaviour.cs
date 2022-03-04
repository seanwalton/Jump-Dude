using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] fireLocations;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireDelay;
    private GameObject newBullet;
    private Vector3 bulletVelocity;

    private void Start()
    {
        InvokeRepeating("FireBullets", fireDelay, fireDelay);
    }

    private void FireBullets()
    {
        for (int i = 0; i < fireLocations.Length; i++)
        {
            newBullet = Instantiate(bulletPrefab,
                fireLocations[i].position, Quaternion.identity);
            bulletVelocity = fireLocations[i].up * bulletSpeed;
            newBullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
        }
    }

}
