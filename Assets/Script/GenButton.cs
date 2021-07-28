using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GenButton : MonoBehaviour
{
    public GameObject prefabButton;
    public GameObject fatherObj;
    public GameObject Scenario;
    // Start is called before the first frame update
    private string url = "https://csl-hcmc.com/api/get-access-properties?scenario=hcm_scenario_0"; 
    private WWW www = null;



    // Update is called once per frame
    public void OnClickButton()
    {
        www = new WWW(urlAddr());
        StartCoroutine(ReceiveResponse());
        
    }
    public void OnClickOff()
    {
        fatherObj.SetActive(false);
        DestoyObject();
    }
    private void Gen(string name)
    {
        GameObject go = GameObject.Instantiate(prefabButton);
        go.transform.SetParent(fatherObj.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.name = name;
        go.GetComponentInChildren<Text>().text = name;
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

        JsonData[] jdata = JsonHelper.FromJson<JsonData>(tmp);
        for(int i = 0; i < jdata.Length; i++)
        {
            Gen(jdata[i].name);
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
