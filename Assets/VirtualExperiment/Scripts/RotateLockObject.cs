using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class RotateLockObject : MonoBehaviour
{
    bool pointer = false;
    float speed = 5f;

    public bool AxisIsX;

    public float down = -180, up = -270;
    public bool Status;

    public GameObject objStatus;
    public bool isVideo = false;
    public bool free = false;
    void Start()
    {
        ResetObject();
    }

    void Update()
    {
        if (pointer)
        {
            if( ButtonObj.openModal || free) {
                if (Input.GetMouseButton(0))
                    {
                        float v = speed * Input.GetAxis("Mouse Y");
                        Debug.Log(v);
                        if (AxisIsX)
                        {
                            if (v > 1)
                            {
                                transform.localEulerAngles = new Vector3(up, 0, 0);
                                if(isVideo){
                                    GameManager.instance.StartVideo();
                                    GameManager.instance.step = 1;
                                }
                                Status = true;
                            }
                            else if (v < -1)
                            {
                                transform.localEulerAngles = new Vector3(down, 0, 0);
                                if(isVideo){
                                    GameManager.instance.PauseVideo();
                                    GameManager.instance.step = 0;
                                }
                                Status = false;
                            }
                        }
                        else
                        {
                            if (v > 1)
                            {
                                transform.localEulerAngles = new Vector3(up, 0, 90);
                                if(isVideo){
                                    GameManager.instance.StartVideo();
                                    GameManager.instance.step = 1;
                                }
                                Status = true;
                            }
                            else if (v < -1)
                            {
                                transform.localEulerAngles = new Vector3(down, 0, 90);
                                if(isVideo){
                                    GameManager.instance.PauseVideo();
                                    GameManager.instance.step = 0;

                                }
                                Status = false;
                            }

                            objStatus.SetActive(Status);
                        }
                        
                        //transform.Rotate(0, 0, v);
                    }
            }
            
        }
    }

    public void ResetObject()
    {
        if (AxisIsX)
        {
            transform.localEulerAngles = new Vector3(down, 0, 0);
            Status = false;
        }
        else
        {
            transform.localEulerAngles = new Vector3(down, 0, 90);
            Status = false;
        }
        
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
