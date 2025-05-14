using UnityEngine;

// Example class for the Ant3D behavior.
public class PeaSpawnerExtras : MonoBehaviour
{
    [SerializeField] private GameObject _peaPrefab;
    
    public void SpawnFood()
    {
        GameObject newPea = Instantiate(_peaPrefab);
        float xPosition = Random.Range(-10f, 10f);
        float zPosition = Random.Range(-10f, 10f);
        newPea.transform.position = new Vector3(xPosition, 1, zPosition);
    }
}
