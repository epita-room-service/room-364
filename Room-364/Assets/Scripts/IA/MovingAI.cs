using UnityEngine;

public class MovingAI : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float maxDistance = 20.0f;

    public GameObject[] players;
    
    private GameObject targetPlayer;
    
    private bool playerLooking = false;

    private bool playerfound = false;
    
    public float killTime = 0f;
    
    [SerializeField] private AudioClip MovingSound;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (!playerfound && players.Length > 0)
        {
            int randomIndex = Random.Range(0, players.Length);
            targetPlayer = players[randomIndex];
            playerfound = true;
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(MovingSound, 0.8f);
    }

    void Update()
    {
        // Check if any players exist
        if (targetPlayer is not null)
        {
            // Check if the player is looking at the AI
            RaycastHit hit;
            if(Physics.Raycast(targetPlayer.transform.position, targetPlayer.transform.forward, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("IA"))
                {
                    playerLooking = true;
                    killTime += Time.deltaTime;
                }
                else
                {
                    playerLooking = false;
                }
            }
            else
            {
                playerLooking = false;
            }

            // Move the AI towards the player if it's not being looked at
            if (!playerLooking)
            {
                Vector3 directionToPlayer = targetPlayer.transform.position - transform.position;
                transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime, Space.World);
                transform.LookAt(targetPlayer.transform);
            }
        }
    }

    public GameObject GetTargetPlayer()
    {
        return targetPlayer;
    }
}