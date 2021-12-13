using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRoller : MonoBehaviour
{
    private static int nScreens = 7;
    public GameObject[] creditScenes = new GameObject[nScreens];
    private static int swapCount = 0;
    private object editScenes;

    // Start is called before the first frame update
    void Start()
    {
        //For each credit scene, add a reference here:
        creditScenes[0] = GameObject.Find("Anderton_A_Credits");
        creditScenes[1] = GameObject.Find("Peterson_M_Credits");
        creditScenes[2] = GameObject.Find("B_Wheeler_Credits");
        creditScenes[3] = GameObject.Find("S_Sundar-Ganesh_Credits");
        creditScenes[4] = GameObject.Find("L_Cox_Credits");
        creditScenes[5] = GameObject.Find("J_Wentz-Cruz_Credits"); 
        creditScenes[6] = GameObject.Find("P_Westover_Credits");

        //Turn all scenes off
        for (int i = 0; i <nScreens; i++)
        {
            creditScenes[i].SetActive(false);
        }
        //Turn back on the "0th"
        creditScenes[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int CurrentScene = swapCount % nScreens;
            creditScenes[CurrentScene].SetActive(false);
            swapCount++;
            CurrentScene = swapCount % nScreens;
            creditScenes[CurrentScene].SetActive(true);
            Debug.Log(CurrentScene);

        }
    }
}
