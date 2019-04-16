using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //Este Script Spawneara en la posicion del game object que tiene el script un gameobject de los que hay en el array.

    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }


}
