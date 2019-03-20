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
    // Indicaremos si es un hazard
    public bool isAHazard;
    // Tiempo para que se aumente daño al hazard progresivamente
    public float overTimeDamage = 0.5f;
    private bool isCollisioning;
    private float overTime;
    private float initialPercen;

    private void Start()
    {
        overTime = overTimeDamage;
        initialPercen = PercentageLostMass;
    }

    private void Update()
    {
        // Si esta collisionando se le restara masa progresivamente
        if (isCollisioning)
        {
            lostMass = GameController.instance.maxMass * PercentageLostMass * Time.deltaTime;
            lostMassSprite = GameController.instance.difSize * PercentageLostMass * Time.deltaTime;
            GameController.instance.LostMass(lostMass, lostMassSprite);

            // Si el contador llega a su limite se le sumara un poco mas de daño que hace progresivamente
            if (overTime >= 0)
            {
                overTime -= Time.deltaTime;
            }
            else {
                PercentageLostMass += initialPercen;
                overTime = overTimeDamage;

   
            }

        }
    }


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si no es un hazard, de la parte inferior de la pantalla
        if (!isAHazard)
        {
            lostMass = GameController.instance.maxMass * PercentageLostMass;
            lostMassSprite = GameController.instance.difSize * PercentageLostMass;
            GameController.instance.LostMass(lostMass, lostMassSprite);
        }
        else { isCollisioning = true; }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si es un hazard y ha dejado de colisionar detenemos el if de update y reiniciamos el daño que hacia en el contacto
        if (isAHazard)
        {
            isCollisioning = false;
            PercentageLostMass = initialPercen;
        }
    }

}
