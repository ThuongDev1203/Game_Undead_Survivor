using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] tiltles;

    public void Lose()
    {
        tiltles[0].SetActive(true);
    }

    public void Win()
    {
        tiltles[1].SetActive(true);

    }
}
