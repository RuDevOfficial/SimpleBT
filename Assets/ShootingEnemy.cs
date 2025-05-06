using System;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject BulletPrefab;
    
    public void ShootBullet()
    {
        GameObject newBullet = Instantiate(BulletPrefab, transform.position + transform.forward * 3, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        newBullet.transform.forward = transform.forward;
    }
}
