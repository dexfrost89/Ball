using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BallControll : MonoBehaviour
{

    class Data
    {
        public float[] x, y, z;

        public Data()
        {
            x = new float[1];
            y = new float[1];
            z = new float[1];
        }

        public Vector3[] ToVector3()
        {
            Vector3[] result = new Vector3[x.Length];
            for(int i = 0; i < x.Length; i++)
            {
                result[i] = new Vector3(x[i], y[i], z[i]);
            }
            return result;
        }
    }

    [SerializeField] private string path;
    

    private Vector3[] points;
    private void ReadData()
    {
        Data data;
        string jsonString;


        path = Application.dataPath + path;
        jsonString = File.ReadAllText(path);

        data = JsonUtility.FromJson<Data>(jsonString);

        points = data.ToVector3();
        /*foreach (float elem in data.x)
        {
            Debug.Log(elem);
        }*/
    }


    private bool staying = true;

    [SerializeField] UnityEngine.UI.Slider slider;

    [SerializeField] private float multiplier;
    public void Move()
    {
        if(staying)
        {
            staying = false;
            transform.position = points[0];
            index = 1;
            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, points[0]);
                lineRenderer.SetPosition(1, transform.position);
            }
        }
    }

    private LineRenderer lineRenderer;

    [SerializeField] private float speed;

    private int index = 0;

    private void MoveToNextPoint()
    {
        float distance = speed * Time.deltaTime * multiplier;
        while(index < points.Length)
        {
            if(distance >= (transform.position - points[index]).magnitude)
            {
                distance -= (transform.position - points[index]).magnitude;
                if (lineRenderer != null)
                {
                    lineRenderer.SetPosition(index, points[index]);
                    lineRenderer.positionCount += 1;
                }
                transform.position = points[index++];
                if (lineRenderer != null)
                {
                    lineRenderer.SetPosition(index, transform.position);
                }
            } else
            {
                transform.position += (points[index] - transform.position).normalized * distance;
                if (lineRenderer != null)
                {
                    lineRenderer.SetPosition(index, transform.position);
                }
                break;
            }
        }

        if(index == points.Length)
        {
            staying = true;
        }
    }

    public void BallRestart()
    {
        transform.position = points[0];
        staying = true;
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }

    private bool isBeingWatched = true;

    public void DisableVisiability()
    {
        isBeingWatched = false;
        multiplier = 0;
    }
    public void EnableVisiability()
    {
        isBeingWatched = true;
        multiplier = slider.value;
    }

    private void Start()
    {
        speed = 10;
        if (slider != null)
        {
            multiplier = slider.value;
        } else
        {
            multiplier = 1;
        }

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        ReadData();
        BallRestart();
        /*foreach(Vector3 elem in points)
        {
            Debug.Log(elem);
        }*/

        //move();
    }

    private void Update()
    {
        if (isBeingWatched && slider != null)
        {
            multiplier = slider.value;
        }
        if (!staying)
        {
            MoveToNextPoint();
        }
    }
}
