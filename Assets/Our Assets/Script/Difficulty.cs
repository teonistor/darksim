using System;
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

    private static bool firstRun = true;
    private static bool DiffNecerCollected = true;
    private static bool KeyNeverCollected = true;

    /// <summary>
    /// Current (potentially in progress) difficulty tweak, between 0 (speed) and 1 (light)
    /// </summary>
    public static float CurrentDifficulty { get; private set; }

    /// <summary>
    /// Target difficulty tweak, between 0 (speed) and 1 (light)
    /// </summary>
	public static float TargetDifficulty { get; private set; }

    /// <summary>
    /// Current level
    /// </summary>
    public static int CurrentLevel { get; private set; }

    /// <summary>
    /// Maximum level reached in the current run of the program
    /// </summary>
    public static int MaxLevel { get; private set; }

    /// <summary>
    /// Increment by which difficulty tweak is adjusted when speed/light is collected
    /// </summary>
    public static float DifficultyIncrement { get { return 0.1f; } }

    /// <summary>
    /// Stamina refill speed when staying still, based on current difficulty tweak
    /// </summary>
    public static float StaminaDrop { get { return Mathf.Lerp(6f, 2f, CurrentDifficulty); } }

    /// <summary>
    /// Stamina refill speed when moving, based on current difficulty tweak
    /// </summary>
    public static float StaminaRefillMoving { get { return Mathf.Lerp(9f, 7f, CurrentDifficulty); } }

    /// <summary>
    /// Stamina refill speed when staying still, based on current difficulty tweak
    /// </summary>
    public static float StaminaRefillStaying { get { return Mathf.Lerp(6.8f, 5.6f, CurrentDifficulty); } }

    /// <summary>
    /// Amount of health lost for one enemy hit
    /// </summary>
    public static float HealthDrop { get { return 0.4f; } }

    /// <summary>
    /// Size of the map for the current level
    /// </summary>
    public static int MapSize { get { return 50; } } // TODO

    /// <summary>
    /// Number of enemies generated in the current level
    /// </summary>
    public static int EnemiesGenerated { get { return 3; } } // TODO

    /// <summary>
    /// Number of keys generated in the current level
    /// </summary>
    public static int KeysGenerated{ get { return 3; } } // TODO

    /// <summary>
    /// Number of keys necessary to open the door in the current level
    /// </summary>
    public static int KeysNecessary { get { return 2; } } // TODO

    /// <summary>
    /// Number of keys currently collected
    /// </summary>
    public static int KeysCollected { get; private set; }

    void Awake () {
        if (firstRun) {
            CurrentDifficulty = TargetDifficulty = 0.5f;
            CurrentLevel = MaxLevel = 0;
        }
    }

    void Update () {
        CurrentDifficulty = Mathf.MoveTowards(CurrentDifficulty, TargetDifficulty, Time.deltaTime * 0.15f);
    }


    public static void CollectSpeed () {
        CheckFirstDiffCollect();
        TargetDifficulty = Mathf.Clamp01(TargetDifficulty - DifficultyIncrement);
    }

    public static void CollectLight () {
        CheckFirstDiffCollect();
        TargetDifficulty = Mathf.Clamp01(TargetDifficulty + DifficultyIncrement);
    }

    public static void CollectKey() {
        CheckFirstKeyCollect();
        KeysCollected++;
        if (KeysCollected == KeysNecessary) {
            Door.Open();
        }
    }

    public static void BeginGame () {
        KeysCollected = 0;
        CurrentLevel = 1;
    }

    public static void RetryLevel () {
        KeysCollected = 0;
    }

    public static void NextLevel () {
        KeysCollected = 0;
        CurrentLevel++;
        MaxLevel = Mathf.Max(MaxLevel, CurrentLevel);
    }

    private static void CheckFirstDiffCollect () {
        if (DiffNecerCollected) {
            DiffNecerCollected = false;
            print("First colect difficulty token");
            // TODO
        }
    }
    private static void CheckFirstKeyCollect () {
        if (KeyNeverCollected) {
            KeyNeverCollected = false;
            print("First colect key collectible");
            // TODO
        }
    }
}
