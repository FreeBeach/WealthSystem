using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCheck : MonoBehaviour
{
    // Start is called before the first frame update
    bool open = false;
    int num = 1500;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update==" + Time.realtimeSinceStartup);
        num--;
        if(num==0)
            StartCoroutine(RunTest());
    }
    IEnumerator RunTest()
    {
        Debug.Log("RunTest=="+Time.frameCount);
        yield return null;
        Debug.Log("RunTest==" + Time.frameCount);
        float temTime = Time.realtimeSinceStartup;
        while ((temTime+3)> Time.realtimeSinceStartup)
        {

        }
        Debug.Log("RunTest==" + Time.frameCount);
        yield return new WaitForSeconds(2);
        Debug.Log("RunTest==" + Time.frameCount);
        Debug.Log("RunTest==END" );
    }
}
