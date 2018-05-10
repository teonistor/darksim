using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Ambiance : MonoBehaviour {
    private static readonly float minAngle = 20f,
                                  maxAngle = 80f;

    private static IList<Enemy> enemies;
    private static Light overheadLight;

    /// <summary>
    /// Number of attacking entities currently counting towards the red shade of the overhead light
    /// </summary>
    public static int AttackCount {
        get {
            return _attackCount;
        }
        set {
            _attackCount = value;
            colorT = 0.5f - Mathf.Pow(2f, -value - 1);
        }
    }

    public static float SpotAngle {
        get {
            return overheadLight.spotAngle;
        }
        set {
            overheadLight.spotAngle = value;
        }
    }

    private static float colorC, colorT;
    private static int _attackCount;

	void Start () {
        enemies = new List<Enemy>();
        overheadLight = GetComponent<Light>();
        AttackCount = 0;
        colorC = colorT = 0f;
	}

    void FixedUpdate () {
        colorC = Mathf.MoveTowards(colorC, colorT, 1.5f * Time.deltaTime);
        overheadLight.color = Color.Lerp(Color.white, Color.red, colorC);
        overheadLight.spotAngle = Mathf.Lerp(minAngle, maxAngle, Difficulty.CurrentDifficulty);
    }
}
