using System;
using UnityEngine;

public class RaycastVisualizer2D : MonoBehaviour
{
    [SerializeField] private float inbetweenDistance;
    [SerializeField] private float rayDistance;

    private Rigidbody2D rb2D;
    
    private void Awake() { rb2D = GetComponent<Rigidbody2D>(); }

    private void OnDrawGizmos()
    {
        if (rb2D) {
            Vector2 vector = (Vector2)gameObject.transform.position + new Vector2(rb2D.linearVelocityX >= 0 ? 1 : -1, 0) * inbetweenDistance;
            Gizmos.DrawLine(vector, vector + Vector2.down * rayDistance );
        }
    }
}
