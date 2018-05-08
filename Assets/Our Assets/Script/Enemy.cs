﻿// MoveTo.cs
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {
    static readonly float epsilonTarget = 0.1f,
                          epsilonHit = 0.6f,
                          blindDistance = 10f,
                          deafDistance = 2f,
                          visionAngle = 40f,
                          runSpeed = 4f,
                          crawlSpeed = 0;// 1.5f;
   
    private NavMeshAgent agent;
    private Vector3 initialPos;
    private Animator anim;

    
    private Player player;
    private Canvas gameOverCanvas;
    public bool chasing { get; private set; }

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        anim = gameObject.GetComponentInChildren<Animator>();
        chasing = false;
    }

    public void Init (Player player, Canvas gameOverCanvas) {
        this.player = player;
        this.gameOverCanvas = gameOverCanvas;
    }

    private float rer (float f) {
        print("Rer " + f);
        return f;
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
             Mathf.Abs(rer(Vector3.Angle(delta, transform.forward))) < visionAngle) &&
            
            // no wall between enemy and player
            !Physics.Raycast(transform.position,
                             delta,
                             dist,
                             LayerMask.GetMask("Wall"))) {

            // ...=> Chase player
            agent.destination = player.transform.position;
            if (!chasing) {
                anim.Play("crawl_fast");
                chasing = true;
                agent.speed = runSpeed;
                Ambiance.attackCount++;
            }

            // Player caught => game over / deal damage
            if (Vector3.SqrMagnitude(agent.destination - transform.position) < epsilonHit) {
                player.DealDamage();
                //gameOverCanvas.gameObject.SetActive(true);
            }

        // Last target reached and player not visible => give up
        } else if (Vector3.SqrMagnitude(agent.destination - transform.position) < epsilonTarget) {
            agent.destination = WorldGenerator.randomLoc;
            if (chasing) {
                anim.Play("crawl");
                chasing = false;
                agent.speed = crawlSpeed;
                Ambiance.attackCount--;
                
            }
        }

    }
}