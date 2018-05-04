using UnityEngine;
using System.Collections.Generic;
public class WorldGenerator : MonoBehaviour {

    //prefabs for wall and enemy
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject enemy;

    //gameobject of the player
    [SerializeField]
    private GameObject player;

    // matrix used for world generation - origin is at top-left.
    private int[,] world;

    //height and width of the world --- if not specified in the editor, it has a default value of 50
    [SerializeField]
    private int height = 50;
    [SerializeField]
    private int width = 50;

    //variable used for player's position: x is the position on 'width' and y is the position on 'height'
    private Vector2 playerPosition;

    //distance between enemy and player 
    [SerializeField]
    private int dist = 30;
    //number of enemies 
    [SerializeField]
    private int numberOfEnemies = 1;

    //Codes used in generating the map
    public const int wallCode = 1;
    public const int objectCode = 2;
    public const int enemyCode = 3;


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

        //executing function to generate enemies;
        generateEnemies(numberOfEnemies,dist);

        //executing function to draw the map
        drawMap();
    }

    /// <summary>
    /// Function padding (adding walls) at the edges of the world.
    /// </summary>
    void pad() {
        for (int i = 0; i < height; ++i) {
            world[i, width - 1] = world[i, 0] = wallCode;
        }
        for (int j = 0; j < width; ++j) {
            world[0, j] = world[height - 1, j] = wallCode;
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
    /// bfs function that generate the enemies.
    /// </summary>
    /// <param name="numberOfEnemies"> integer indicating the number of enemies</param>
    /// <param name="distance"> integer indicating the minimum distance between the enemies and the player</param>
    void generateEnemies(int numberOfEnemies, int distance) {

        int[,] mapChecker = new int[height, width]; 

        //queue elements are vector3, with x,y the coordinates and z the distance between the given point and the player
        //queue used for bfs

        Queue<Vector3> qu = new Queue<Vector3>();
        //adding the player position to the queue
        qu.Enqueue(new Vector3(playerPosition.x, playerPosition.y, 1));

        //list containing the possible enemy locations
        List<Vector2> possibleEnemyLoc = new List<Vector2>();

        //bfs
        while(qu.Count != 0) {
            Vector3 currentPos = qu.Dequeue();
            
            //checking if current position was visited
            if(mapChecker[(int)currentPos.x, (int)currentPos.y] != 0)
                continue;
            //checking if current position is empty
            if (world[(int)currentPos.x, (int)currentPos.y] != 0)
                continue;

            mapChecker[(int)currentPos.x, (int)currentPos.y] = (int)currentPos.z;

            qu.Enqueue(currentPos + new Vector3(1, 0, 1));
            qu.Enqueue(currentPos + new Vector3(0, 1, 1));
            qu.Enqueue(currentPos + new Vector3(-1, 0, 1));
            qu.Enqueue(currentPos + new Vector3(0, -1, 1));

            if(mapChecker[(int)currentPos.x, (int)currentPos.y] > distance) {
                possibleEnemyLoc.Add(new Vector2(currentPos.x, currentPos.y));
            }
        }

        System.Random rnd = new System.Random();
        while(numberOfEnemies > 0) {
            int nextEnemyIndex = rnd.Next(0, possibleEnemyLoc.Count);
            Vector2 nextEnemy = possibleEnemyLoc[nextEnemyIndex];
            possibleEnemyLoc.RemoveAt(nextEnemyIndex);
            world[(int)nextEnemy.x, (int)nextEnemy.y] = enemyCode;
            numberOfEnemies--;
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
                 * 3 -> enemy
                 */

                if (world[i, j] == wallCode) {
                    Instantiate(wall, new Vector3(i, wall.transform.localScale.y / 2, j), Quaternion.identity, transform);
                }else if (world[i, j] == enemyCode) {
                    GameObject newEnemy = Instantiate(enemy, new Vector3(i, enemy.transform.localScale.y / 2, j), Quaternion.identity);
                    newEnemy.GetComponent<EnemyAI>().SetLocation(new Vector2(i,j));
                }
            }
        }
    }

    public int[,] getWorld() {
        return world;
    }
}
