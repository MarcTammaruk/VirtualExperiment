using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int modeGame = 0;
    public int step = 0;
    public List<Text> TempPlateHeat;
    public List<Text> TempDoublePipe;

    public List<Text> FlowRatePlateHeat;
    public List<Text> FlowRateDoublePipe;

    public int coldTemp;
    public int hotTemp;

    public int coldFlowRate;
    public int hotFlowRate;

    public float cold_in,cold_out,hot_in,hot_out;

    public GameObject selectObject;
    public GameObject cameraObject;
    public string nameObjectSelect = "";

    public Text name;
    public Text value;
    public Text unit;
    public Text topic;

    private string tagName;

    public RotateLockObject coldPumpV1;
    public RotateLockObject hotPumpV1;

    public RotateLockObject coldPumpV2;
    public RotateLockObject hotPumpV2;

    public int state =  0;
    public float Q_hot;
    public float Q_cold;

    public VideoPlayer[] videoPlayer;

    public GameObject[] text; 
    public Material DisableStatus;
    public Material EnableStatus;

    public float[] valve = new float[4];

    public GameObject v5;
    public RotateObject2 v5_pivot;
    public GameObject v6;
    public RotateObject2 v6_pivot;


    Dictionary<string,Vector3> item = new Dictionary<string,Vector3>() {
        { "Fuses1_01",new Vector3(1.4f,10.26f,2.26f) },
        { "Fuses1_02",new Vector3(-0.14f,10.26f,2.26f) },
        { "Valve1_01",new Vector3(-1.82f,10.26f,-1.67f) },
        { "Valve1_02",new Vector3(-1.82f,10.26f,-1.13f) },
        { "Valve1_03",new Vector3(-1.82f,10.26f,-0.44f) },
        { "Valve1_04",new Vector3(-1.82f,10.26f,0.17f) },
        { "Valve2_01",new Vector3(-1.62f,10.26f,-3.07f) },
        { "Valve2_02",new Vector3(-1.62f,10.26f,-2.52f) },
        { "Valve2_03",new Vector3(-1.62f,10.26f,-1.78f) },
        { "Valve2_04",new Vector3(-1.62f,10.26f,-1.17f) },
        { "Valve2_05",new Vector3(-2.45f,10.26f,-0.31f) },
        { "Valve2_06",new Vector3(-2.45f,10.26f,-0.03f) },
        { "Fuses2_01",new Vector3(0.051f,10.26f,2.113f) },
        { "Fuses2_02",new Vector3(1.804f,10.26f,2.113f) },
    };

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        cold_in = coldTemp;
        hot_in = hotTemp;
        

        FlowRatePlateHeat[0].text = coldFlowRate.ToString();
        FlowRatePlateHeat[1].text = hotFlowRate.ToString();

        FlowRateDoublePipe[0].text = coldFlowRate.ToString();
        FlowRateDoublePipe[1].text = hotFlowRate.ToString();

       if (modeGame == 1)
        {
            /*
            Q_hot = (-1.8672f * (cold_in - 7f)) - 316.56f;
            Q_cold = (30.879f * (hot_in - 3f)) + 360.22f;
            */
            Q_hot = ((0.926f * hot_in) - 10.718f) - ((((1303 * hot_in) + 69165) / 200000) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
            Q_cold = ((1.075f * cold_in) + 13.25f) - ((((3 * cold_in) + 250) / 320) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
        }
        else if(modeGame == 2)
        {
            /*
            Q_hot = (-2.9859f * (cold_in - 7f)) + 2.3261f;
            Q_cold = (3.8381f * (hot_in - 3f)) + 42.714f;
            */
            Q_hot = ((1.175f * hot_in) - 9.25f) - ((((-19 * hot_in) + 930) / 800) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
            Q_cold = ((1.0122f * cold_in) + 0.4039f) + ((((-8429 * cold_in) + 332705) / 400000) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
        }
        else if(modeGame == 3)
        {
            /*
            Q_hot = (-0.074f * (cold_in - 7f)) - 4.3033f;
            Q_cold = (-0.3707f * (hot_in - 3f)) + 8.610f;
            */
            Q_hot = ((0.9989f * hot_in) - 0.0223f) - ((((75 * hot_in) + 1834) / 400000) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
            Q_cold = ((0.9992f * cold_in) + 0.1135f) + ((((81 * cold_in) + 2125) / 400000) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
        }
        else if(modeGame == 4)
        {
            /*
            Q_hot = (0.2347f * (cold_in - 7f)) - 3.8382f;
            Q_cold = (-0.3707f * (hot_in - 3f)) + 8.610f;
            */

            Q_hot = ((0.9996f * hot_in) - 0.1448f) - ((((31 * hot_in) + 25066) / 400000) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
            Q_cold = ((1.0071f * cold_in) - 0.1283f) + ((((647 * cold_in) + 13293) / 400000) * (Mathf.Abs(hotFlowRate - coldFlowRate)));
        }

        if(coldPumpV1.Status && coldFlowRate != 0 && cold_in!= 0){
            //cold_out = (Q_cold / (coldFlowRate * 4.187f)) + cold_in;
            cold_out = Q_cold;
            FlowRatePlateHeat[0].text = coldFlowRate.ToString();
        }
        else if(coldPumpV2.Status && coldFlowRate != 0 && cold_in!= 0){
            //cold_out = (Q_cold / (coldFlowRate * 4.187f)) + cold_in;
            cold_out = Q_cold;
            FlowRateDoublePipe[0].text = coldFlowRate.ToString();
        }
        else {
            cold_out = 0;
            FlowRatePlateHeat[0].text = "0";
            FlowRateDoublePipe[0].text = "0";
        }
        if(hotPumpV1.Status && hotFlowRate != 0 && hot_in != 0){
            //hot_out = (Q_hot / (hotFlowRate * 4.187f)) + hot_in;
            hot_out = Q_hot;
            FlowRatePlateHeat[1].text = hotFlowRate.ToString();

        }
        else if(hotPumpV2.Status && hotFlowRate != 0 && hot_in != 0){
            //hot_out = (Q_hot / (hotFlowRate * 4.187f)) + hot_in;
            hot_out = Q_hot;
            FlowRateDoublePipe[1].text = hotFlowRate.ToString();
        }
        else {
            hot_out = 0;
            FlowRatePlateHeat[1].text = "0";
            FlowRateDoublePipe[1].text = "0";
        }

       
        if(!coldPumpV1.Status)
        {
            TempPlateHeat[1].text = "0.00";
        }
        else
        {
            TempPlateHeat[1].text = cold_out.ToString("F2");
        }

        if(!coldPumpV2.Status)
        {
            TempDoublePipe[1].text = "0.00";
        }
        else
        {
            TempDoublePipe[1].text = cold_out.ToString("F2");
        }


        if(!hotPumpV1.Status)
        {
            TempPlateHeat[3].text = "0.00";
        }
        else
        {
            TempPlateHeat[3].text = hot_out.ToString("F2");
        }

        if(!hotPumpV2.Status)
        {
            TempDoublePipe[3].text = "0.00";
        }
        else
        {
            TempDoublePipe[3].text = hot_out.ToString("F2");
        }
        TempPlateHeat[0].text = cold_in.ToString("F2");
        TempPlateHeat[2].text = hot_in.ToString("F2");

        TempDoublePipe[0].text = cold_in.ToString("F2");
        TempDoublePipe[2].text = hot_in.ToString("F2");

        // if(tagName == "switch_cold"){
            // value.text = coldTemp.ToString();
        // }
        // if(tagName == "switch_hot"){
        //     value.text = hotTemp.ToString();
        // }
        if(tagName == "switch_cold"){
            value.text = coldTemp.ToString();
        }
        else if(tagName == "switch_hot"){
            value.text = hotTemp.ToString();

        }
        else if(tagName == "V1_Cold" || tagName == "V2_ByPass"){
            value.text = coldFlowRate.ToString();

        }
        else if(tagName == "V3_Hot" || tagName == "V4_ByPass"){
            value.text = hotFlowRate.ToString();

        }

        if ((!v5_pivot.openValve && modeGame == 4) || (!v6_pivot.openValve && modeGame == 3))
        {
            coldFlowRate = 0;
            hotFlowRate = 0;
        }
    }

    public void setFlowRate(int n,float value)
    {
        /*
        if (n == 2 || n == 4)
        {
            valve[n - 1] = Mathf.FloorToInt((value / 100) * valve[n - 2]);
        }
        else
        {
            valve[n - 1] = Mathf.FloorToInt(value);
            //valve[n] = Mathf.FloorToInt((value / 100) * valve[n - 1]);
        }
        */

        valve[n - 1] = value;

        float percent1, percent3;

        percent1 = ((valve[1] / 100) * (valve[0] - 4));
        percent3 = ((valve[3] / 100) * (valve[2] - 4));

        coldFlowRate = Mathf.FloorToInt(percent1 + (valve[0] - (percent1 * 2)));
        hotFlowRate = Mathf.FloorToInt(percent3 + (valve[2] - (percent3 * 2)));

        if (valve[0] == 0)
        {
            coldFlowRate = 0;
        }
        else if(valve[0] < 4)
        {
            coldFlowRate = 4;
        }

        if (valve[2] == 0)
        {
            hotFlowRate = 0;
        }
        else if (valve[2] < 4)
        {
            hotFlowRate = 4;
        }
        //coldFlowRate = Mathf.FloorToInt(valve[1] + (valve[0] - (valve[1] * 2)));
        //hotFlowRate = Mathf.FloorToInt(valve[3] + (valve[2] - (valve[3] * 2)));
    }

    public void SetModeGame(int mode)
    {
        modeGame = mode;

        if(mode == 4){
            v6.GetComponent<BoxCollider>().enabled = false;
            v5.GetComponent<BoxCollider>().enabled = true;
            RotateObject2.new1 = false;
        }
        else if(mode == 3){
            v5.GetComponent<BoxCollider>().enabled = false;
            v6.GetComponent<BoxCollider>().enabled = true;
            RotateObject2.new1 = false;
        }
        else {
            RotateObject2.new1 = true;
        }

    }

    // public void onPointerEnter()
    // {
    //     if(selectObject != null)
    //     {
    //         selectObject.GetComponent<Outline>().enabled = true;
    //     }
    // }

    // public void onPointerExit()
    // {
    //     if(selectObject != null)
    //     {
    //         selectObject.GetComponent<Outline>().enabled = true;
    //     }
    // }


    public void SetSelectObject(GameObject obj)
    {
        Debug.Log("SetSelct "+ EnableOutline.openModal);
        if(selectObject == null && EnableOutline.openModal)
        {
            tagName = obj.tag;
            if(obj.tag == "switch_cold"){
                topic.text = "Temperature";
                name.text = "Cold inlet :";
                // value.text = coldTemp.ToString();
                unit.text = "°C";
            }
            else if(obj.tag == "switch_hot"){
                topic.text = "Temperature";
                name.text = "Hot inlet :";
                // value.text = hotTemp.ToString();
                unit.text = "°C";
            }
            else if(obj.tag == "V1_Cold")
            {
                topic.text = "Cold Water";
                name.text = "Flow rate :";
                // value.text = coldFlowRate.ToString();
                unit.text = "LPM";
            }
            else if (obj.tag == "V2_ByPass")
            {
                topic.text = "Cold Water";
                name.text = "Flow rate :";
                // value.text = coldFlowRate.ToString();
                unit.text = "LPM";
            }
            else if (obj.tag == "V3_Hot")
            {
                topic.text = "Hot Water";
                name.text = "Flow rate :";
                // value.text = hotFlowRate.ToString();
                unit.text = "LPM";
            }
            else if (obj.tag == "V4_ByPass")
            {
                topic.text = "Hot Water";
                name.text = "Flow rate :";
                // value.text = hotFlowRate.ToString();
                unit.text = "LPM";
            }
            else {
                topic.text = "";
                name.text = "";
                value.text = "";
                unit.text = "";
            }
            cameraObject.transform.position = item.ContainsKey(obj.name) ? item[obj.name] : new Vector3(obj.transform.position.x, 10.26f, obj.transform.position.z);
            selectObject = obj;
            nameObjectSelect = obj.name;
            if (obj.GetComponent<RotateObjtest>() != null)
            {
                obj.GetComponent<RotateObjtest>().enabled = true;
            }
            if(item.ContainsKey(obj.name)) {
            GameObject objTmp =  GameObject.Find(obj.name+"_Pivot");
            if(objTmp.GetComponent<RotateLockObject>() != null)
            {
                objTmp.GetComponent<RotateLockObject>().enabled = true;
            }
            else
            {
                objTmp.GetComponent<RotateObject2>().enabled = true;
            }
        }
        }
    }

    public void ClearSelectObject()
    {
        selectObject.GetComponent<Outline>().enabled = false;    
        if (selectObject.GetComponent<RotateObjtest>() != null)
        {
            selectObject.GetComponent<RotateObjtest>().enabled = false;
        }

        if(item.ContainsKey(nameObjectSelect)) {
            GameObject objTmp =  GameObject.Find(nameObjectSelect+"_Pivot");
            if(objTmp.GetComponent<RotateLockObject>() != null)
            {
                objTmp.GetComponent<RotateLockObject>().enabled = false;
            }
            else
            {
                objTmp.GetComponent<RotateObject2>().enabled = false;
            }
        }
        // if (selectObject.GetComponent<RotateLockObject>() != null)
        // {
        //     selectObject.GetComponent<RotateLockObject>().enabled = false;
        // }
        nameObjectSelect = "";
        tagName = "";
        selectObject = null;
        EnableOutline.openModal = false;
    }

    public void ResetButton() 
    {
        foreach (VideoPlayer video in videoPlayer){
            video.Stop();
        }
        step = 0;
        hotFlowRate = 0;
        coldFlowRate = 0;
        RotateObject2.subStep1 = false;
        RotateObject2.subStep2 = false;
        RotateObject2.new1 = false;
        foreach (GameObject obj in text){
            obj.GetComponent<MeshRenderer>().material = DisableStatus;
        }

        for(int i = 0; i < valve.Length; i++)
        {
            valve[i] = 0f;
        }
        coldPumpV1.Status = false;
        coldPumpV2.Status = false;
        hotPumpV1.Status = false;
        hotPumpV2.Status = false;
    }
    public void StartVideo()
    {
        videoPlayer[modeGame-1].Play();

    }
    public void PauseVideo()
    {
        videoPlayer[modeGame-1].Pause();
    }

    public void QuitProgram()
    {
        Application.Quit();
    }

    public void HomeProgram()
    {
        ResetButton();
        SceneManager.LoadScene(0);
    }
}
