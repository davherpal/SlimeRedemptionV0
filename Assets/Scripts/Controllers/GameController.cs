using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // En este script se trabaja con todo aquello que se masa, puntos...

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

    [HideInInspector] public bool shoot;
    public Image ShootImg;
    private float alphaShootable = 1f;
    private float alphaUnShootable = 0.4f;

    [HideInInspector] public float alturaActual;
    [HideInInspector] public int enemiesKilled;
    [HideInInspector] public int score;
    public Text alturaText;
    public Text scoreText;

    [HideInInspector]public int timeFall = 20; // provisional

    public float damageFlashSpeed = 5f;
    public Color damageColor;
    public Image damageImage;
    [HideInInspector] public bool damaged;

    public float healFlashSpeed = 5f;
    public Color healColor;
    public Image healImage;
    [HideInInspector] public bool healed;

    public GameObject canvas;
    private SaveController saveController;

    [HideInInspector] public bool isDead = false;

    // Particle System cuando se desliza
    public GameObject particle;

    // Particle sistem cuando muere
    public GameObject particleGO;
 
    private void Awake()
    {
        instance = this;
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
        saveController = GetComponent<SaveController>();

        particle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Si se esta deslizando y no esta en el suelo
        if (isSliding.isWall || isSliding.isStickyWall || isSliding.isIce && !isSliding.isGround )
        {

            particle.SetActive(true);

            if (mass >= 0)      //Pierde masa solo si es mayor de 0
            {
                mass = mass - (lostMassPerSecond * Time.deltaTime);
                slider.value = mass;
                player.transform.localScale = new Vector2(player.transform.localScale.x - (spriteSizeLost * Time.deltaTime), player.transform.localScale.y - (spriteSizeLost * Time.deltaTime));
            }
            else
            {
                //DEAD
                if (!isDead)
                {
                    playerDead();
                }
            }
        }
        else
        {
            particle.SetActive(false);
        }

        // Checks si es true y en el siguiente hace un lerp entre colores a una velocidad x
        DamageFlash();
        HealFlash();

        // ALTURA ACTUAL DEL JUGADOR
        alturaActual = player.transform.position.y;
        alturaText.text = alturaActual.ToString("F0") + " m";

    }
    // Añade puntuacion cuando enemigo muere
    public void AddScore(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();         
    }
    // Permite disparar o no y modifica canvas
    public void setShoot(bool shootable)
    {
        var tempColor = ShootImg.color;
        shoot = shootable;
        if (shootable)
        {
       
            tempColor.a = alphaShootable;
            ShootImg.color = tempColor;
            
        }
        else
        {
      
            tempColor.a = alphaUnShootable;
            ShootImg.color = tempColor;
        }

    }
    // Añade masa al player
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
    // Hace perder masa al player y checkea si esta vivo o no
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
        
        else
        {
            //DEAD
            //Debug.Log("dead");
            //FindObjectOfType<audioController>().Play("gameover");
            playerDead();
        }
        
    }
    // Si se recibe daño pantallazo de un color determinado
    public void DamageFlash()
    {
        if (damaged)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
    // Si se cura pantallazo de un color determinado
    public void HealFlash()
    {
        if (healed)
        {
            healImage.color = healColor;
        }
        else
        {
            healImage.color = Color.Lerp(healImage.color, Color.clear, healFlashSpeed * Time.deltaTime);
        }
        healed = false;
    }
    // Añade un numeor mas cuando un enemigo ha sido matado
    public void AddEnemyKilled()
    {
        enemiesKilled++;
    }
    // Cosas ha hacer cuando muere
    public void playerDead()
    {
        isDead = true;
        FindObjectOfType<audioController>().Play("gameover");
        canvas.GetComponent<MenusScript>().loadGameOverMenu();
        enemiesKilled += saveController.LoadDataEnemies();
        alturaActual +=saveController.LoadDataHeight();
        score += saveController.LoadDataScore();

        saveController.SaveData(score, alturaActual, enemiesKilled);

        player.gameObject.SetActive(false);
        Instantiate(particleGO, player.position, Quaternion.identity);
        particleGO.GetComponent<ParticleSystem>().Play();

    }

}



