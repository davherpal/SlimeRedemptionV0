using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasaController : MonoBehaviour
{

    public float mass = 100;
    public int lostMassPerSecond = 50;
    public float timer = 1;
    public int gainedMass;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = mass;
        slider.value = mass;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mass = mass - (lostMassPerSecond * Time.deltaTime);
            slider.value = mass;
        }

        if (Input.GetMouseButtonDown(1))
        {
            mass += gainedMass;
            slider.value = mass;
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
