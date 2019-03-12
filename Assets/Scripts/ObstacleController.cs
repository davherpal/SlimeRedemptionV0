using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private MasaController mc;
    [Tooltip("Porcentaje de masa que perdera al entrar en contacto")]
    [Range(0.0f, 1f)]
    public float PercentageLostMass;

    // Masa que perdera player segun el porcentaje de masa dada.
    private float lostMass;
    // Tamaño que perdera el sprite segun el porcentaje de masa dada.
    private float lostMassSprite;

    // Start is called before the first frame update
    void Start()
    {
        mc = GameObject.FindGameObjectWithTag("Player").GetComponent<MasaController>();
        // No le doy valores a las variables aqui porque este gameobject se crea antes del Player
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostMass = mc.maxMass * PercentageLostMass;//modificar cuando creemos game controller
            lostMassSprite = mc.difSize * PercentageLostMass;

            mc.LostMass(lostMass, lostMassSprite);//modificar cuando creemos game controller       
        }
    }
}
