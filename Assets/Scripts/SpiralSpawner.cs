using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiralSpawner : MonoBehaviour
{
    public GameObject spiral;
    private float pillarHeight;
    public Queue<GameObject> spirals = new Queue<GameObject>();
    public int numberOfSpirals;
    public float spiralDownSpeed = 5f;
    
    void Start()
    {
        StartSpawning();
    }
    void Awake() {
        pillarHeight = spiral.transform.Find("Pillar").GetComponent<Renderer>().bounds.size.y;
        SetupInitialSpirals();
    }

    void SetupInitialSpirals()
    {
        for (int i=numberOfSpirals; i >= 1; i--)
        {
            // GameObject newSpiral = Instantiate(spiral, new Vector3(0, (-pillarHeight*i) + (numberOfSpirals*pillarHeight - pillarHeight), 0), Quaternion.identity);
            GameObject newSpiral = Instantiate(spiral, new Vector3(0, -pillarHeight*i + 15, 0), Quaternion.identity);

            spirals.Enqueue(newSpiral);
        }
    }

    void SpawnSpiral()
    {
        if(spirals.Count >= numberOfSpirals) {
            GameObject obj = spirals.Dequeue();
            Destroy(obj);
        }
        // Hack: The 0.1 syncs up the initial spirals to the new spawned ones. Spent 3 hours trying to fix. 
        GameObject newSpiral = Instantiate(spiral, new Vector3(0,pillarHeight-0.1f, 0), Quaternion.identity);
        Rigidbody rb = newSpiral.GetComponent<Rigidbody>();
        // newSpiral.GetComponent<Animator>().enabled = true;
        rb.velocity = new Vector3(0, -spiralDownSpeed, 0);
        spirals.Enqueue(newSpiral);
    }

    void StartSpawning()
    {
        float repeatTime = 2f;
        InvokeRepeating("SpawnSpiral", (repeatTime/2), repeatTime);
        foreach (GameObject spiral in spirals)
        {
            // spiral.GetComponent<Animator>().enabled = true;
            spiral.GetComponent<Rigidbody>().velocity = new Vector3(0, -spiralDownSpeed, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
