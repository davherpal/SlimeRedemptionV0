using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDisparoController : MonoBehaviour
{
    public Transform bulletSpawner;
    public GameObject bulletPrefab;
    public float spawnRate = 1f;

    private void Start()
    {
        //Hace la funcion que le pongas, tardara 0s en ejecutarse y se repetira cada 1s 
        InvokeRepeating("EnemyShooting", 1f, spawnRate);
    }
    //Funcion para disparar
    public void EnemyShooting()
    {
        Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
    }

}
