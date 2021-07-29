using System.Collections;
using System.Collections.Generic;
using System.Text;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostData : MonoBehaviour
{
    public GameObject Interactive;
    private void Start()
    {
        
         StartCoroutine(ExampleCoroutine());
        #region Off All

        #endregion
    }
    #region Rotate
    public void RotateOn()
    {
        StartCoroutine(EventOn("ROTATE"));
    }
    public void RotateOff()
    {
        StartCoroutine(EventOff("ROTATE"));
    }
    #endregion

    #region Grid
    public void GridOn()
    {
        StartCoroutine(EventOn("GRID"));
    }
    public void GridOff()
    {
        StartCoroutine(EventOff("GRID"));
    }
    public void onClickGrid()
    {
        if (Interactive.GetComponent<SwitchManager>().isOn == true)
        {
            StartCoroutine(EventOff("GRID"));
        }
        else
            StartCoroutine(EventOn("GRID"));
    }
    #endregion

    #region ABM
    public void ABMOn()
    {
        StartCoroutine(EventOn("ABM"));
    }
    public void ABMOff()
    {
        StartCoroutine(EventOff("ABM"));
    }
    #endregion

    #region Geojson
    public void GEOJSONOn()
    {
        StartCoroutine(EventOn("GEOJSON"));
    }
    public void GEOJSONOff()
    {
        StartCoroutine(EventOff("GEOJSON"));
    }
    #endregion

    #region Aggregated Trips
    public void AggregatedTripsOn()
    {
        StartCoroutine(EventOn("AGGREGATED_TRIPS"));
    }
    public void AggregatedTripsOff()
    {
        StartCoroutine(EventOff("AGGREGATED_TRIPS"));
    }
    #endregion

    #region Access
    public void AccessOn()
    {
        StartCoroutine(EventOn("ACCESS"));
    }
    public void AccessOff()
    {
        StartCoroutine(EventOff("ACCESS"));
    }
    #endregion

    #region Textual
    public void TextualOn()
    {
        StartCoroutine(EventOn("TEXTUAL"));
    }
    public void TextualOff()
    {
        StartCoroutine(EventOff("TEXTUAL"));
    }
    #endregion

    #region Shadows
    public void ShadowsOn()
    {
        StartCoroutine(EventOn("SHADOWS"));
    }
    public void ShadowsOff()
    {
        StartCoroutine(EventOff("SHADOWS"));
    }
    #endregion
   
    IEnumerator EventOn(string method)
    {
        string bodyJsonString = "{\"table\":\"hcm_scenario_0\"," + $"\"option\":\"{method}\"," + "\"mode\":\"ON\"}";
        var request = new UnityWebRequest($"https://csl-hcmc.com/api/set-option?", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }
    IEnumerator EventOff(string method)
    {
        string bodyJsonString = "{\"table\":\"hcm_scenario_0\"," + $"\"option\":\"{method}\"," + "\"mode\":\"OFF\"}";
        var request = new UnityWebRequest($"https://csl-hcmc.com/api/set-option?", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1f);
        GridOff();
        //yield return new WaitForSeconds(1.5f);
        //RotateOff();
        yield return new WaitForSeconds(1f);
        ABMOff();
        yield return new WaitForSeconds(0.5f);
        GEOJSONOff();
        yield return new WaitForSeconds(0.5f);
        AggregatedTripsOff();
        yield return new WaitForSeconds(0.5f);
        AccessOff();
        yield return new WaitForSeconds(0.5f);
        TextualOff();
        yield return new WaitForSeconds(0.5f);
        ShadowsOff();
        yield return new WaitForSeconds(0.5f);
      
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}