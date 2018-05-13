using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    static readonly string hide = "<color=#0000>{0}</color>";

    [SerializeField] private Text lvlTxt, xpTxt;
    [SerializeField] private float rollTime = 1f;

    private string lvlTemplate, xpTemplate;

    private static int xp;
    private static Score instance;

	void Start () {
        lvlTemplate = Difficulty.IsTutorial ? hide : lvlTxt.text;
        lvlTxt.text = string.Format(lvlTemplate, Difficulty.CurrentLevel);

        xpTemplate = Difficulty.IsTutorial ? hide : xpTxt.text;
        xpTxt.text = string.Format(xpTemplate, xp);

        instance = this;
	}
	
	void Update () {
	    
	}
    
    private IEnumerator rollXP(int increment) {
        int a = xp,
            b = xp + increment;
        xp += increment;
        for (; a<=b; a +=(int) (increment * Time.deltaTime * rollTime)) {
            xpTxt.text = string.Format(xpTemplate, a);
            yield return new WaitForEndOfFrame();
        }
        xpTxt.text = string.Format(xpTemplate, b);
    }

    public static void Reset () {
        xp = 0;
    }


    public static void ComputeSuccess (out int completion, out int timeliness, out int health, out int stealth) {
        if (Difficulty.IsTutorial) {
            completion = timeliness = health = stealth = 0;

        } else {
            completion = 1000;
            timeliness = 2000 - (int)(2000 * Time.timeSinceLevelLoad / (120 + 10 * Difficulty.CurrentLevel));
            health = (int)(700 * Player.Health);
            stealth = Enemy.HasBeenChasing ? 0 : 350;
            
            if (timeliness < 0) timeliness = 0;

            xp += completion + timeliness + health + stealth;
        }
    }


    public static void ComputeFail(out int penalty) {
        penalty = Difficulty.IsTutorial ? 0 : -500;
        xp += penalty;
    }


    public static void KeyCollection() {
        instance.StartCoroutine(instance.rollXP(100));
    }
}
