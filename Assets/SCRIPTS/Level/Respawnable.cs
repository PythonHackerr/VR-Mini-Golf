using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Respawnable : MonoBehaviour
{
    public Transform checkpointsParent;
    public Transform collidersParent;
    public bool b_Pause = false;
    public GameObject RespawnEffect;
    public bool isRespawning = false;
    public RespawnManager respawnManager;

    Collider[] colliders;
    Transform[] checkpoints;
    Rigidbody rb;
    float t = 0;
    GameObject thisObject;


    void Start()
    {
        thisObject = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
        colliders = collidersParent.GetComponentsInChildren<Collider>();
        checkpoints = checkpointsParent.GetComponentsInChildren<Transform>().Where(t => t != checkpointsParent.transform).ToArray();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RespawnZone")
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        if (!isRespawning)
        {
            if (respawnManager) respawnManager.AddObjectToRespawnList(thisObject, FindClosestCheckpoint().gameObject);
            isRespawning = true;
            StopCoroutine("RepawnInitialization");
            StartCoroutine("RepawnInitialization");
        }
    }

    IEnumerator RepawnInitialization()
    {
        DeactivateObject();

        Transform cloud = Instantiate(RespawnEffect.transform);
        cloud.transform.position = gameObject.transform.position;

        Vector3 TargetPos = transform.position;
        Vector3 TargetEuler = transform.eulerAngles;

        Vector3 targetPos = FindClosestCheckpoint().position;
        Quaternion targetQuaternion = FindClosestCheckpoint().rotation;

        // Wait if multiple objects must resapwn on the same checkPoint
        yield return new WaitUntil(() => respawnManager.CanGoToRespawnPosition(thisObject) == true);
        t = 0;
        float timeToMove = 1;

        // Move to the closest Checkpoint
        while (t < timeToMove)
        {
            if (!b_Pause)
            {
                t = Mathf.MoveTowards(t, timeToMove, Time.deltaTime / timeToMove);
            }
            yield return null;
        }

        timeToMove = 1;
        t = 0;

        // Move to the closest Checkpoint
        while (t < timeToMove)
        {
            if (!b_Pause)
            {
                t = Mathf.MoveTowards(t, timeToMove, Time.deltaTime / timeToMove);

                TargetPos = Vector3.Lerp(TargetPos, targetPos + new Vector3(0, .5F, 0), 1f - Mathf.Cos(t * Mathf.PI * 0.5f));
                transform.position = TargetPos;

                transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, 1f - Mathf.Cos(t * Mathf.PI * 0.5f));

            }
            yield return null;
        }

        Destroy(cloud.gameObject);

        // whait until the object can respawn
        yield return new WaitUntil(() => respawnManager.CanRespawn(transform) == false);
        yield return new WaitUntil(() => respawnManager.UpdateObjectToRespawn(thisObject) == true);
        ActivateObject();
        timeToMove = 1;
        t = 0;


        while (t < timeToMove)
        {
            if (!b_Pause)
            {
                t = Mathf.MoveTowards(t, timeToMove, Time.deltaTime / timeToMove);
            }
            yield return null;
        }
        isRespawning = false;

        yield return null;
    }

    Transform FindClosestCheckpoint()
    {
        if (checkpoints.Length > 0)
        {
            Transform closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            Vector3 diff;
            float curDistance;
            foreach (Transform checkpoint in checkpoints)
            {
                diff = checkpoint.position - position;
                curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = checkpoint;
                    distance = curDistance;
                }
            }
            return closest;
        }
        else
        {
            return null;
        }
    }

    public void DeactivateObject()
    {
        if (rb) rb.isKinematic = true;

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    public void ActivateObject()
    {
        StartCoroutine("I_ActivateObject");
    }

    IEnumerator I_ActivateObject()
    {
        rb.isKinematic = false;
        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }
        yield return null;
    }
}
