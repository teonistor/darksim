// MoveTo.cs
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
    private static float epsilonTarget, epsilonHit, blindDistance, deafDistance, visionAngle, runSpeed, crawlSpeed;

    [SerializeField] private GameObject indicatorPrefab;

    private NavMeshAgent agent;
    private Vector3 initialPos;
    private Animator anim;
    private AudioSource audio;

    public bool IsChasing { get; private set; }
    public static bool HasBeenChasing { get; private set; }

    public static float BlindDistance {
        get {
            return blindDistance;
        } set {
            blindDistance = Mathf.Clamp(value, 9f, 50f);
        }
    }

    public static float DeafDistance {
        get {
            return deafDistance;
        } set {
            deafDistance = Mathf.Clamp(value, 2f, 7f);
        }
    }

    public static float VisionAngle {
        get {
            return visionAngle;
        }  set {
            visionAngle = Mathf.Clamp(value, 30f, 90f);
        }
    }

    public static float RunSpeed {
        get {
            return runSpeed;
        } set {
            runSpeed = Mathf.Clamp(value, 3.8f, 6f);
        }
    }

    static Enemy () {
        Reset();
    }

    /// <summary>
    /// Set initial values for enemy parameters
    /// </summary>
    public static void Reset () {
        epsilonTarget = 0.1f;
        epsilonHit = 0.6f;
        blindDistance = 10f;
        deafDistance = 2f;
        visionAngle = 35f;
        runSpeed = 4f;
        crawlSpeed = 1.5f;
    }

    private Player player;
    private TargetIndicator indicator;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        anim = gameObject.GetComponentInChildren<Animator>();
        audio = gameObject.GetComponentInChildren<AudioSource>();
        IsChasing = false;
        HasBeenChasing = false;
    }

    public void Init (Player player) {
        this.player = player;
        indicator = Instantiate(indicatorPrefab).GetComponent<TargetIndicator>();
        indicator.Init(transform, Camera.main, WorldGenerator.Player.transform, () => IsChasing);
    }
    
    void FixedUpdate () {
        Vector3 delta = player.transform.position - transform.position;
        float dist = Vector3.Magnitude(delta);
        Debug.DrawRay(transform.position, delta, Color.white);

        // Enemy close enough to see player, and
        if (dist < blindDistance &&
            
            // either close enough to hear her
            (dist < deafDistance ||
             
            // or looking towards her; and
             Mathf.Abs(Vector3.Angle(delta, transform.forward)) < visionAngle) &&
            
            // no wall between enemy and player
            !Physics.Raycast(transform.position,
                             delta,
                             dist,
                             LayerMask.GetMask("Wall"))) {

            // ...=> Chase player
            agent.destination = player.transform.position;
            if (!IsChasing) {
                anim.Play("crawl_fast");
                audio.volume = 0.8f;
                IsChasing = true;
                HasBeenChasing = true;
                agent.speed = runSpeed;
                Ambiance.AttackCount++;
            }

            // Player caught => game over / deal damage
            if (Vector3.SqrMagnitude(agent.destination - transform.position) < epsilonHit) {
                player.DealDamage();
                //gameOverCanvas.gameObject.SetActive(true);
            }

        // Last target reached and player not visible => give up
        } else if (Vector3.SqrMagnitude(agent.destination - transform.position) < epsilonTarget) {
            if(WorldGenerator.hasLoc())
                agent.destination = WorldGenerator.randomLoc;
            
            if (IsChasing) {
                anim.Play("crawl");
                audio.volume = 0.5f;
                IsChasing = false;
                agent.speed = crawlSpeed;
                Ambiance.AttackCount--;
            }
        }
    }

    void LateUpdate () {
        audio.enabled = Time.timeScale > 0f;
    }
}