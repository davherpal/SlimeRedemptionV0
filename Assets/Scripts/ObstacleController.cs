using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    [Tooltip("Porcentaje de masa que perdera al entrar en contacto")]
    [Range(0.0f, 1f)]
    public float PercentageLostMass;

    // Masa que perdera player segun el porcentaje de masa dada.
    private float lostMass;
    // Tamaño que perdera el sprite segun el porcentaje de masa dada.
    private float lostMassSprite;


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostMass = GameController.instance.maxMass * PercentageLostMass;//modificar cuando creemos game controller
            lostMassSprite = GameController.instance.difSize * PercentageLostMass;

            GameController.instance.LostMass(lostMass, lostMassSprite);//modificar cuando creemos game controller       
        }
    }
}
