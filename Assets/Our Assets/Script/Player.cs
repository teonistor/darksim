using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public enum Side { Up, Right, Down, Left };

    [SerializeField]
    private GameObject playerAvatar;
    [SerializeField]
    private GameObject lightObject;
    private Light light;
    [SerializeField]
    private float walkSpeed = 4.5f,
                  runSpeed = 8f;

    public static Player Instance { get; private set; }
    public Actions PlayerActions { get; private set; }
    public static float Stamina { get; private set; }
    public static float MaxStamina { get; private set; }
    public static float MaxLight { get; private set; }
    public static float Health { get; private set; }

    private int[] blockedMove = new int[] { 0, 0, 0, 0 };
    private float lastDamageTaken = 0f;
    private bool running = false;

    [SerializeField]
    private float tradeOffScore = 100; //100 - max lighting, 0 - max run stamina (2f)
    void Start () {
        Instance = this;
        PlayerActions = playerAvatar.GetComponent<Actions>();
        Stamina = 1f;
        MaxStamina = 1f;
        MaxLight = 80;
        Health = 1f;
        light = lightObject.GetComponent<Light>();
    }

    public int this[Side s] {
        get {
            return blockedMove[(int)s];
        } set {
            blockedMove[(int)s] = value;
        }
    }

    private bool blockedUp {
        get {
            return this[Side.Up] > 0;
        }
    }

    private bool blockedRight {
        get {
            return this[Side.Right] > 0;
        }
    }

    private bool blockedDown {
        get {
            return this[Side.Down] > 0;
        }
    }

    private bool blockedLeft {
        get {
            return this[Side.Left] > 0;
        }
    }


    void Update () {
        Vector3 v = new Vector3();

        // Get movement input from axes
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");

        // Block movement when hitting stuff
        if (blockedUp && v.z > 0f) v.z = 0f;
        if (blockedRight && v.x > 0f) v.x = 0f;
        if (blockedDown && v.z < 0f) v.z = 0f;
        if (blockedLeft && v.x < 0f) v.x = 0f;

        // Normalise (no diagonal speedup)
        v = Vector3.ClampMagnitude(v, 1f) * Time.deltaTime;

        // Run status based on run button and stamina
        if (Input.GetButtonDown("Run")) running = true;
        if (Input.GetButtonUp("Run") || Stamina <= 0f) running = false;

        if (v != Vector3.zero) {
            if (running) {
                tradeOffScore -= 10f * Time.deltaTime;
                if (tradeOffScore < 0) tradeOffScore = 0;
                MaxStamina = 1 + 1 / (tradeOffScore + 1);
                MaxLight = (tradeOffScore / 100) * 80 + 1;
                light.spotAngle = MaxLight;
                v *= runSpeed;
                PlayerActions.Run();
                Stamina -= Time.deltaTime / Difficulty.StaminaDrop;
            } else {
                if(Stamina == MaxStamina) {
                    tradeOffScore += 10f * Time.deltaTime;
                    if(tradeOffScore > 100) tradeOffScore = 100;
                }
                MaxStamina = 1 + 1 / (tradeOffScore + 1);
                MaxLight = (tradeOffScore / 100) * 80 + 1;
                v *= walkSpeed;
                PlayerActions.Walk();
                Stamina = Mathf.Min(Stamina + Time.deltaTime / Difficulty.StaminaRefillMoving, MaxStamina);
                light.spotAngle = MaxLight;
            }

            // Turn towards heading direction
            float a = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
            Vector3 r = playerAvatar.transform.localEulerAngles;
            r.y = Mathf.MoveTowardsAngle(r.y, a, 666 * Time.deltaTime);
            playerAvatar.transform.localEulerAngles = r;

            // Finally, move
            transform.position += v;

        } else {
            PlayerActions.Stay();
            MaxStamina = 1 + 1 / (tradeOffScore + 1);
            MaxLight = (tradeOffScore / 100) * 80 + 1;
            light.spotAngle = MaxLight;

            Stamina = Mathf.Min(Stamina + Time.deltaTime / Difficulty.StaminaRefillStaying, MaxStamina);
        }

    }

    public void DealDamage () {
        if (Health > 0f) {
            if (Time.time - lastDamageTaken > 1f) {
                lastDamageTaken = Time.time;
                Health -= Difficulty.HealthDrop;
                if (Health <= 0f) {
                    enabled = false;
                    PlayerActions.Death();
                    StartCoroutine(delayFailMenu());
                } else {
                    PlayerActions.Damage();
                }
            }
        }
    }

    private IEnumerator delayFailMenu () {
        yield return new WaitForSeconds(3f); // Duration of death animation
        WorldGenerator.FailCanvas.SetActive(true);
    }
}
