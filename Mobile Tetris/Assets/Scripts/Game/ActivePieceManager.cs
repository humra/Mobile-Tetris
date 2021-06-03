using UnityEngine;

public class ActivePieceManager : MonoBehaviour
{
    [SerializeField]
    private float autoFallInterval = 1f;

    private float lastFall;
    private bool isGamePaused = false;
    private float defaultAutoFallInterval;

    public IActivePieceControl activePieceControl;

    private void Start()
    {
        defaultAutoFallInterval = autoFallInterval;
        if(!IsValidGridPosition())
        {
            activePieceControl.GameOver();
        }
    }

    private void Update()
    {
        if(Time.time - lastFall >= autoFallInterval && !isGamePaused)
        {
            MovePiece(MoveDirection.Drop);
        }
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

    private bool IsOutOfBounds()
    {
        foreach (Transform child in this.transform)
        {
            Vector2 childVector = PlayAreaManager.FixVector2(child.position);

            if(PlayAreaManager.IsOutOfBounds((int)childVector.x, (int)childVector.y))
            {
                return true;
            }
        }

        return false;
    }

    private void UpdatePlayAreaGrid()
    {
        //Removes old children from playAreaGrid
        for(int y = 0; y < PlayAreaManager.fieldHeight; ++y)
        {
            for(int x = 0; x < PlayAreaManager.fieldWidth; ++x)
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

    public void MovePiece(MoveDirection direction)
    {
        switch(direction)
        {
            case MoveDirection.Left:
                transform.position += new Vector3(-1, 0, 0);
                if(IsValidGridPosition())
                {
                    UpdatePlayAreaGrid();
                }
                else
                {
                    transform.position += new Vector3(1, 0, 0);
                }
                break;

            case MoveDirection.Right:
                transform.position += new Vector3(1, 0, 0);
                if (IsValidGridPosition())
                {
                    UpdatePlayAreaGrid();
                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
                break;

            case MoveDirection.Drop:
                transform.position += new Vector3(0, -1, 0);
                if(IsValidGridPosition())
                {
                    UpdatePlayAreaGrid();
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                    activePieceControl.RowsRemoved(PlayAreaManager.RemoveFilledRows());

                    if(IsOutOfBounds())
                    {
                        activePieceControl.GameOver();
                    }
                    else
                    {
                        activePieceControl.SpawnNextPiece();
                    }

                    this.enabled = false;
                }
                lastFall = Time.time;
                break;

            case MoveDirection.Rotate:
                transform.Rotate(0, 0, 90);
                if(IsValidGridPosition())
                {
                    UpdatePlayAreaGrid();
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
                break;

            case MoveDirection.DropFast:
                if(autoFallInterval < defaultAutoFallInterval)
                {
                    autoFallInterval = defaultAutoFallInterval;
                }
                else
                {
                    autoFallInterval = 0.1f;
                }
                break;
        }
    }

    public void SetPaused(bool paused)
    {
        isGamePaused = paused;
    }
}

public enum MoveDirection
{
    Left,
    Right,
    Rotate,
    Drop,
    DropFast
}
