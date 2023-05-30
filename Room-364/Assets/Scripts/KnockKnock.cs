using System.Collections;
using UnityEngine;

public class KnockKnock : MonoBehaviour

{
    [SerializeField] private AudioClip KnockSound;
    [SerializeField] private float TimeBeforeKnock;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(TimeBeforeKnock);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(KnockSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
