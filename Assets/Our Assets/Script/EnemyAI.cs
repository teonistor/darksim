using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour {

    private WorldGenerator worldGen;

    private int[,] world;

    private Vector2 location;
    void Start () {
        location = new Vector2();
	}

    void SetUp(Vector2 location, WorldGenerator worldGen) {
        this.location = location;
        this.worldGen = worldGen;
        world = worldGen.getWorld();
    }

    void Astar(Vector2 target) {
        
    }
    public void SetLocation(Vector2 newLoc) {
        location = newLoc;
    }
}
