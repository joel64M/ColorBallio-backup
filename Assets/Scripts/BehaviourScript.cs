using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class BehaviourScript : MonoBehaviour
{
    GameManagerScript gms;
    PathFollower pf;
    TrailRenderer tr;
    ParticleSystem ps;
    MeshRenderer mr;
    bool stopTrigger;

    Vector3 initialPos;

    float lastDistanceTravelled;

    [HideInInspector]
    public  float speed;

    void Start()
    {
         

        tr = GetComponentInChildren<TrailRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        mr = GetComponentInChildren<MeshRenderer>();
        pf = GetComponent<PathFollower>();

        gms = GameManagerScript.instance;

        initialPos = transform.position;
        speed = gms.speed;
        pf.speed = 0;
    }
    IEnumerator Respawn()
    {

        ps.Play();
        pf.speed = 0;
        mr.enabled = false;
        tr.Clear();
        tr.enabled = false;
        yield return new WaitForSeconds(.5f);


        mr.enabled = true;
      // transform.position = initialPos;

        pf.distanceTravelled = lastDistanceTravelled - 8f;
        pf.speed = speed;
        hit = false;
        yield return new WaitForSeconds(.1f);

        tr.Clear();
        tr.enabled = true;


    }
    bool hit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (stopTrigger)
            return;

        if (other.gameObject.CompareTag("Goal"))
        {
            stopTrigger = true;
            gms.CharacterReachedGoal(this.GetComponent<Statistics>());

            if(this.GetComponent<PlayerScript>()!=null)
                this.GetComponent<PlayerScript>().enabled = false;

            if (this.GetComponent<AIScript>() != null)
                this.GetComponent<AIScript>().enabled = false;

        }

        if (other.gameObject.CompareTag("Death"))
        {
            if (hit)
            {
                return;
            }
            hit = true;
            lastDistanceTravelled = pf.distanceTravelled;
            StartCoroutine(Respawn());
        }

    }
}
