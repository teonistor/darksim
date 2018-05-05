using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour {

    private WorldGenerator worldGen;
    private Rigidbody rb;

    [SerializeField]
    private float speed = 3;
    private Point nextPoint;
    private int[,] world;
    private int[,] checkWorld;

    private static int[] xchanges = new int[] { -1, 0, 1, 0 };
    private static int[] ychanges = new int[] { 0, -1, 0, 1 };

    private static int[] xspecial = new int[] { -1, 1, -1, 1 };
    private static int[] yspecial = new int[] { -1, -1, 1, 1 };

    private static int dim = xchanges.Length;
    private static int sdim = xspecial.Length;

    private Vector2Int location;
    private Vector2 playerPos;

    private List<Point> path;
    private int pathIndex;
    private bool moving = false;
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        location = new Vector2Int();
	}

    public float computeEuclid2Dist(Vector2 start, Vector2 end) {
        return (start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y);
    }
    public float computeEuclid2Dist(Point start, Vector2 end) {
        return (start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y);
    }

    public void SetUp(Vector2 location, WorldGenerator worldGen) {
        this.location = new Vector2Int((int)location.x,(int)location.y);
        this.worldGen = worldGen;
        world = worldGen.getWorld();
        Astar();
    }

    void Astar() {
        playerPos = worldGen.GetPlayerLoc();
        PriorityQueue<Point> pq = new PriorityQueue<Point>();
        pq.Enqueue(new Point(location.x, location.y, computeEuclid2Dist(location, playerPos), new Point(location.x, location.y,0)));
        checkWorld = new int[worldGen.GetWidth(), worldGen.GetHeight()];
        Point current;
        print(playerPos);
        while (pq.Count != 0) {
            current = pq.Dequeue();

            if (checkWorld[current.x, current.y] != 0) {
                continue;
            }

            checkWorld[current.x, current.y] = 1;

            for (int i = 0; i < dim; ++i) {
                int nextx = current.x + xchanges[i];
                int nexty = current.y + ychanges[i];
                if (nextx >= 0 && nextx < worldGen.GetHeight() && nexty >= 0 && nexty < worldGen.GetWidth()
                    && world[nextx, nexty] != worldGen.GetWallCode() 
                    && (current.orig.x != nextx || current.orig.y != nexty)) {
                    pq.Enqueue(new Point(nextx, nexty, computeEuclid2Dist(new Vector2Int(nextx, nexty), playerPos), current));
                }
            }

            for (int i = 0; i < sdim; ++i) {
                int nextx = current.x + xspecial[i];
                int nexty = current.y + yspecial[i];
                int condx1 = current.x;
                int condx2 = current.x + xspecial[i];
                int condy1 = current.y;
                int condy2 = current.y + yspecial[i];
                if (nextx >= 0 && nextx < worldGen.GetHeight() && nexty >= 0 && nexty < worldGen.GetWidth()
                    && world[nextx, nexty] != worldGen.GetWallCode()
                    && (current.orig.x != nextx || current.orig.y != nexty)
                    && (world[condx1,condy1] != worldGen.GetWallCode() && world[condx2,condy2] != worldGen.GetWallCode())) {
                    pq.Enqueue(new Point(nextx, nexty, computeEuclid2Dist(new Vector2Int(nextx, nexty), playerPos), current));
                }
            }

            if (current.h < 1) {
                path = new List<Point>();
                while(current != null) {
                    path.Add(current);
                    current = current.orig;
                }
                pathIndex = path.Count-1;
            }
        }
        
    }

    private Vector3 PointToPosition(Point p) {
        return new Vector3(p.x,1,p.y);
    }
    private bool IsThere(Point p) {
        Vector3 targetLoc = PointToPosition(p);
        if (Mathf.Abs(targetLoc.x - transform.position.x) > 0.1 || Mathf.Abs(targetLoc.z - transform.position.z) > 0.1)
            return false;
        return true;
    }

    private void FixedUpdate() {
        /*if(moving == true && nextPoint != null) {
            moving = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, PointToPosition(nextPoint), step);
            nextPoint = null;

        } else {
            if(path.Count > 0) {
                moving = true;
                nextPoint = path[pathIndex];
            }
        }

        if (nextPoint != null && IsThere(nextPoint) && pathIndex >= 1) {
            print(path.Count);
            Astar();
            if (pathIndex > 2)
                pathIndex -= 2;
            else if (pathIndex > 1)
                pathIndex--;
            nextPoint = path[pathIndex];
        }*/
    }

    public void SetLocation(Vector2Int newLoc) {
        location = newLoc;
    }
}
