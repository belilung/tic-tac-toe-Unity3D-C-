using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    //init prefabs
    //X, O: closure
    //playerChip: prefab for initiate board blocks 
    public GameObject X, O, playerChip;
    //board size, default = 3.
    public int boardSize = 3;

    //current player, cross (1) or noncross (0). default: 1.
    private int playerMoveState;
    //i: rows. j: columns
    private int i, j;
    //raycast for PC mouse and events (click)
    private RaycastHit hit;
    //bool val for game status. If false - game not over. If true, - gameover. default: false. 
    private bool gameover = false;
    //board - square matrix
    private GameObject[,] board;

    // Use this for initialization
    void Start () {
        playerMoveState = 1;
        board = new GameObject[boardSize, boardSize];
        
        //fill board
        for (i=0; i<boardSize; i++)
        {
            for (j=0; j<boardSize; j++)
            {
                Vector3 blockVector = new Vector3(this.transform.position.x + i, this.transform.position.y, this.transform.position.z + j);
                board[i, j] = Instantiate(playerChip, blockVector, Quaternion.identity, this.GetComponent<Transform>());
                //each block have i and j value (playerChip class)
                board[i, j].GetComponent<playerChip>().setI(i);
                board[i, j].GetComponent<playerChip>().setJ(j);
            }
        }
	}

    // Update is called once per frame
    //if game not over - start events
    void Update() {
        if (gameover == false)
        {
            eventsOnBoard();
        }
    }

    //Check for main and side diagonal win rule
    void diagonalChecker()
    {
        int y = 0, z = boardSize - 1;
        for (int x = 0; x < boardSize; x++)
        {
            if (board[x, x].GetComponent<playerChip>().getOwner() == playerMoveState
                || board[x, z].GetComponent<playerChip>().getOwner() == playerMoveState)
            {
                y++;
            }
            z--;
        }

        //if all block by main and side diagonal have one owner, he wins 
        if (y == boardSize)
        {
            Debug.Log("Winner: " + playerMoveState);
            gameover = true;
        }
    }

    //Check for gorizontal win rule
    void gorizontalChecker(int i)
    {
        int y = 0;
        
        for (int x = 0; x < boardSize; x++)
        {
            if (board[i, x].GetComponent<playerChip>().getOwner() == playerMoveState)
            {
                y++;
            }
        }
        //if all block by main and side diagonal have one owner, he wins 
        if (y == boardSize)
        {
            Debug.Log("Winner: " + playerMoveState);
            gameover = true;
        }
    }

    //Check for vertical win rule
    void verticalChecker(int j)
    {
        int y = 0;
        
        for (int x = 0; x < boardSize; x++)
        {
            
            if(board[x, j].GetComponent<playerChip>().getOwner() == playerMoveState)
            {
                y++;
            }
        }
        //if all block by main and side diagonal have one owner, he wins 
        if (y == boardSize)
        {
            Debug.Log("Winner: " + playerMoveState);
            gameover = true;
        }
            
    }
    
    //check for draw 
    void drawChecker()
    {
        int controlDraw = boardSize;
        for(int x = 0; x < boardSize; x++)
        {
            for(int y = 0; y < boardSize; y++)
            {
                if(board[x, y].GetComponent<playerChip>().getOwner() == -1)
                {
                    controlDraw--;
                }
            }
        }

        if(controlDraw == boardSize && gameover == false)
        {
            gameover = true;
            Debug.Log("Draw.");
        }
    }
    
    //events on board (any input can be there)
    void eventsOnBoard()
    {
        int i, j;

        //click on board
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null)
                {
                    //get row and column of clicked object
                    i = (int)hit.transform.gameObject.GetComponent<playerChip>().getI();
                    j = (int)hit.transform.gameObject.GetComponent<playerChip>().getJ();

                    this.setUpChip(i, j);
                    verticalChecker(j);
                    gorizontalChecker(i);
                    diagonalChecker();
                    drawChecker();
                    if (gameover == false)
                    {
                        setNextMove();
                    }
                }    
        }

        
        
    }

    //set up player chip
    //void 
    void setUpChip(int i, int j)
    {
        GameObject currentPlayerChip = board[i, j];
        int owner = currentPlayerChip.GetComponent<playerChip>().getOwner();

        switch (playerMoveState)
        {
            //noncross player
            case 0:
                {
                    if (owner == -1)
                    {
                        currentPlayerChip.GetComponent<playerChip>().setColor(Color.red);
                        currentPlayerChip.GetComponent<playerChip>().setOwner(0);
                    } else
                    {
                        Debug.Log("Already own by cross");
                    }
                    
                    
                    break;
                }
            //cross player
            case 1:
                {
                    if (owner == -1)
                    {
                        currentPlayerChip.GetComponent<playerChip>().setColor(Color.black);
                        currentPlayerChip.GetComponent<playerChip>().setOwner(1);
                    }
                    else
                    {
                        Debug.Log("Already own by noncross");
                    }

                    
                    break;
                }
        }
    }

    void setNextMove()
    {
        this.playerMoveState = this.playerMoveState == 0 ? 1 : 0;
    }
}
