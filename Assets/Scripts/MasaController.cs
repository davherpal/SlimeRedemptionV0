using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasaController : MonoBehaviour
{
    public float InMass = 100;
    public float time2LoseAllMass = 2f;
    public float PercentageGainedMass;
    private float gainedMass;

    private float mass;
    private float lostMassPerSecond;            //TARDA DOS SEGUNDOS EN PERDER TODA LA MASA

    
    public float finSize = 2f;
    private float inSize;
    private float difSize;
    private float gainedMassSprite;

    private float spriteSizeLost = .25f;            // TAMAÑO QUE QUEREMOS QUE PIERDA POR SEGUNDO, ES DECIR PARA PERDER TODA REQUIERE DOS
                                                    //DEPENDERA DEL TAMAÑO QUE TENDRA EL SLIME EN VERSION FINA
    public Slider slider;
    //public float timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        PercentageGainedMass = PercentageGainedMass / 100;
        mass = InMass;

        gainedMass = InMass * PercentageGainedMass;
        lostMassPerSecond = mass/ time2LoseAllMass;

        inSize = transform.localScale.x;
        difSize = inSize - finSize;
        spriteSizeLost = difSize / time2LoseAllMass;
        gainedMassSprite = difSize * PercentageGainedMass;

        slider.maxValue = mass;
        slider.value = mass;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (mass >= 0)
        {
            if (Input.GetMouseButton(0))
            {
                mass = mass - (lostMassPerSecond * Time.deltaTime);
                slider.value = mass;         
                transform.localScale = new Vector2(transform.localScale.x - (spriteSizeLost * Time.deltaTime), transform.localScale.y - (spriteSizeLost * Time.deltaTime));

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (mass <= InMass)
            {
                mass += gainedMass;
                slider.value = mass;
                transform.localScale = new Vector2(transform.localScale.x + gainedMassSprite, transform.localScale.y +gainedMassSprite);
            }

        }

        /*
        if (Input.GetMouseButton(0))
        {

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                mass = mass - lostMassPerSecond;
                slider.value = mass;
                print("ya" + mass);
                timer = 1;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            timer = 1;
        }
        */
        
    }
}
