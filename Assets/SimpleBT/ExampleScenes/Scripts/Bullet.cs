using System.Collections;
using UnityEngine;

// Example script for the bullet class
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DeathOnTime());
    }

    private void FixedUpdate() { rb.MovePosition(transform.position + transform.forward * 0.5f); }

    IEnumerator DeathOnTime()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
