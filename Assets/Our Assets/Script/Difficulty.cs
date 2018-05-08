using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour {

    /* What to change by difficulty:
     * - Stamina durations
     * - Enemy speed
     * - Overhead light angle
     * - Enemy visibility angle
 
     * What to change by level progression:
     * - Map size (and thus minimum distance to enemies)
     * - Number of keys
     * - Number of enemies
     * 
     */


	public static float difficulty { get; private set; }

    // TODO
    public static float staminaDrop { get { return 4f; } }
    public static float staminaRefillMoving { get { return 8.5f; } }
    public static float staminaRefillStaying { get { return 6.2f; } } // Quite arbitrary?

    // This is more about level progression
    public static int mapSize { get; private set; }
}
