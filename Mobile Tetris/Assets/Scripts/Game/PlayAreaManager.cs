using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaManager : MonoBehaviour
{
    private static int rowsRemoved = 0;

    public static readonly int fieldWidth = 10;
    public static readonly int fieldHeight = 23;
    public static Transform[,] playAreaGrid = new Transform[fieldWidth, fieldHeight];

    private static int yOffset = 2;

    public static bool IsInsideBorder(int x, int y)
    {
        return (x >= 0 && x < fieldWidth && y >= 0);
    }

    public static bool IsOutOfBounds(int x, int y)
    {
        return (x < 0 || x > fieldWidth || y >= fieldHeight - yOffset);
    }

    public static int RemoveFilledRows()
    {
        rowsRemoved = 0;

        for (int y = 0; y < fieldHeight - yOffset; ++y)
        {
            if (IsRowFilled(y))
            {
                DeleteRow(y);
                DropRowsAbove(y + 1);
                y--;
                rowsRemoved++;
            }
        }

        return rowsRemoved;
    }

    public static bool IsRowFilled(int y)
    {
        for (int x = 0; x < fieldWidth; ++x)
        {
            if (playAreaGrid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public static void DeleteRow(int y)
    {
        for (int x = 0; x < fieldWidth; ++x)
        {
            Destroy(playAreaGrid[x, y].gameObject);
            playAreaGrid[x, y] = null;
        }
    }

    public static void DropRowsAbove(int y)
    {
        for (int i = y; i < fieldHeight - yOffset; ++i)
        {
            DropDownSingleRow(i);
        }
    }

    public static void DropDownSingleRow(int y)
    {
        for (int x = 0; x < fieldWidth; ++x)
        {
            if (playAreaGrid[x, y] != null)
            {
                // Move one towards bottom
                playAreaGrid[x, y - 1] = playAreaGrid[x, y];
                playAreaGrid[x, y] = null;

                // Update Block position
                playAreaGrid[x, y - 1].position += new Vector3(0, -1, 0);
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