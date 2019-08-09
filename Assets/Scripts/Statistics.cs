using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Statistics : MonoBehaviour
{
    public string playerName;
    public int rank;
    public float completion;

  public  bool isPlayer;


    Transform thisTransform;
    float goalZ, startZ, divZ;


    GameManagerScript gms;
    PathFollower pf;


    PlayerScript ps;
    AIScript ais;
    TextMesh tm;

    void Start()
    {

        tm = GetComponentInChildren<TextMesh>();
        gms = GameManagerScript.instance;
        pf = GetComponent<PathFollower>();
        if (isPlayer)
        {
            isPlayer = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowScript>().SetCamera(this.transform);

            UIManagerScript.instance.mainPlayerStats = this.GetComponent<Statistics>();
            ps = GetComponent<PlayerScript>();
            GetComponent<AIScript>().enabled = false;
        }
        else
        {
            isPlayer = false;
            ais = GetComponent<AIScript>();
            GetComponent<PlayerScript>().enabled = false;
        }
        //  gms.stats.Add(this.GetComponent<Statistics>());
        gms.AddToStats(GetComponent<Statistics>());
        tm.text = playerName;
        thisTransform = GetComponent<Transform>();
        startZ = thisTransform.position.z;
        goalZ = GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position.z;
        divZ = goalZ - startZ;


    }

    // Update is called once per frame
    void Update()
    {
        if (gms.isGameStart)
        {
            completion = Mathf.Round((pf.distanceTravelled/ pf.totalPathLength) *100);     // Mathf.Round(((thisTransform.position.z - startZ) / divZ)*100);
        }
    }
}
