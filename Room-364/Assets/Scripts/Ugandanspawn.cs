
using UnityEngine;

public class Ugandanspawn : MonoBehaviour
{
    
    public GameObject objSpawn; // L'objet à faire apparaître
    public Transform spawn; // Où l'objet doit apparaître

    public void SpawnObject()
    {
        Instantiate(objSpawn, spawn.position, spawn.rotation);
    }
}
