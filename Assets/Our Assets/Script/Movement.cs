using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public enum Side { Up, Right, Down, Left };

    [SerializeField] private GameObject playerAvatar;
    [SerializeField] private float walkSpeed = 4.5f,
                                   runSpeed = 8f;

    private Actions playerActions;
    private int[] blockedMove = new int[] { 0, 0, 0, 0 };
    public static float stamina { get; private set; }
    private bool running = false;

    void Start () {
        playerActions = playerAvatar.GetComponent<Actions>();
        stamina = 1f;
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
        if (Input.GetButtonUp("Run") || stamina <= 0f) running = false;

        if (v != Vector3.zero) {
            if (running) {
                v *= runSpeed;
                playerActions.Run();
                stamina -= Time.deltaTime / Difficulty.staminaDrop;
            } else {
                v *= walkSpeed;
                playerActions.Walk();
                stamina = Mathf.Min(stamina + Time.deltaTime / Difficulty.staminaRefillMoving, 1f);
            }

            // Turn towards heading direction
            float a = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
            Vector3 r = playerAvatar.transform.localEulerAngles;
            r.y = Mathf.MoveTowardsAngle(r.y, a, 666 * Time.deltaTime);
            playerAvatar.transform.localEulerAngles = r;

            // Finally, move
            transform.position += v;

        } else {
            playerActions.Stay();
            stamina = Mathf.Min(stamina + Time.deltaTime / Difficulty.staminaRefillStaying, 1f);
        }
    }
}
