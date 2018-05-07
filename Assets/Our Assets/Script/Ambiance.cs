using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Ambiance : MonoBehaviour {

    private static IList<EnemyNew> enemies;
    private static Light overheadLight;
    public static int attackCount {
        get {
            return _attackCount;
        }
        set {
            _attackCount = value;
            targetT = 0.5f - Mathf.Pow(2f, -value - 1);
        }
    }

    private static float t, targetT;
    private static int _attackCount;

	void Start () {
        enemies = new List<EnemyNew>();
        overheadLight = GetComponent<Light>();
        attackCount = 0;
        t = targetT = 0f;
	}

    void FixedUpdate () {
        t = Mathf.MoveTowards(t, targetT, 1.5f * Time.deltaTime);
        overheadLight.color = Color.Lerp(Color.white, Color.red, t);
    }
}
