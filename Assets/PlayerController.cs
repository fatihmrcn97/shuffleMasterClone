using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float zSpeed;
    public float _currentRunningSpeed; //4
    [SerializeField] private float limit_Z;
    public Vector3 offSet;
    private Camera cam;

    public static bool gamePause = false;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (gamePause)
            return;


        cam.transform.localPosition = transform.position + offSet;

        float newX = 0;
        float touchXDelta = 0;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = -Input.GetTouch(0).deltaPosition.x / Screen.width; // parmak kaydýrmasý
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = -Input.GetAxis("Mouse X"); // mouse kaydýrmasý
        }
        newX = transform.position.z + zSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limit_Z, limit_Z); // Sýnýrlandýrma

        Vector3 newPosition = new Vector3(transform.position.x + _currentRunningSpeed * Time.deltaTime, transform.position.y, newX);
        transform.position = newPosition;

    }
}
