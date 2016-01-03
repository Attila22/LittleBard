using UnityEngine;
using System.Collections;

public class FragmentLookatCam : MonoBehaviour
{
    Transform targetCam;

    bool isActive = false;

    void Awake()
    {
		if(Camera.main!=null)
        	targetCam = Camera.main.transform;
    }

    public void SetActive(bool flag)
    {
        isActive = flag;
    }

    void Update()
    {
        if (!isActive)
            return;
        transform.LookAt(targetCam);
    }


}
