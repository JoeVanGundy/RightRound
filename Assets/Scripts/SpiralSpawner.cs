using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiralSpawner : MonoBehaviour
{
    public GameObject spiral;
    public Queue<GameObject> spirals = new Queue<GameObject>();
    public float yRotation = 0;
    public int numberOfSpirals = 5;
    void Start()
    {
        float repeatTime = spiral.transform.Find("Pillar").GetComponent<Renderer>().bounds.size.y/5;
        // Hack: Invoke repeating was off by one frame. So wait one less frame than normal to start
        InvokeRepeating("SpawnSpiral", repeatTime-Time.deltaTime, repeatTime);
        
    }
    void Awake() {
         SetupInitialSpirals();
    }

    void SetupInitialSpirals()
    {
        for (int i=numberOfSpirals; i > 0; i--)
        {
            GameObject spawned = Instantiate(spiral, new Vector3(0, (-10*i) + 5, 0), Quaternion.identity);
            Rigidbody rb = spawned.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, -5, 0);
            spawned.transform.eulerAngles = new Vector3(
                spawned.transform.eulerAngles.x,
                yRotation,
                spawned.transform.eulerAngles.z
            );
            spirals.Enqueue(spawned);
        }
    }

    void SpawnSpiral()
    {
        GameObject spawned = Instantiate(spiral, new Vector3(0, -5, 0), Quaternion.identity);
        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -5, 0);
        spawned.transform.eulerAngles = new Vector3(
            spawned.transform.eulerAngles.x,
            yRotation,
            spawned.transform.eulerAngles.z
        );
        spirals.Enqueue(spawned);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject obj in spirals)
        {
            obj.transform.eulerAngles = new Vector3(
                obj.transform.eulerAngles.x,
                yRotation,
                obj.transform.eulerAngles.z
            );
        }
        if(yRotation >= 360) {
            yRotation = 0;
        }
        yRotation += 1;

        if(spirals.Count >= numberOfSpirals) {
            GameObject obj = spirals.Dequeue();
            Destroy(obj);
        }
    }
}
