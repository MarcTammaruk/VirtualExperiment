using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnableOutline : EventTrigger
{
    Outline gameObject;
    ID order;
    public static bool openModal;
    // Start is called before the first frame update
    void Start()
    {
        gameObject = GetComponent<Outline>();
        order = GetComponent<ID>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //  public void onPointerEnter()
    // {
    //     Debug.Log("Reset Rotation 90");
    //     gameObject.enabled = true;
    // }

    // public void onPointerExit()
    // {
    //     gameObject.enabled = false;

    // }

    public void OnPointerEnterFunction(){
        if((order.playAll && GameManager.instance.step != 0) || !order.playAll){
            if((!order.playAll && order.id == 10 && RotateObject2.new1) || (!order.playAll && order.id == 1 && RotateObject2.subStep1 && RotateObject2.new1) || (!order.playAll && order.id == 2 && RotateObject2.subStep2 && RotateObject2.new1) || order.playAll){
                gameObject.enabled = true;
                openModal = true;
           }
        }
    }

    public void OnPointerExitFunction(){
        if((order.playAll && GameManager.instance.step != 0) || !order.playAll){
            if((!order.playAll && order.id == 10 && RotateObject2.new1) || (!order.playAll && order.id == 1 && RotateObject2.subStep1 && RotateObject2.new1) || (!order.playAll && order.id == 2 && RotateObject2.subStep2 && RotateObject2.new1) || order.playAll){
                    if(GameManager.instance.nameObjectSelect == gameObject.name && gameObject.enabled == false){
                    gameObject.enabled = true;
                    openModal = false;

                }
                    else if(GameManager.instance.nameObjectSelect != gameObject.name ){
                    gameObject.enabled = false;
                    openModal = false;
                 }
            }
        }
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        if((order.playAll && GameManager.instance.step != 0) || !order.playAll){
            if((!order.playAll && order.id == 10 && RotateObject2.new1) || (!order.playAll && order.id == 1 && RotateObject2.subStep1 && RotateObject2.new1) || (!order.playAll && order.id == 2 && RotateObject2.subStep2 && RotateObject2.new1) || order.playAll){
                gameObject.enabled = true;
                openModal = true;
           }
        }
    }

    public override void OnPointerExit(PointerEventData data)
    {
        if((order.playAll && GameManager.instance.step != 0) || !order.playAll){
            if((!order.playAll && order.id == 10 && RotateObject2.new1) || (!order.playAll && order.id == 1 && RotateObject2.subStep1 && RotateObject2.new1) || (!order.playAll && order.id == 2 && RotateObject2.subStep2 && RotateObject2.new1) || order.playAll){
                    if(GameManager.instance.nameObjectSelect == gameObject.name && gameObject.enabled == false){
                    gameObject.enabled = true;
                    openModal = false;

                }
                    else if(GameManager.instance.nameObjectSelect != gameObject.name ){
                    gameObject.enabled = false;
                    openModal = false;
                 }
            }
        }
    }
    
}
