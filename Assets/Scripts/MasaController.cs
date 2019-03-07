using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasaController : MonoBehaviour
{
    public float maxMass = 100;
    [Tooltip("Tiempo para perder toda la masa en segundos")]
    public float time2LoseAllMass = 2f;
    // Porcentaje de la masa principal que se gana.
    [Range(0.0f, 1f)]
    public float PercentageGainedMass;
    [Tooltip("Tamaño final que tendra el slime, cuando masa = 0")]
    public float finSize = 2f;

    private float gainedMass;
    private float mass;
    private float lostMassPerSecond;         
    private float inSize;
    private float difSize;
    private float gainedMassSprite;
    private float spriteSizeLost;           

    public Slider slider;
    public SlimeController isSliding;

    // Start is called before the first frame update
    void Start()
    {
        mass = maxMass;
        gainedMass = maxMass * PercentageGainedMass;

        lostMassPerSecond = mass/ time2LoseAllMass;         //Masa que se pierde por segundo

        inSize = transform.localScale.x;
        difSize = inSize - finSize;
        gainedMassSprite = difSize * PercentageGainedMass;

        spriteSizeLost = difSize / time2LoseAllMass;        //Tamaño del sprite que se reduce por segundo
 
        slider.maxValue = mass;
        slider.value = mass;

    }

    // Update is called once per frame
    void Update()
    {
        //Si se esta deslizando
        if (isSliding.isRight || isSliding.isLeft && isSliding.stop)   
        {
            if (mass >= 0)      //Pierde masa solo si es mayor de 0
            {
                mass = mass - (lostMassPerSecond * Time.deltaTime);              
                slider.value = mass;
                transform.localScale = new Vector2(transform.localScale.x - (spriteSizeLost * Time.deltaTime), transform.localScale.y - (spriteSizeLost * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Fruit"))
        { 

            Destroy(collision.gameObject);
            if (mass < maxMass)     //Gana masa solo si es menor la masa maxima
            {
                //Si la masa ganada es muy alta, el slider no se actualizara hasta que no se baje del valor de la masa principal
                mass += gainedMass;
                slider.value = mass;
                transform.localScale = new Vector2(transform.localScale.x + gainedMassSprite, transform.localScale.y + gainedMassSprite);
            }
        }
    }
}
