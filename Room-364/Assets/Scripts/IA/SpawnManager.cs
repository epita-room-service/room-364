
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float FirstTimeBeforeSpawn;

    public float minSpawnTime;

    public float maxSpawnTime;
    
    private float elapsedTime = 0f;

    private GameObject[] AI;

    private bool generate = true;
    
    [SerializeField] private AudioClip DeathSound;
    [SerializeField] private AudioClip TouchedSound;
    
    
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (generate && elapsedTime >= FirstTimeBeforeSpawn)
        {
            SpawnObject();
            AI = GameObject.FindGameObjectsWithTag("IA");
            generate = false;
        }
        
        if (AI is not null && AI.Length > 0)
            DestroyAI();
    }
    void SpawnObject()
    {
        PhotonNetwork.Instantiate(objectToSpawn.name,transform.position,Quaternion.identity);
        //Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }

    void DestroyAI()
    {
        if (AI[0].GetComponent<MovingAI>().killTime >= 3f)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(DeathSound, 0.5f);
            PhotonNetwork.Destroy(AI[0]);
            AI = null;
            generate = true;
            elapsedTime = 0f;
            FirstTimeBeforeSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }
        
        else if (Vector3.Distance(AI[0].transform.position, AI[0].GetComponent<MovingAI>().GetTargetPlayer().transform.position) < 1f)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(TouchedSound);
            PhotonNetwork.Destroy(AI[0]);
            AI = null;
            generate = true;
            elapsedTime = 0f;
            FirstTimeBeforeSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
