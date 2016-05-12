using UnityEngine;
using System.Collections;

public class GridMaster : MonoBehaviour {
   
    private float squarewidth = 1f;
    private float squarelength = 1f;
    private float topleftx = -3f;
    private float toplefty = 3f;
    private Vector3[,] grid = new Vector3[10, 10];
    public GameObject cube;

    // Use this for initialization
    void Start () {


        //A simple grid where each cell only knows its transform
        //To generate it you need to know the width and height of each cell
        //and the x and y position of the middle of the top left cell
        //[0,0][1,0][2,0][3,0]
        //[0,1][1,1][2,1][3,1]   Example of how grid is numbered
        //[0,3][1,3][2,3][3,3]

        for (float width = 0; width <= 6; width++)
        {
            for (float height = 0; height <= 6; height++)
            {
                grid[(int) width, (int) height] = new Vector3(topleftx + (width * squarewidth), toplefty - (height * squarelength), 0f);
            }
        }
        
    }

 
	// Update is called once per frame
	void Update () {
        
    }

    //Lets other classes get the position of cell on grid
    public Vector3 getPosition(int xpos, int ypos)
    {
        return grid[xpos, ypos];
    }
 
}
