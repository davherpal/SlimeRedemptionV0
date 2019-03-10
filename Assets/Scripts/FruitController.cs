using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    
    private MasaController mc;
    [Tooltip("Porcentaje de masa que ganara al entrar en contacto")]
    [Range(0.0f, 1f)]
    public float PercentageGainedMass;

    // Masa que ganara player segun el porcentaje de masa dada.
    private float gainedMass;
    // Tamaño que ganara el sprite segun el porcentaje de masa dada.
    private float gainedMassSprite;

    private void Start()
    {
        mc = GameObject.FindGameObjectWithTag("Player").GetComponent<MasaController>();
        // No le doy valores a las variables aqui porque este gameobject se crea antes del Player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gainedMass = mc.maxMass * PercentageGainedMass;//modificar cuando creemos game controller
            gainedMassSprite = mc.difSize * PercentageGainedMass;

            mc.AddMass(gainedMass, gainedMassSprite);//modificar cuando creemos game controller
            Destroy(gameObject);
        }
    }
    
    
    
}
