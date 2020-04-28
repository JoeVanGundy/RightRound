using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiralSpawner : MonoBehaviour
{
    public GameObject spiral;
    public GameObject spiralParent;


    private float pillarHeight;
    private Queue<GameObject> spirals = new Queue<GameObject>();
    public int numberOfSpirals;
    public float pillarMoveDownSpeed;

    public float spiralRotationsPerSecond;
    
    private float timeToWaitBetweenSpawns;


    
    void Start()
    {
        pillarHeight = spiral.transform.Find("Pillar").GetComponent<Renderer>().bounds.size.y;
        timeToWaitBetweenSpawns = pillarHeight/pillarMoveDownSpeed;
        SetUpInitialSpirals();
        // StartSpawning();
    }
    void Awake() {
    }

    void SetUpInitialSpirals() {
        for (int i=1; i <= numberOfSpirals; i++)
        {
            // Subtract pillar height to account for the center being in the middle of the object
            GameObject newSpiral = Instantiate(spiral, new Vector3(0, pillarHeight*i - pillarHeight/2, 0), Quaternion.identity);
            newSpiral.transform.parent = spiralParent.transform;
            spirals.Enqueue(newSpiral);
        }
    }

    IEnumerator SpawnSpiral()
    {
        while(true) {

            // Sync pillars: Without this the tower spawns +- one frame worth of difference resulting in space between towers.
            Vector3 firstSpiralPosition;
            if(spirals.Count == 0) {
                // If for some reason the initial towers aren't set up
                firstSpiralPosition = new Vector3(0,35,0);
            } else {
                // For every other spiral
                GameObject firstSpiral = spirals.Peek();
                firstSpiralPosition = firstSpiral.transform.position;
            }
            float firstSpiralOffset = pillarHeight*spirals.Count;
            Vector3 correctPosition = new Vector3(0,firstSpiralPosition.y + firstSpiralOffset,0);

            // Set up new pillar
            GameObject newSpiral = Instantiate(spiral, correctPosition, Quaternion.identity);
            newSpiral.transform.parent = spiralParent.transform;

            // Only allow a certain number of spirals
            if(spirals.Count >= numberOfSpirals) {
                GameObject obj = spirals.Dequeue();
                Destroy(obj);
            }
            spirals.Enqueue(newSpiral);

            // Set speed of spiral
            newSpiral.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, -pillarMoveDownSpeed, 0);

            yield return new WaitForSeconds(timeToWaitBetweenSpawns);
        }
    }

    void StartSpawning()
    {
        foreach (GameObject spiral in spirals)
        {
           spiral.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, -pillarMoveDownSpeed, 0); 
        }
        StartCoroutine(SpawnSpiral());
    }

    void Update() {
        if(Input.GetButtonDown("Submit")) {
            StartSpawning();
        }
        // foreach (GameObject spiral in spirals)
        // {
        //     spiral.transform.position += new Vector3(0, -10 * Time.deltaTime, 0);
        // }
    }

}
