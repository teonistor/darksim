using UnityEngine;

public class Player : MonoBehaviour {
    public enum Side { Up, Right, Down, Left };

    [SerializeField] private GameObject playerAvatar;
    [SerializeField] private float walkSpeed = 4.5f,
                                   runSpeed = 8f;

    public static Player Instance { get; private set; }
    public Actions PlayerActions { get; private set; }
    public static float Stamina { get; private set; }
    public static float Health { get; private set; }

    private int[] blockedMove = new int[] { 0, 0, 0, 0 };
    private float lastDamageTaken = 0f;
    private bool running = false;

    void Start () {
        Instance = this;
        PlayerActions = playerAvatar.GetComponent<Actions>();
        Stamina = 1f;
        Health = 1f;
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
                v *= runSpeed;
                PlayerActions.Run();
                Stamina -= Time.deltaTime / Difficulty.StaminaDrop;
            } else {
                v *= walkSpeed;
                PlayerActions.Walk();
                Stamina = Mathf.Min(Stamina + Time.deltaTime / Difficulty.StaminaRefillMoving, 1f);
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
            Stamina = Mathf.Min(Stamina + Time.deltaTime / Difficulty.StaminaRefillStaying, 1f);
        }

    }

    public void DealDamage () {
        if (Time.time - lastDamageTaken > 3f) {
            lastDamageTaken = Time.time;
            Health -= Difficulty.HealthDrop;
            if (Health < 0f) {
                enabled = false;
                PlayerActions.Death();
            }
        }
    }
}
