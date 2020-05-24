using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private float _pitch = 0, _yaw = 90;

    void Start()
    {
        lastClickTime = Time.time - 2;
    }

    const float doubleClickTime = 1;

    private float lastClickTime;

    private string object_name = "";
    [SerializeField] private float speedH = 2f;
    [SerializeField] private float speedV = 2f;

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {

            _yaw += speedH * Input.GetAxis("Mouse X");
            _pitch -= speedV * Input.GetAxis("Mouse Y") % 180;

            _pitch = Mathf.Max(-90, Mathf.Min(90, _pitch));

            transform.parent.eulerAngles = new Vector3(0f, _yaw, 0);
            transform.parent.eulerAngles = new Vector3(_pitch, transform.eulerAngles.y, 0);
        }



        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.Log(Physics.Raycast(ray, out hit));
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ball")
                {
                    if (Time.time < lastClickTime + doubleClickTime && object_name == hit.collider.name)
                    {
                        hit.collider.gameObject.GetComponent<BallControll>().BallRestart();
                    }
                    else
                    {
                        lastClickTime = Time.time;
                        object_name = hit.collider.name;
                        hit.collider.gameObject.GetComponent<BallControll>().Move();
                    }
                }
            }
        }
    }
}
