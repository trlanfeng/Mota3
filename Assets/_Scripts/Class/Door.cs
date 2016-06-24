using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public enum doorType { red, yellow, blue, metal, big, bighead };
    public doorType type;
    public bool hasDoorProtector;
    //public GameObject[] protectorGameObject;
    public List<GameObject> protectorGameObject;
    void Start()
    {
        if (type == doorType.big || type == doorType.bighead)
        {
            hasDoorProtector = true;
        }
    }
}
