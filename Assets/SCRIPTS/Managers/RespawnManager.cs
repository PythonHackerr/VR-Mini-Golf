using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<GameObject> objList = new List<GameObject>();
    public List<GameObject> checkPointNumber = new List<GameObject>();
    public List<bool> canMove = new List<bool>();
    public List<int> listNumber = new List<int>();
    public List<float> _timer = new List<float>();
    public float refTimeBetweenTwoRespawn = 1;


    void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject obj in gos)
        {
            _timer.Add(refTimeBetweenTwoRespawn);
        }
    }

    public void AddObjectToRespawnList(GameObject newObj, GameObject checkpoint)
    {
        if (objList.Count == 0)
        {
            objList.Insert(0, newObj);
        }
        else
        {
            objList.Insert(objList.Count, newObj);
        }

        if (checkPointNumber.Count == 0)
        {
            checkPointNumber.Insert(0, checkpoint);
        }
        else
        {
            checkPointNumber.Insert(checkPointNumber.Count, checkpoint);
        }

        if (canMove.Count == 0)
        {
            canMove.Insert(0, true);
        }
        else
        {
            canMove.Insert(canMove.Count, true);
        }

        ObjectCanMove(newObj, checkpoint, canMove.Count - 1);

        Debug.Log("Checkpoint name: " + checkpoint.name);
        int tmpNum = int.Parse(checkpoint.name.Trim());
        bool b_AddValue = true;
        if (listNumber.Count == 0)
        {
            foreach (int num in listNumber)
            {
                if (num == tmpNum)
                    b_AddValue = false;
            }
            if (b_AddValue)
                listNumber.Insert(0, int.Parse(checkpoint.name));
        }
        else
        {
            foreach (int num in listNumber)
            {
                if (num == tmpNum)
                    b_AddValue = false;
            }
            if (b_AddValue)
                listNumber.Insert(listNumber.Count, int.Parse(checkpoint.name));
        }
    }


    public bool returnIfCanRespawn(GameObject newObj, GameObject checkpoint)
    {
        bool result = false;
        for (var i = 0; i < objList.Count; i++)
        {
            if (checkPointNumber[i] == checkpoint && objList[i] != newObj)
            {
                result = false;
                break;
            }
            else if (checkPointNumber[i] == checkpoint &&
                     objList[i] == newObj /*&& 
                     _timer[int.Parse(checkpoint.name)-1] == 0*/)
            {
                result = true;
                objList.RemoveAt(i);
                checkPointNumber.RemoveAt(i);
                canMove.RemoveAt(i);
                _timer[int.Parse(checkpoint.name) - 1] = refTimeBetweenTwoRespawn;
                break;
            }

        }
        return result;
    }


    public bool CanRespawn(Transform newObj)
    {
        bool collisionCheck = true;

        int cmpt = 0;
        Collider[] col = Physics.OverlapBox(newObj.position, new Vector3(.1F, .5f, .5F), newObj.rotation);

        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.tag == "Ball" && col[i].gameObject != newObj.gameObject)
            {
                cmpt++;
            }

        }
        if (cmpt == 0)
            collisionCheck = false;

        return collisionCheck;
    }


    void ObjectCanMove(GameObject newObj, GameObject checkpoint, int posNumber)
    {
        bool b_allowToMove = true;
        if (listNumber.Count == 0)
        {
            canMove[posNumber] = true;
        }

        for (int i = 0; i < listNumber.Count; i++)
        {
            if (listNumber[i] == int.Parse(checkpoint.name))
            {
                b_allowToMove = false;
                canMove[posNumber] = b_allowToMove;
                break;
            }
        }
    }


    public bool CanGoToRespawnPosition(GameObject newObj)
    {

        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i] == newObj && canMove[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    public bool UpdateObjectToRespawn(GameObject newObj)
    {
        int currentCheckpoint = 0;
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i] == newObj)
            {
                for (int j = 0; j < listNumber.Count; j++)
                {
                    if (listNumber[j] == int.Parse(checkPointNumber[i].name))
                    {
                        currentCheckpoint = listNumber[j];
                        listNumber.RemoveAt(j);
                    }

                }
                objList.RemoveAt(i);
                checkPointNumber.RemoveAt(i);
                canMove.RemoveAt(i);
            }
        }


        for (int i = 0; i < objList.Count; i++)
        {
            if (currentCheckpoint == int.Parse(checkPointNumber[i].name))
            {
                canMove[i] = true;
                listNumber.Add(currentCheckpoint);
                break;
            }
        }

        return true;
    }

}
