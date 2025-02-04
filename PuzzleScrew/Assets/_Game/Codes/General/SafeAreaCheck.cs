
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaCheck : MonoBehaviour
{
    private void Start()
    {
        float safeArea = Screen.safeArea.yMin;
        if (safeArea > 0)
        {
            GetComponent<RectTransform>().offsetMax = new Vector2(GetComponent<RectTransform>().offsetMax.x, -Screen.safeArea.yMin * (0.7f));
            //GetComponent<RectTransform>().offsetMin = new Vector2(GetComponent<RectTransform>().offsetMin.x, 25);

        }
    }
}
