using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public GameObject[] towers;
    public GameObject[] holograms;

    private int currentTower = 0;


    //testing
    private Vector3 placeablePoint;
    private void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.origin + mouseRay.direction * 1000f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(placeablePoint, .5f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Creates the ray
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Performing raycast
        if (Physics.Raycast(mouseRay, out hit))
        {
            Placeable p = hit.collider.GetComponent<Placeable>();
            if (p)
            {
                // Set placeable point for testing
                placeablePoint = p.transform.position;
            }
        }
	}

    public void SelectTower(int tower)
    {
        // Is tower within range of array 'towers'
        if (tower >= 0 && tower < towers.Length)
        {
            // Set currentTower to tower
            currentTower = tower;
        }
            
    }
}
