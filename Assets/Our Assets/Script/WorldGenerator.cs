using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class WorldGenerator : MonoBehaviour {

    //prefabs for wall and enemy
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject exit;

    //gameobject of the player
    [SerializeField]
    private Player player;

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
    [SerializeField]
    private int numberOfObjects = 3;

    [SerializeField] private LocalNavMeshBuilder navMeshBuilder;
    [SerializeField] private GameObject collectible, indicator;
    private static GameObject indicatorS; // Hack
    private static Player playerS; // Hack

    // Menu canvass & static getters
    [SerializeField] private GameObject pauseCanvas, successCanvas, failCanvas;
    public static GameObject PauseCanvas { get; private set; }
    public static GameObject SuccessCanvas { get; private set; }
    public static GameObject FailCanvas { get; private set; }

    // Variable for exit side
    public int exitSide;

    //Codes used in generating the map
    public const int wallCode = 1;
    public const int objectCode = 2;
    public const int enemyCode = 3;
    public const int exitCode = 4;

    //List with possible locations on the map
    private static List<Vector2> possibleLoc;
    private static List<Vector2> possibleObjectLoc;
    void Start() {
        indicatorS = indicator;
        playerS = player;
        possibleLoc = new List<Vector2>();
	    possibleObjectLoc = new List<Vector2>();
	    PauseCanvas = pauseCanvas;
        SuccessCanvas = successCanvas;
        FailCanvas = failCanvas;
        world = new int[height, width];
        //setting player postion to the middle of the map
        playerPosition.x = (int)width / 2;
        playerPosition.y = (int)height / 2;

        player.transform.position = new Vector3(playerPosition.x, player.transform.position.y, playerPosition.y);



        //executing function to generate the map
        generateRandomWorld();

        //executing function to add walls around the map
        pad();

        //makes the exit of the level
        makeExit();

        //executing function to generate enemies;
        generateEnemies(numberOfEnemies,dist);

        //executing function to draw the map
        drawMap();
        navMeshBuilder.UpdateNavMesh();
    }

    /// <summary>
    /// Function padding (adding walls) at the edges of the world.
    /// </summary>
    void pad() {
        for (int i = 0; i < height; ++i) {
            world[i, width - 1] = world[i, 0] = 0;
        }
        for (int j = 0; j < width; ++j) {
            world[0, j] = world[height - 1, j] = 0;
        }

    }

    void makeExit() {
        int where = Random.Range(0, 4);
        switch (where) {
            case 0: world[Random.Range(0, height), 0] = exitCode; break;
            case 1: world[0, Random.Range(0, width)] = exitCode; break;
            case 2: world[Random.Range(0, height), width - 1] = exitCode; break;
            case 3: world[height - 1, Random.Range(0, width)] = exitCode; break;
        }
        exitSide = where;
    }

    /// <summary>
    /// Function generating the world.
    /// </summary>
    void generateRandomWorld() {
        int[,] roomMap = new int[height, width];
        System.Random generator = new System.Random();
        int startingRoomDim = generator.Next(4,6);
        if (startingRoomDim >= width / 2 || startingRoomDim >= height / 2) {
            return;
        }
        for (int i = height / 2 - startingRoomDim; i <= height / 2 + startingRoomDim; ++i) {
            for (int j = width / 2 - startingRoomDim; j <= width / 2 + startingRoomDim; ++j) {
                world[i, j] = 0;
                roomMap[i, j] = 1;
            }
            world[i, width / 2 - startingRoomDim] = world[i, width / 2 + startingRoomDim] = 1;
        }

        for (int j = width / 2 - startingRoomDim; j <= width / 2 + startingRoomDim; ++j) {
            world[height / 2 - startingRoomDim, j] = world[height / 2 + startingRoomDim, j] = 1;
        }

        int coridors = generator.Next(2, 6);

        while (coridors-- > 0) {
            int nextCoridor = generator.Next(1, startingRoomDim * 8 - 2);
            //print(nextCoridor);

            if (nextCoridor % (startingRoomDim * 2) == startingRoomDim - 1) {
                nextCoridor++;
            }
            if (nextCoridor % (startingRoomDim * 2) == 0) {
                nextCoridor++;
            }
            if (nextCoridor % (startingRoomDim * 2) == 1) {
                nextCoridor++;
            }
           // print(nextCoridor);

            if (nextCoridor < startingRoomDim * 2) {
                world[height / 2 - startingRoomDim, width / 2 + nextCoridor - startingRoomDim] = 0;
                nextCoridor--;
                world[height / 2 - startingRoomDim, width / 2 + nextCoridor - startingRoomDim] = 0;

            } else if (nextCoridor < startingRoomDim * 4) {
                nextCoridor -= startingRoomDim * 2;
                world[height / 2 + nextCoridor - startingRoomDim, width / 2 + startingRoomDim] = 0;
                nextCoridor--;
                world[height / 2 + nextCoridor - startingRoomDim, width / 2 + startingRoomDim] = 0;

            } else if (nextCoridor < startingRoomDim * 6) {
                nextCoridor -= startingRoomDim * 4;
                world[height / 2 + startingRoomDim, width / 2 + nextCoridor - startingRoomDim] = 0;
                nextCoridor--;
                world[height / 2 + startingRoomDim, width / 2 + nextCoridor - startingRoomDim] = 0;

            } else if (nextCoridor < startingRoomDim * 8) {
                nextCoridor -= startingRoomDim * 6;
                world[height / 2 + nextCoridor - startingRoomDim, width / 2 - startingRoomDim] = 0;
                nextCoridor--;
                world[height / 2 + nextCoridor - startingRoomDim, width / 2 - startingRoomDim] = 0;

            }
        }
        bool topCorridorStarted = false;
        bool bottomCorridorStarted = false;
        int topStart = 0;
        int topFinish;
        int botStart = 0;
        int botFinish;
        for (int i = height / 2 - startingRoomDim; i <= height / 2 + startingRoomDim; ++i) {
            if(world[i, width / 2 - startingRoomDim] == 0 && !bottomCorridorStarted) {
                //print("found");
                bottomCorridorStarted = true;
                botStart = i-1;
                //print(topStart);
            }else if (world[i, width / 2 - startingRoomDim] == 1 && bottomCorridorStarted) {
                bottomCorridorStarted = false;
                botFinish = i;
                for(int k = width / 2 - startingRoomDim; k > 0; --k) {
                    world[botFinish, k] = world[botStart, k] = 1;
                    for(int q = botStart+1; q < botFinish; ++q) {
                        roomMap[q, k] = 1; 
                    }
                }
            }
            if (world[i, width / 2 + startingRoomDim] == 0 && !topCorridorStarted) {
                //print("found");
                topCorridorStarted = true;
                topStart = i - 1;
                //print(topStart);
            } else if (world[i, width / 2 + startingRoomDim] == 1 && topCorridorStarted) {
                topCorridorStarted = false;
                topFinish = i;
                for (int k = width / 2 + startingRoomDim; k < width; ++k) {
                    world[topFinish, k] = world[topStart, k] = 1;
                    for (int q = topFinish - 1; q > topStart; --q) {
                        roomMap[q, k] = 1;
                    }
                }
            }
        }

        bool leftCorridorStarted = false;
        bool rightCorridorStarted = false;
        int leftStart = 0;
        int leftFinish;
        int rightStart = 0;
        int rightFinish;
        for (int i = width / 2 - startingRoomDim; i <= width / 2 + startingRoomDim; ++i) {
            if (world[height / 2 - startingRoomDim, i] == 0 && !leftCorridorStarted) {
                leftCorridorStarted = true;
                leftStart = i - 1;
            } else if (world[height / 2 - startingRoomDim, i] == 1 && leftCorridorStarted) {
                leftCorridorStarted = false;
                leftFinish = i;
                for (int k = height/ 2 - startingRoomDim; k > 0; --k) {
                    world[k, leftStart] = world[k, leftFinish] = 1;
                    for (int q = leftStart + 1; q < leftFinish; ++q) {
                        roomMap[k, q] = 1;
                    }
                }
            }
            if (world[height / 2 + startingRoomDim, i] == 0 && !rightCorridorStarted) {
                rightCorridorStarted = true;
                rightStart = i - 1;
            } else if (world[height / 2 + startingRoomDim, i] == 1 && rightCorridorStarted) {
                rightCorridorStarted = false;
                rightFinish = i;
                for (int k = height / 2 + startingRoomDim; k < height; ++k) {
                    world[k, rightStart] = world[k, rightFinish] = 1;
                    for (int q = rightStart + 1; q < rightFinish; ++q) {
                        roomMap[k, q] = 1;
                    }
                }
            }
        }
        
        for(int i = 0; i < height; ++i) {
            for(int j = 0; j < width; ++j) {
                if (roomMap[i,j] == 0) {
                    //generate room
                    int roomId = generator.Next(0, RoomData.rooms.Length);
                    for (int k = 0; k < RoomData.rooms[roomId].Length; ++k) {
                        for (int r = 0; r < RoomData.rooms[roomId][k].Length; ++r) {
                            if(i+k < height && j+r < width && roomMap[i+k,j+r] == 0) {
                                world[i + k, j + r] = RoomData.rooms[roomId][k][r];
                                roomMap[i + k, j + r] = 1;
                                //print(RoomData.rooms[roomId][k][r]);
                            }
                        }
                    }
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
            if(currentPos.x < 0 || currentPos.y < 0 || currentPos.x >= height || currentPos.y >= width || mapChecker[(int)currentPos.x, (int)currentPos.y] != 0)
                continue;

            //checking if current position is empty
            if (world[(int)currentPos.x, (int)currentPos.y] == wallCode)
                continue;

            mapChecker[(int)currentPos.x, (int)currentPos.y] = (int)currentPos.z;

            qu.Enqueue(currentPos + new Vector3(1, 0, 1));
            qu.Enqueue(currentPos + new Vector3(0, 1, 1));
            qu.Enqueue(currentPos + new Vector3(-1, 0, 1));
            qu.Enqueue(currentPos + new Vector3(0, -1, 1));

            if (world[(int)currentPos.x, (int)currentPos.y] == objectCode) {
                possibleObjectLoc.Add(new Vector2(currentPos.x, currentPos.y));
                world[(int)currentPos.x, (int)currentPos.y] = 0;
            }else if (world[(int)currentPos.x, (int)currentPos.y] == 0 && mapChecker[(int)currentPos.x, (int)currentPos.y] > distance) {
                possibleEnemyLoc.Add(new Vector2(currentPos.x, currentPos.y));
                possibleLoc.Add(new Vector2(currentPos.x, currentPos.y));
            } else if (world[(int)currentPos.x, (int)currentPos.y] == 0 && mapChecker[(int)currentPos.x, (int)currentPos.y] > 0) {
                possibleLoc.Add(new Vector2(currentPos.x, currentPos.y));
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
        
        while(numberOfObjects > 0) {
            if (possibleObjectLoc.Count == 0)
                break;
            int nextObjectIndex = rnd.Next(0, possibleObjectLoc.Count);
            Vector2 nextObject = possibleObjectLoc[nextObjectIndex];
            possibleObjectLoc.RemoveAt(nextObjectIndex);
            world[(int)nextObject.x, (int)nextObject.y] = objectCode;
            numberOfObjects--;
        }
        //Filling blank spots that can't be reached

        for(int i = 0; i < height; ++i) {
            for(int j = 0; j < width; ++j) {
                if (mapChecker[i, j] == 0 && world[i, j] == 0)
                    world[i, j] = 1;
            }
        }
    }
    /// <summary>
    /// Function that draws the world.
    /// </summary>
    void drawMap() {
        int extrapad = 20;
        Vector2 extraExitSpace = new Vector2(0,0);
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
                    newEnemy.GetComponent<Enemy>().Init(player);
                }else if (world[i, j] == objectCode) {
                    Instantiate(collectible, new Vector3(i, collectible.transform.localScale.y / 2, j), Quaternion.identity, transform);
                }else if (world[i,j] == exitCode) {
                    float iExit = 0, jExit = 0;
                    Quaternion rot = Quaternion.identity;
                    switch (exitSide) { 
                        case 0: iExit = i; jExit = j-0.5f;
                                extraExitSpace = new Vector2(i, j - 1);
                                rot = Quaternion.Euler(-90, 0, 0);
                                break;

                        case 1: iExit = i-0.5f; jExit = j;
                                extraExitSpace = new Vector2(i - 1, j);
                                rot = Quaternion.Euler(-90, 90, 0);
                                break;

                        case 2: iExit = i; jExit = j+0.5f;
                                extraExitSpace = new Vector2(i, j + 1);
                                rot = Quaternion.Euler(-90, 180, 0);
                                break;

                        case 3: iExit = i+0.5f; jExit = j;
                                extraExitSpace = new Vector2(i + 1, j);
                                rot = Quaternion.Euler(-90, 270, 0);
                                break;
                    }
                    Instantiate(exit, new Vector3(iExit, exit.transform.localScale.y / 2, jExit), rot, transform);
                }
            }
        }
        //add more pad
        for (int i = -extrapad; i < 0; ++i) {
            for (int j = 0; j < width; ++j) {
                if( i != extraExitSpace.x || j != extraExitSpace.y) 
                    Instantiate(wall, new Vector3(i, wall.transform.localScale.y / 2, j), Quaternion.identity, transform);
            }
        }
        for (int i = height; i < height+extrapad; ++i) {
            for (int j = 0; j < width; ++j) {
                if (i != extraExitSpace.x || j != extraExitSpace.y)
                    Instantiate(wall, new Vector3(i, wall.transform.localScale.y / 2, j), Quaternion.identity, transform);
            }
        }
        for (int i = -extrapad; i < height+extrapad; ++i) {
            for (int j = -extrapad; j < 0; ++j) {
                if (i != extraExitSpace.x || j != extraExitSpace.y)
                    Instantiate(wall, new Vector3(i, wall.transform.localScale.y / 2, j), Quaternion.identity, transform);
            }
        }
        for (int i = -extrapad; i < height + extrapad; ++i) {
            for (int j = width; j < width + extrapad; ++j) {
                if (i != extraExitSpace.x || j != extraExitSpace.y)
                    Instantiate(wall, new Vector3(i, wall.transform.localScale.y / 2, j), Quaternion.identity, transform);
            }
        }
    }

    /// <summary>
    /// A random reachable location on the generated map
    /// </summary>
    public static Vector3 randomLoc { get {
        Vector3 loc = possibleLoc[Random.Range(0, possibleLoc.Count)];
        loc.z = loc.y;
        loc.y = 0.5f;
        return loc;
    }}

    public int[,] getWorld() {
        return world;
    }

    public Vector2 GetPlayerLoc() {
        return playerPosition;
    }

    public int GetWallCode() {
        return wallCode;
    }

    public int GetWidth() {
        return width;
    }
    public int GetHeight() {
        return height;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount < 2)
            pauseCanvas.SetActive(true);
    }

    public static TargetIndicator CreateTargetIndicator(Collect target) {
        TargetIndicator indicator = Instantiate(indicatorS).GetComponent<TargetIndicator>();
        indicator.Init(target.transform, Camera.main, playerS.transform);
        return indicator;
    }
}
