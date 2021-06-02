using UnityEngine;

public class ActivePieceManager : MonoBehaviour
{
    private void Update()
    {
        
    }

    private bool IsValidGridPosition()
    {
        foreach(Transform child in this.transform)
        {
            Vector2 childVector = PlayAreaManager.FixVector2(child.position);

            if(!PlayAreaManager.IsInsideBorder((int)childVector.x, (int)childVector.y))
            {
                return false;
            }

            //Checks intersection within the piece in case the rotation would case it to
            //intersect with itself and give a false result
            if(PlayAreaManager.playAreaGrid[(int)childVector.x, (int)childVector.y] != null &&
                PlayAreaManager.playAreaGrid[(int)childVector.x, (int)childVector.y].parent != this.transform)
            {
                return false;
            }
        }

        return true;
    }

    private void UpdatePlayAreaGrid()
    {
        //TO-DO
        //CHECK IF NEEDS CHANGING TO ++Y
        //Removes old children from playAreaGrid
        for(int y = 0; y < PlayAreaManager.fieldHeight; y++)
        {
            for(int x = 0; x < PlayAreaManager.fieldWidth; x++)
            {
                if(PlayAreaManager.playAreaGrid[x, y] != null)
                {
                    if(PlayAreaManager.playAreaGrid[x, y].parent == transform)
                    {
                        PlayAreaManager.playAreaGrid[x, y] = null;
                    }
                }
            }
        }

        //Adds new children to the playAreaGrid
        foreach(Transform child in transform)
        {
            Vector2 childVector = PlayAreaManager.FixVector2(child.position);
            PlayAreaManager.playAreaGrid[(int)childVector.x, (int)childVector.y] = child;
        }
    }
}
