using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObstaculoCaida : MonoBehaviour
{
    public float time2Destroy = 4f; // tiempo para destruir el gameobject despues de detectar al player
    public string soundToPlay;      //aqui pondremos que sonido de la lista queremos que suene cuando el enemigo detecte al player
    public bool notsound;       // si esta en false, no sonara ningun sonido al detectar al player, es necesario para evitar errores
    private Rigidbody2D rb2d;
    public GameObject go;
    private bool once = true;       // para que solo suene una vez
    // Start is called before the first frame update
    void Start()
    {
        rb2d = go.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)         // si detectamos al con el player
    {
        if (collision.gameObject.tag == "Player")
        {
            if (once)
            {
                if (!notsound)
                {
                    FindObjectOfType<audioController>().Play(soundToPlay);      //suena el sonido
                    once = false;   //ponemos esto en falso para que no suene mas d euna vez
                }
            }
            rb2d.isKinematic = false;
            Destroy(gameObject, time2Destroy);
        }     
    }
}
