using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnablePieces;

    private Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.cyan, Color.magenta };

    public GameObject SpawnPiece()
    {
        GameObject temp = Instantiate(spawnablePieces[Random.Range(0, spawnablePieces.Length)], transform.position, Quaternion.identity);

        Color randomColor = colors[Random.Range(0, colors.Length)];
        foreach(SpriteRenderer spriteRenderer in temp.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = randomColor;
        }

        return temp;
    }
}
