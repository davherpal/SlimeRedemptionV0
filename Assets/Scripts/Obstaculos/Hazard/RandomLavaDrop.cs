using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLavaDrop : MonoBehaviour
{
    //Este Script Spawneara en la posicion del game object que tiene el script un gameobject de los que hay en el array.

    public GameObject[] objects;
    private int spawnRepeat;
    private int iniSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnRepeat = Random.Range(8, 16);
        iniSpawn = Random.Range(0, 8);
        InvokeRepeating("Spawn", iniSpawn, spawnRepeat);
    }

    public void Spawn()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
