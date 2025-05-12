using System;
using SimpleBT.NonEditor;
using UnityEngine;

// Example script for the Shooter 3D scene
public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    private SBTBlackboard _blackboard;

    private void Awake() { _blackboard = GetComponent<SBTBlackboard>(); }

    public void ShootBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward * 3, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        newBullet.transform.forward = transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Projectile") == false) return;

        float health = _blackboard.GetValue<float>("HEALTH");
        _blackboard.AddValue("HEALTH", Mathf.Max(0, health - 1));
    }
}
