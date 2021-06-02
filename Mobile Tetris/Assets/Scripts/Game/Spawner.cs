using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnablePieces;

    public void SpawnPiece()
    {
        int randomIndex = Random.Range(0, spawnablePieces.Length);

        Instantiate(spawnablePieces[randomIndex], transform.position, Quaternion.identity);
    }
}
