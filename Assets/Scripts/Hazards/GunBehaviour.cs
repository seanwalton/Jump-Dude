using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] fireLocations;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireDelay;
    [SerializeField] private int numberOfBullets;


    private List<GameObject> myBullets = new List<GameObject>();
    private GameObject newBullet;
    private Vector3 bulletVelocity;
    private int currentBullet;

    private void Start()
    {
        InitalisePool();
        InvokeRepeating("FireBullets", fireDelay, fireDelay);
    }

    private void InitalisePool()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(false);
            myBullets.Add(newBullet);
        }

        currentBullet = 0;
    }

    private void FireBullets()
    {
        for (int i = 0; i < fireLocations.Length; i++)
        {

            newBullet = myBullets[currentBullet];
            newBullet.SetActive(true);
            newBullet.transform.position = fireLocations[i].position;
            bulletVelocity = fireLocations[i].up * bulletSpeed;
            newBullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

            currentBullet++;
            if (currentBullet >= numberOfBullets) currentBullet = 0;

        }
    }

}
