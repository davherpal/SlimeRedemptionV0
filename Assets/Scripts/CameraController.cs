using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothCam;
    public Transform target;
    [Range(0.2f, 0.9f)] public float deadZoneSizeFactor = 0.7f;

    private float dZH, dZW;

    // Use this for initialization
    void Start()
    {
        dZH = Camera.main.orthographicSize * deadZoneSizeFactor;
        dZW = dZH * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        float yMov;
        if (IsTargetInDeadZone(out yMov))
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + yMov, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothCam);
        }
    }

    bool IsTargetInDeadZone(out float yMov)
    {
        bool inDeadZone = false;
  
        yMov = 0;

        //Variables para regular el rango que tiene que pasar el jugador para que empieze la camara a moverse
        float yMaxDeadZone = transform.position.y + dZH;
        float yMinDeadZone = transform.position.y - dZH;

        //Si la posicion (y o vertical) del target es mayor al yMixDeadZone,  mueve la pantalla hacia el player hasta que target.position y sea menor a yMaxDeadZone
        if (target.position.y > yMaxDeadZone)
        {
            yMov = target.position.y - yMaxDeadZone;
            inDeadZone = true;
        }
        //Si la posicion (y o vertical) del target es menor al yMinDeadZone,  mueve la pantalla hacia el player hasta que target.position y sea mayor a yMinDeadZone
        if (target.position.y < yMinDeadZone)
        {
            yMov = target.position.y - yMinDeadZone;
            inDeadZone = true;
        }

        return inDeadZone;
    }

}
