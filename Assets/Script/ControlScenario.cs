using System.Collections;
using System.Collections.Generic;
using System.Text;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ControlScenario : MonoBehaviour
{
    public void control()
    {
        float i = GetComponent<Slider>().value;
        if (i == 1 || i == 2)
        {
            i++;
        }
        Debug.Log(i);
        string name = "hcm_scenario_" + i;
        Debug.Log(name);
        StartCoroutine(Scenario(name));
    }
    IEnumerator Scenario(string scenario)
    {
        string bodyJsonString = "{"+$"\"scenario\":\"{scenario}\"," + "\"table\":\"hcm_scenario_0\"}";
        var request = new UnityWebRequest($"https://csl-hcmc.com/api/choose-scenario?", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }
}
