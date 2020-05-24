using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwaper : MonoBehaviour
{

    private GameObject[] following;
    private GameObject active;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        following = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach(GameObject camera in following)
        {
            camera.SetActive(false);
            camera.GetComponentInParent<BallControll>().DisableVisiability();
        }
        index = 2;
        active = following[index];
        SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetInactive(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponentInParent<BallControll>().DisableVisiability();
    }

    private void SetActive(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponentInParent<BallControll>().EnableVisiability();
    }

    public void SetNext()
    {
        SetInactive(active);
        index += following.Length - 1;
        index = index % following.Length;
        active = following[index];
        SetActive(active);
    }
    public void SetPrev()
    {
        SetInactive(active);
        index += 1;
        index = index % following.Length;
        active = following[index];
        SetActive(active);
    }
}
