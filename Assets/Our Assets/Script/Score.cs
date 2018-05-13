using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    [SerializeField] private Text lvlTxt, xpTxt;
    [SerializeField] private float rollTime = 1f;

    private string lvlTemplate = "Level {0}",
                   xpTemplate = "{0} XP";

    private static int xp;

    private static Score instance;

	void Start () {
        lvlTxt.text = string.Format(lvlTemplate, Difficulty.CurrentLevel);
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


    public static void ComputeSuccess () {
        // Player.Health...
        // some time measure
        // perfect stealth bonus ?

        // Handle end display
    }


    public static void ComputeFail() {
        // -1000 or whatever

        // Handle end display
    }


    public static void KeyCollection() {
        instance.StartCoroutine(instance.rollXP(100));
    }
}
