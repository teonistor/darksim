using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    static readonly string[] tutorialMessages = new string[] {
         "Walk out the door to complete a level\n<color=aqua>WASD</color> / <color=aqua>arrow keys</color> to move",
         "Collect Unity Logo keys to open the door\n<color=aqua>Tab</color> to pause at any time",
         "Avoid crawlers – they'll damage your health and kill you\nHold down <color=aqua>Shift</color> to sprint (consumes stamina)",
         "Collect Flashlight and Speed powerups to\nbalance light area versus stamina duration",
         "Sometimes not all keys on the map are\nnecessary to open the door" };


    [SerializeField] private float rollTime = 1f;

    private Text text;
    private string template;

    private static int xp;
    private static Score instance;

	void Start () {
        text = GetComponentInChildren<Text>();
        template = text.text;

        if (Difficulty.IsTutorial)
            text.text = tutorialMessages[Difficulty.CurrentLevel-1];
        else
            text.text= string.Format(template, Difficulty.CurrentLevel, xp);

        //lvlTemplate = Difficulty.IsTutorial ? hide : lvlTxt.text;
        //lvlTxt.text = string.Format(lvlTemplate, Difficulty.CurrentLevel);

        //xpTemplate = Difficulty.IsTutorial ? hide : xpTxt.text;
        //xpTxt.text = string.Format(xpTemplate, xp);

        instance = this;
	}
	
	void Update () {
	    
	}
    
    private IEnumerator rollXP(int increment) {
        int a = xp,
            b = xp + increment;
        xp += increment;
        for (; a<=b; a +=(int) (increment * Time.deltaTime * rollTime)) {
            text.text = string.Format(template, Difficulty.CurrentLevel, a);
            yield return new WaitForEndOfFrame();
        }
        text.text = string.Format(template, Difficulty.CurrentLevel, b);
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

            Enemy.BlindDistance += 4f * Player.Health;
            Enemy.DeafDistance += 0.5f * Player.Health;
            Enemy.VisionAngle += 3f * Player.Health;
            Enemy.RunSpeed += 0.2f * Player.Health;
        }
    }


    public static void ComputeFail(out int penalty) {
        penalty = Difficulty.IsTutorial ? 0 : -500;
        xp += penalty;
        if (xp < 0) {
            penalty -= xp;
            xp = 0;
        }

        Enemy.BlindDistance -= 0.7f;
        Enemy.DeafDistance -= 0.2f;
        Enemy.VisionAngle -= 1f;
        Enemy.RunSpeed -= 0.1f;
    }


    public static void KeyCollection() {
        if (!Difficulty.IsTutorial)
            instance.StartCoroutine(instance.rollXP(100));
    }
}
