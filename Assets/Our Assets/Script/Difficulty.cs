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
    public static float StaminaDrop { get { return 4f; } }
    public static float StaminaRefillMoving { get { return 8.5f; } }
    public static float StaminaRefillStaying { get { return 6.2f; } } // Quite arbitrary?
    public static float HealthDrop { get { return 0.4f; } }

    // This is more about level progression
    public static int MapSize { get; private set; }
}
