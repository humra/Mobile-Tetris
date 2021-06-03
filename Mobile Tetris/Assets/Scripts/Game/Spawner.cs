using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnablePieces;

    private Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.cyan, Color.magenta };
    private int[] randomPieceIndex = new int[100];
    private int currentIndex = 0;

    private void Start()
    {
        CreateRandomIndexes();   
    }

    private void CreateRandomIndexes()
    {
        for (int i = 0; i < randomPieceIndex.Length; i++)
        {
            randomPieceIndex[i] = Random.Range(0, spawnablePieces.Length);
        }

        currentIndex = 0;
    }

    public GameObject SpawnPiece()
    {
        if(currentIndex == randomPieceIndex.Length - 1)
        {
            CreateRandomIndexes();
        }
        
        GameObject temp = Instantiate(spawnablePieces[randomPieceIndex[currentIndex]], transform.position, Quaternion.identity);

        Color randomColor = colors[Random.Range(0, colors.Length)];
        foreach(SpriteRenderer spriteRenderer in temp.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = randomColor;
        }

        currentIndex++;
        return temp;
    }

    public int GetNextPieceIndex()
    {
        return randomPieceIndex[currentIndex];
    }
}
