using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    //Masa maxima y inicia del Player, mover en el futuro a game controller ya que es la vida.
    public float maxMass = 100;
    [Tooltip("Tiempo para perder toda la masa en segundos")]
    public float time2LoseAllMass = 2f;
    [Tooltip("Tamaño final que tendra el slime, cuando masa = 0")]
    public float finSize = 2f;
    public float inSize;

    private float mass;
    private float lostMassPerSecond;
    [HideInInspector] public float difSize;
    private float spriteSizeLost;

    public Slider slider;
    public Transform player;
    private SlimeController isSliding;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                DestroyImmediate(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        mass = maxMass;
        lostMassPerSecond = maxMass / time2LoseAllMass;         //Masa que se pierde por segundo

        difSize = inSize - finSize;
        spriteSizeLost = difSize / time2LoseAllMass;        //Tamaño del sprite que se reduce por segundo

        slider.maxValue = maxMass;
        slider.value = maxMass;

        isSliding = player.GetComponent<SlimeController>();

    }

    // Update is called once per frame
    void Update()
    {
        //Si se esta deslizando y no esta en el suelo
        if (isSliding.isRight && !isSliding.isGround || isSliding.isLeft && !isSliding.isGround)
        {
            if (mass >= 0)      //Pierde masa solo si es mayor de 0
            {
                mass = mass - (lostMassPerSecond * Time.deltaTime);
                slider.value = mass;
                player.transform.localScale = new Vector2(player.transform.localScale.x - (spriteSizeLost * Time.deltaTime), player.transform.localScale.y - (spriteSizeLost * Time.deltaTime));
            }
        }
    }

    public void AddMass(float gainedMass, float gainedMassSprite)
    {

        if (mass < maxMass)     //Gana masa solo si es menor la masa maxima
        {
            mass += gainedMass;
            slider.value = mass;
            player.transform.localScale = new Vector2(player.transform.localScale.x + gainedMassSprite, player.transform.localScale.y + gainedMassSprite);

            if (mass > maxMass) // Si una vez añadida la masa es mayor al valor maximo, lo ponemos al valor maximo indicado
            {
                mass = maxMass;
                slider.value = maxMass;
                player.transform.localScale = new Vector2(inSize, inSize);
            }
        }
    }

    public void LostMass(float lostMass, float lostMassSprite)
    {
        if (mass > 0)     //Gana masa solo si es menor la masa maxima
        {
            mass -= lostMass;
            slider.value = mass;
            player.transform.localScale = new Vector2(player.transform.localScale.x - lostMassSprite, player.transform.localScale.y - lostMassSprite);

            if (mass < 0) // Si una vez perdida la masa es menor al valor minimo, lo ponemos al valor minimo indicado
            {
                mass = 0;
                slider.value = 0;
                player.transform.localScale = new Vector2(finSize, finSize);
            }

        }
        /*
        else
        {
            mass = 0;
            slider.value = 0;
            transform.localScale = new Vector2(finSize, finSize);
            //MUERTO
        }
        */
    }



}



