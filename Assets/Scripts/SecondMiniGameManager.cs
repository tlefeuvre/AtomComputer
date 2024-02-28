using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMiniGameManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<GameObject> UIPoints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = UIPoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < UIPoints.Count; i++)
        {
            Vector3 newPos = UIPoints[i].transform.position * 102.4f;
            
            lineRenderer.SetPosition(i, newPos);
        }
    }
}
