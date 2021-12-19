using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AD : MonoBehaviour
{

    string gameId = "4243647";
    string placementId = "Bottom";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Advertisement.Initialize(gameId);
        while (!Advertisement.IsReady(placementId))
          yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId);
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
