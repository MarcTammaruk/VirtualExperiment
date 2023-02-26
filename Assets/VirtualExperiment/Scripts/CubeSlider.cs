using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSlider : MonoBehaviour
{
    public enum PumpName {
        COLDRATE1,
        HOTRATE1,
        COLDRATE2,
        HOTRATE2
    }
    public float speed;
    public float max;
    public float min;
    public int score;

    public Vector3 posA;
    public Vector3 posB;

    public PumpName pump;

    // Start is called before the first frame update

    void Start()
    {
        transform.localPosition = posA;
    }

    // Update is called once per frame
    void Update()
    {
        if(PumpName.COLDRATE1 == pump){
            score = int.Parse(GameManager.instance.FlowRatePlateHeat[0].text);
        }
        else if(PumpName.HOTRATE1 == pump){
            score = int.Parse(GameManager.instance.FlowRatePlateHeat[1].text);
        }
        else if(PumpName.COLDRATE2 == pump){
            score = int.Parse(GameManager.instance.FlowRateDoublePipe[0].text);
        }
        else if(PumpName.HOTRATE2 == pump){
            score = int.Parse(GameManager.instance.FlowRateDoublePipe[1].text);
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(posB.x, posA.y+((posB.y-posA.y)/40) * score,posB.z) ,0.05f);
    }
}
