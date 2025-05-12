using UnityEngine;

public class PlayerFPSExample : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject ShootGameObjectPosition;

    public void OnShoot()
    {
        GameObject newBullet = Instantiate(BulletPrefab, ShootGameObjectPosition.transform.position + transform.forward * 3, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        newBullet.transform.forward = transform.forward;
    }
}
