using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObjtest : MonoBehaviour
{
    float speed = 5f;
    bool pointer = false;

    public bool FlowRateIsCold;

    public float min = 135;
    public float max = 225;

    public int valueDegree;
    void Start()
    {
        // ResetObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointer && ButtonObj.openModal)
        {
            if (Input.GetMouseButton(0) && (gameObject.transform.rotation.eulerAngles.y <= min || gameObject.transform.rotation.eulerAngles.y >= max))
            {
                float v = speed * Input.GetAxis("Mouse X");
                transform.Rotate(0, 0, v);
            }
            if (gameObject.transform.rotation.eulerAngles.y > min && gameObject.transform.rotation.eulerAngles.y <= 180)
            {
                Debug.Log("Reset Rotation 90");
                transform.localEulerAngles = new Vector3(270, min, 0);
            }

            else if (gameObject.transform.rotation.eulerAngles.y < max && gameObject.transform.rotation.eulerAngles.y > 180)
            {
                Debug.Log("Reset Rotation 270");
                transform.localEulerAngles = new Vector3(270, max, 0);
            }

            if(gameObject.transform.rotation.eulerAngles.y >= max)
            {
                valueDegree = Mathf.FloorToInt((gameObject.transform.rotation.eulerAngles.y-max)/ ((min+(360-max)) /100 ));
                // Debug.LogError(gameObject.transform.rotation.eulerAngles.y);
                
            }
            else if (gameObject.transform.rotation.eulerAngles.y <= min)
            {
                valueDegree = Mathf.FloorToInt((gameObject.transform.rotation.eulerAngles.y) / ((min + (360 - max)) / 100) + 50);
                // Debug.LogError(gameObject.transform.rotation.eulerAngles.y);
            }

            if (FlowRateIsCold)
            {
                GameManager.instance.coldTemp = valueDegree;
            }
            else
            {
                GameManager.instance.hotTemp = valueDegree;
            }
            GameManager.instance.value.text = valueDegree.ToString();
        }
    }

    public void ResetObject()
    {
        transform.localEulerAngles = new Vector3(270, max, 0);
        GameManager.instance.coldTemp = 0;
        GameManager.instance.hotTemp = 0;
    }

    public void onPointerEnter()
    {
        pointer = true;
    }

    public void onPointerExit()
    {
        pointer = false;
    }
}
