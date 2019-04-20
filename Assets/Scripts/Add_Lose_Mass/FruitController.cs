using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
   
    // Este script te añade masa

    [Tooltip("Porcentaje de masa que ganara al entrar en contacto")]
    [Range(0.0f, 1f)]
    public float PercentageGainedMass;

    // Masa que ganara player segun el porcentaje de masa dada.
    private float gainedMass;
    // Tamaño que ganara el sprite segun el porcentaje de masa dada.
    private float gainedMassSprite;
    // Puntos que añade al comer
    public int score = 25;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.healed = true;

            GameController.instance.AddScore(score);

            gainedMass = GameController.instance.maxMass * PercentageGainedMass;//modificar cuando creemos game controller
            gainedMassSprite = GameController.instance.difSize * PercentageGainedMass;

            GameController.instance.AddMass(gainedMass, gainedMassSprite);//modificar cuando creemos game controller
            FindObjectOfType<audioController>().Play("giveHealth");
            Destroy(gameObject);
        }
    }
    
    
    
}
