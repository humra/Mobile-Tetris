using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnablePieces;

    public GameObject SpawnPiece()
    {
        int randomIndex = Random.Range(0, spawnablePieces.Length);

        return Instantiate(spawnablePieces[randomIndex], transform.position, Quaternion.identity);
    }
}
