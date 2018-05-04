using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject wall;
    // Use this for initialization

    // matrix used for world generation - origin is at top-left.
    private int[,] world;

    //height and width of the world --- if not specified in the editor, it has a default value of 50
    [SerializeField]
    private int height = 50;
    [SerializeField]
    private int width = 50;

    //variable used for player's position: x is the position on 'width' and y is the position on 'height'
    private Vector2 playerPosition;

    [SerializeField]
    private GameObject player;
    void Start() {
        world = new int[height, width];

        //setting player postion to the middle of the map
        playerPosition.x = (int)width / 2;
        playerPosition.y = (int)height / 2;

        player.transform.position = new Vector3(playerPosition.x, player.transform.position.y, playerPosition.y);

        //executing function to add walls around the map
        pad();

        //executing function to generate the map
        generateRandomWorld();

        //executing function to draw the map
        drawMap();
    }

    /// <summary>
    /// Function padding (adding walls) at the edges of the world.
    /// </summary>
    void pad() {
        for (int i = 0; i < height; ++i) {
            world[i, width - 1] = world[i, 0] = 1;
        }
        for (int j = 0; j < width; ++j) {
            world[0, j] = world[height - 1, j] = 1;
        }
    }

    /// <summary>
    /// Function generating the world.
    /// </summary>
    void generateRandomWorld() {
        System.Random generator = new System.Random();
        int chance = 10;
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                if (generator.Next(0, 100) < chance && (i != playerPosition.x || j != playerPosition.y)) {
                    world[i, j] = 1;
                }
            }
        }
    }

    /// <summary>
    /// Function that draws the world.
    /// </summary>
    void drawMap() {
        //double for loop that iterates through the map 
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                /*
                 * Values of the world map:
                 * 
                 * 0 -> empty space
                 * 1 -> wall
                 * 2 -> special object
                 */

                if (world[i, j] == 1) {
                    Instantiate(wall, new Vector3(i, wall.transform.localScale.y / 2, j), Quaternion.identity);
                }
            }
        }
    }
}
