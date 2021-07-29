using System.Collections;
using System.Collections.Generic;
using System.Text;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GenButton : MonoBehaviour
{
    public GameObject prefabButton;
    public GameObject fatherObj;
    public GameObject Scenario;
    // Start is called before the first frame update
   // private string url = "https://csl-hcmc.com/api/get-access-properties?scenario=hcm_scenario_0"; 
    private WWW www = null;


    // Update is called once per frame
    public void OnClickOn()
    {
        Debug.Log(urlAddr());
        www = new WWW(urlAddr());
        StartCoroutine(ReceiveResponse());
        
    }
    public void OnClickOff()
    {
        fatherObj.SetActive(false);
        DestoyObject();
    }
    private void Gen(string name, int index)
    {
        GameObject go = GameObject.Instantiate(prefabButton);
        go.transform.SetParent(fatherObj.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.name = name;
        go.GetComponentInChildren<Text>().text = name;
        if (fatherObj.transform.childCount == 1)
        {
            go.transform.GetChild(1).GetComponent<SwitchManager>().isOn = true;
            go.transform.GetChild(1).GetComponent<SwitchManager>().UpdateUI();
            go.transform.GetChild(1).GetComponent<Button>().interactable = false;
            StartCoroutine(EventClick(index));
        }
        go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { clickClick(go, index);});
    }
    void clickClick(GameObject objClick, int index)
    {
        foreach (Transform child in fatherObj.transform)
        {
            if (child.transform.name != objClick.name)
            {
                child.GetChild(1).GetComponent<SwitchManager>().isOn = false;
                child.GetChild(1).GetComponent<Button>().interactable = true;
                child.GetChild(1).GetComponent<SwitchManager>().UpdateUI();
            }
            else
            {
                child.GetChild(1).GetComponent<Button>().interactable = false;
            }
        }
        StartCoroutine(EventClick(index));
    }

   // {
   // "option":"ACCESS",
   // "access_property_index": 2,
   // "mode":"ON",
   // "table": "hcm_scenario_0"
   // }



IEnumerator EventClick(int index)
    {
        string bodyJsonString = "{\"option\":\"ACCESS\"," + $"\"access_property_index\":\"{index}\"," + "\"mode\":\"ON\","+"\"table\":\"hcm_scenario_0\"}";
        var request = new UnityWebRequest($"https://csl-hcmc.com/api/set-option?", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }


    private string urlAddr()
    {
        float i = Scenario.GetComponent<Slider>().value;
        if (i == 1 || i == 2)
        {
            i++;
        }
        string name = "hcm_scenario_" + i;
        string link = $"https://csl-hcmc.com/api/get-access-properties?scenario="+name;
        return link;
    }

    private IEnumerator ReceiveResponse()
    {
        yield return www;
        JSONObject json = new JSONObject(www.text);

        string tmp = fixJson(json.ToString());
        Debug.Log(tmp);
        JsonData[] jdata = JsonHelper.FromJson<JsonData>(tmp);
        for(int i = 0; i < jdata.Length; i++)
        {
            Gen(jdata[i].name,jdata[i].index);
        }
        fatherObj.SetActive(true);
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    private void DestoyObject()
    {
        foreach(Transform child in fatherObj.transform)
        {
            if (child.transform.name != "Accessibility Button")
            {
                Destroy(child.gameObject);
            }
        }
    }
}
