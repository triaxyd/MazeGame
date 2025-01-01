using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [Header("Components")]
    AudioManager audioManager;
    [SerializeField] private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    [Header("Movement Settings")]
    [SerializeField] private float range; // Radius of movement area
    [SerializeField] private Transform centrePoint; // Center point of movement area

    [Header("Speed Settings")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float walkAcceleration = 5f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float chaseAcceleration = 7f;

    public bool IsChasing { get; private set; }
    public bool IsTerrorRadiusPlaying { get; private set; }

    private void Awake()
    {
        // Initialize components
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        agent.speed = walkSpeed;
        agent.acceleration = walkAcceleration;
        IsChasing = false;
        IsTerrorRadiusPlaying = false;
    }

    void Update()
    {
        if (IsChasing)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    public void StartChase()
    {
        IsChasing = true;
        agent.speed = chaseSpeed;
        agent.acceleration = chaseAcceleration;
        audioManager.PlaySFX(audioManager.enemyChase); // Chase growl
        animator.SetBool("isChasing", true);
        StopTerrorGrowl(); // Stop the terror growl when chasing
    }

    public void StopChase()
    {
        IsChasing = false;
        agent.speed = walkSpeed;
        agent.acceleration = walkAcceleration;
        animator.SetBool("isChasing", false);
    }

    public void PlayTerror()
    {
        if (!IsChasing && !IsTerrorRadiusPlaying) // Only play if not chasing
        {
            IsTerrorRadiusPlaying = true;
            audioManager.PlaySFX(audioManager.enemyGrowl, true); // True for looping the growl sound
        }
    }

    public void StopTerrorGrowl()
    {
        IsTerrorRadiusPlaying = false;
        audioManager.StopSFX(audioManager.enemyGrowl); // Stop the terror growl sound
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); // For debugging
                agent.SetDestination(point);
            }
        }
    }

    private void Chase()
    {
        agent.speed = chaseSpeed;
        agent.acceleration = chaseAcceleration;
        agent.SetDestination(player.position);
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
