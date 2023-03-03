using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObj : MonoBehaviour
{
    public GameObject[] objClose;
    public GameObject[] objOpen;
    ID order;
    public static bool openModal;

    // Start is called before the first frame update
    void Start()
    {
        order = GetComponent<ID>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBtn() 
    {
        if((order.playAll && GameManager.instance.step != 0) || (!order.playAll && GameManager.instance.step != 0)){
            if((!order.playAll && order.id == 10 && RotateObject2.new1) || (!order.playAll && order.id == 1 && RotateObject2.subStep1 && RotateObject2.new1) || (!order.playAll && order.id == 2 && RotateObject2.subStep2 && RotateObject2.new1) || order.playAll){
                
                foreach (GameObject _objClose in objClose){
                    _objClose.SetActive(false);
                }

                foreach (GameObject _objOpen in objOpen){
                    _objOpen.SetActive(true);
                }
                openModal = true;
            }
        }
    }
    
    public void CloseModal() {
        openModal = false;
        GameManager.instance.StartVideo();
    }
}
