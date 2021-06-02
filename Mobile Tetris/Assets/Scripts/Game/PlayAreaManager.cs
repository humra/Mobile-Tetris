using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaManager : MonoBehaviour
{
    private static int rowsRemoved = 0;

    public static readonly int fieldWidth = 10;
    public static readonly int fieldHeight = 21;
    public static Transform[,] playAreaGrid = new Transform[fieldWidth, fieldHeight];

    public static bool IsInsideBorder(int x, int y)
    {
        return (x >= 0 && x < fieldWidth && y >= 0);
    }

    public static int RemoveFilledRows()
    {
        rowsRemoved = 0;

        for(int y = 0; y < fieldHeight; y++)
        {
            if(IsRowFilled(y))
            {
                DeleteRow(y);
                DeleteRow(y);
                rowsRemoved++;
                y--;
            }
        }

        return rowsRemoved;
    }

    public static bool IsRowFilled(int rowIndex)
    {
        for(int x = 0; x < fieldWidth; x++)
        {
            if(playAreaGrid[x, rowIndex] == null)
            {
                return false;
            }
        }

        return true;
    }

    public static void DeleteRow(int rowIndex)
    {
        for(int x = 0; x < fieldWidth; x++)
        {
            Destroy(playAreaGrid[x, rowIndex].gameObject);
            playAreaGrid[x, rowIndex] = null;
        }

        DropDownRows(rowIndex);
    }

    public static void DropDownRows(int removedRowIndex)
    {
        for(int x = 0; x < fieldWidth; x++)
        {
            for(int y = removedRowIndex; y < fieldHeight; y++)
            {
                if (playAreaGrid[x, y] != null)
                {
                    playAreaGrid[x, y - 1] = playAreaGrid[x, y];
                    playAreaGrid[x, y] = null;

                    playAreaGrid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    //Rotating the game piece sometimes results in non-round numbers such as 1.00000001
    //This function makes sure they are set properly
    public static Vector2 FixVector2(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}
