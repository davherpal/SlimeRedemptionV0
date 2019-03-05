using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyMov : MonoBehaviour
{
    public float periodo_rad_seg = 720;

    private Transform targetCenter;
    private float distance;
    private float angulo;


    // Start is called before the first frame update
    void Start()
    {
        targetCenter = GameObject.FindGameObjectWithTag("Tag1").transform;
        distance = Vector3.Distance(transform.position, targetCenter.position);
        angulo = 0;

        //Ambas opciones son los mismo
        //periodo_rad_seg = periodo_rad_seg * Mathf.PI / 180;
        periodo_rad_seg = Mathf.Deg2Rad * periodo_rad_seg;
    }

    // Update is called once per frame
    void Update()
    {
        //Donde esta el asteroide
        Vector3 posCenter = targetCenter.position;
        Vector3 offset = new Vector3(distance * Mathf.Cos(angulo), distance * Mathf.Sin(angulo), 0f);
        Vector3 newPos = posCenter + offset;

        transform.position = newPos;

        angulo += periodo_rad_seg * Time.deltaTime;

    }
}
