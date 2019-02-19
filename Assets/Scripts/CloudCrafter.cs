using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour {
    [Header("Set in Inspector")]
    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150,100,10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f;

    private GameObject[] cloudInstances;

    void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject Cloud;
        for (int i = 0; i < numClouds; i++) {
            Cloud = Instantiate<GameObject>(cloudPrefab);

            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);

            cPos.z = 100 - 90 * scaleU;

            Cloud.transform.position = cPos;
            Cloud.transform.localScale = Vector3.one * scaleVal;

            Cloud.transform.SetParent(anchor.transform);

            cloudInstances[i] = Cloud;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject Cloud in cloudInstances) {
            float scaleVal = Cloud.transform.localScale.x;
            Vector3 cPos = Cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            if (cPos.x <= cloudPosMin.x) {
                cPos.x = cloudPosMax.x;
            }

            Cloud.transform.position = cPos;
        }
	}
}
