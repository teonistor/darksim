using UnityEngine.SceneManagement;
using UnityEngine;


public class Difficulty : MonoBehaviour {

    /* Difficulty tweak (by object collection):
     * - Stamina duration
     * - Overhead light angle
 
     * Increase by level progression:
     * - Map size (and thus minimum distance to enemies (?))
     * - Number of keys generated
     * - Number of keys necessary (tends towards equal with generated?)
     * - Number of enemies
     * - Enemy speed
     * - Enemy awareness params
 
     * When dying, slightly decrease:
     * - Enemy speed
     * - Enemy awareness params
     */
    
    private static bool DiffNeverCollected = true;
    private static bool KeyNeverCollected = true;
    private static int currentLevel;

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
    public static int CurrentLevel {
        get {
            return currentLevel;
        }
        private set {
            currentLevel = value;

            if (IsTutorial)
                switch (value) {
                    case 1: MapSize = 10; KeysGenerated = 0; KeysNecessary = 0; EnemiesGenerated = 0; PowerupsGenerated = 0; break;
                    case 2: MapSize = 15; KeysGenerated = 1; KeysNecessary = 1; EnemiesGenerated = 0; PowerupsGenerated = 0; break;
                    case 3: MapSize = 15; KeysGenerated = 2; KeysNecessary = 2; EnemiesGenerated = 5; PowerupsGenerated = 0; break;
                    case 4: MapSize = 20; KeysGenerated = 2; KeysNecessary = 2; EnemiesGenerated = 0; PowerupsGenerated = 1; break;
                    case 5: MapSize = 25; KeysGenerated = 3; KeysNecessary = 2; EnemiesGenerated = 1; PowerupsGenerated = 1; break;
                    default:
                        SceneManager.LoadSceneAsync(0);
                        break;
                }
            else {
                MapSize = (value + 6) * 5;
                KeysGenerated = EnemiesGenerated = (value + 7) / 2;
                KeysNecessary = value / 2 + 3;
                EnemiesGenerated = value / 2 + 1 + value / 5;
                PowerupsGenerated = (value + 6) / 4;
            }
        }
    }

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
    public static int MapSize { get; private set; }

    /// <summary>
    /// Number of enemies generated in the current level
    /// </summary>
    public static int EnemiesGenerated { get; private set; }

    /// <summary>
    /// Number of powerups generated in the current level
    /// </summary>
    public static int PowerupsGenerated { get; private set; }

    /// <summary>
    /// Number of keys generated in the current level
    /// </summary>
    public static int KeysGenerated { get; private set; }

    /// <summary>
    /// Number of keys necessary to open the door in the current level
    /// </summary>
    public static int KeysNecessary { get; private set; }

    /// <summary>
    /// Number of keys currently collected
    /// </summary>
    public static int KeysCollected { get; private set; }

    /// <summary>
    /// Whether the tutorial is currently in progress
    /// </summary>
    public static bool IsTutorial { get; private set; }

    /// <summary>
    /// Whether the last tutorial level is currently in progress
    /// </summary>
    public static bool IsLastTutorial { get { return IsTutorial && currentLevel == 5; } }


    static Difficulty () {
        CurrentLevel = MaxLevel = 1;
        CurrentDifficulty = TargetDifficulty = 0.5f;
    }

    void Update () {
        CurrentDifficulty = Mathf.MoveTowards(CurrentDifficulty, TargetDifficulty, Time.deltaTime * 0.15f);
    }

    /// <summary>
    /// Notify that a speed powerup was collected.
    /// </summary>
    public static void CollectSpeed () {
        CheckFirstDiffCollect();
        TargetDifficulty = Mathf.Clamp01(TargetDifficulty - DifficultyIncrement);
    }

    /// <summary>
    /// Notify that a light powerup was collected.
    /// </summary>
    public static void CollectLight () {
        CheckFirstDiffCollect();
        TargetDifficulty = Mathf.Clamp01(TargetDifficulty + DifficultyIncrement);
    }

    /// <summary>
    /// Notify that a key was collected.
    /// </summary>
    public static void CollectKey () {
        CheckFirstKeyCollect();
        KeysCollected++;
        if (KeysCollected == KeysNecessary) {
            Door.Open();
        }
    }

    /// <summary>
    /// Reset certain counters for beginning a game
    /// </summary>
    /// <param name="tutorial">Whether the tutorial is being begun rather than the actual game (default false)</param>
    public static void BeginGame (bool tutorial=false) {
        IsTutorial = tutorial;
        CurrentDifficulty = TargetDifficulty = 0.5f;
        KeysCollected = 0;
        CurrentLevel = 1;
    }

    /// <summary>
    /// Reset key counter when restarting a level
    /// </summary>
    public static void RetryLevel () {
        KeysCollected = 0;
    }

    /// <summary>
    /// Reset key counter and increment level count
    /// </summary>
    public static void NextLevel () {
        KeysCollected = 0;
        CurrentLevel++;
        MaxLevel = Mathf.Max(MaxLevel, CurrentLevel);
    }

    private static void CheckFirstDiffCollect () {
        if (DiffNeverCollected) {
            DiffNeverCollected = false;
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
