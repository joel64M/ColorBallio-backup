using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using GameAnalyticsSDK;
public class ScreenResolutionScript : MonoBehaviour
{
    public static ScreenResolutionScript instance;

    [Range(30,100)]     public int resolutionPercentage=75;      private void Start()     {
        if (!FB.IsInitialized)         {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);         }
         if (instance != null && instance != this)         {             Destroy(this.gameObject);             return;         }         else         {             instance = this;             SetResolution();
            GameAnalytics.Initialize();             DontDestroyOnLoad(this.gameObject);         }     }

    private void InitCallback()     {         if (FB.IsInitialized)         {             FB.ActivateApp();         }

    }      public void OnApplicationPause(bool pause)     {         if (!pause)         {             if (FB.IsInitialized)             {                 FB.ActivateApp();             }         }     }
    void SetResolution()     {         int height = (int)(Screen.currentResolution.height * resolutionPercentage) / 100;         int width = (int)(Screen.currentResolution.width * resolutionPercentage) / 100;         //print(width);         //print(height);         Screen.SetResolution(width, height, true);     }


    /*
     * 
// GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, //Application.version,GameManager.level.ToString());
     * */
}
