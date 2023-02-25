using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject2 : MonoBehaviour
{
    float speed = 5f;
    bool pointer = false;

    public float min;
    public float max;

    public bool openValve;
    public GameObject StatusValve;
    public Material DisableStatus, EnableStatus;
    public int valueDegree = 40;
    public int step;
    public static bool subStep1;
    public static bool subStep2;

    GameObject v2;

    GameObject v4;
    
    ID order;

    void Start()
    {
        ResetObject();
        order = GetComponent<ID>();
        v2 = GameObject.Find("V2 By pass");
        v4 = GameObject.Find("V4 By pass");
        // GameManager.instance.hotFlowRate = 40;
        // GameManager.instance.coldFlowRate = 40;
    }

    void Update()
    {
        if (pointer && ButtonObj.openModal)
        {
            Debug.LogError(gameObject.transform.rotation.eulerAngles.y);
            if (Input.GetMouseButton(0) && (gameObject.transform.rotation.eulerAngles.y >= min && gameObject.transform.rotation.eulerAngles.y <= max))
            {
                float v = speed * Input.GetAxis("Mouse X")*-1;
                transform.Rotate(0, v, 0);
            }
            if (gameObject.transform.rotation.eulerAngles.y > max && gameObject.transform.rotation.eulerAngles.y <= 180)
            {
                transform.localEulerAngles = new Vector3(0, max, 0);
            }

            else if (gameObject.transform.rotation.eulerAngles.y < min || gameObject.transform.rotation.eulerAngles.y > 180)
            {
                transform.localEulerAngles = new Vector3(0, min, 0);
            }
            int tmp1 = Mathf.FloorToInt((max - gameObject.transform.rotation.eulerAngles.y) / ((max - min) / 40));
            int tmp2 = Mathf.FloorToInt((max - gameObject.transform.rotation.eulerAngles.y) / ((max - min) / tmp1 - 10));
            valueDegree = tmp2 + (tmp1 - (tmp2*2));
            Debug.Log(this.gameObject.tag);
             if(this.gameObject.name == "Valve1_03_Pivot" || this.gameObject.name == "Valve2_03_Pivot"){
                GameManager.instance.hotFlowRate = valueDegree;
            }
            else if(this.gameObject.name == "Valve1_01_Pivot" || this.gameObject.name == "Valve2_01_Pivot"){
                GameManager.instance.coldFlowRate = valueDegree;
            }
            if(this.gameObject.name == "Valve1_04_Pivot" || this.gameObject.name == "Valve2_04_Pivot"){
                GameManager.instance.hotFlowRate = valueDegree;
            }
            else if(this.gameObject.name == "Valve1_02_Pivot" || this.gameObject.name == "Valve2_02_Pivot"){
                GameManager.instance.coldFlowRate = valueDegree;
            }
            Debug.Log("Tmp1 "+tmp1);
            Debug.Log("Tmp2 "+tmp2);
            Debug.Log("valueDegree "+valueDegree);

        }
           if(order.playAll && GameManager.instance.step != 0 ){
             if(gameObject.transform.rotation.eulerAngles.y == min)
            {
                openValve = true;
                // GameManager.instance.step = 2;
                StatusValve.GetComponent<MeshRenderer>().material = EnableStatus;
                if(gameObject.name == "Valve1_01_Pivot" || gameObject.name == "Valve2_01_Pivot"){
                    subStep1 = true;
                    v2.GetComponent<MeshRenderer>().material = EnableStatus;
                    GameManager.instance.coldFlowRate = 40;

                }
                else if(gameObject.name == "Valve1_03_Pivot" || gameObject.name == "Valve2_03_Pivot"){
                    v4.GetComponent<MeshRenderer>().material = EnableStatus;
                    subStep2 = true;
                    GameManager.instance.hotFlowRate = 40;
                }
            }
            else
            {
                openValve = false;
                // GameManager.instance.step = 1;
                StatusValve.GetComponent<MeshRenderer>().material = DisableStatus;
                 if(gameObject.name == "Valve1_01_Pivot" || gameObject.name == "Valve2_01_Pivot"){
                    subStep1 = false;
                    v2.GetComponent<MeshRenderer>().material = DisableStatus;
                     GameManager.instance.coldFlowRate = 0;

                }
                else if(gameObject.name == "Valve1_03_Pivot" || gameObject.name == "Valve2_03_Pivot"){
                    v4.GetComponent<MeshRenderer>().material = DisableStatus;
                    subStep2 = false;
                    GameManager.instance.hotFlowRate = 0;
                }
            }   
        }
    }

    public void ResetObject()
    {
        transform.localEulerAngles = new Vector3(0, max, 0);
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
