using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public Transform towerParent;
    public GameObject[] towers;
    public GameObject[] holograms;
    public int currentTower = 0;

    
    private void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.origin + mouseRay.direction * 1000f);
    }

    // Function to disable the GameObjects of all referenced holograms
    void DisableAllHolograms ()
    {
        foreach (var holo in holograms)
        {
            holo.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        DisableAllHolograms();

        // Creates the ray
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Performing raycast
        if (Physics.Raycast(mouseRay, out hit))
        {
            Placeable p = hit.collider.GetComponent<Placeable>();
            if (p && p.isAvailable)
            {
                // <<<<HOVERING>>>>>
                // Get hologram of current tower
                GameObject holo = holograms[currentTower];
                // Activate hologram
                holo.SetActive(true);
                // Position hologram to tile
                Vector3 placePoint = p.GetPivotPoint();
                holo.transform.position = placePoint;
                // <<<<PLACEMEMT>>>>
                // If LMB is down
                if (Input.GetMouseButtonDown(0))
                {
                    // Get current tower GameObject prefab
                    GameObject towerToSpawn = towers[currentTower];
                    // Spawn new clone of that tower
                    // Position new tower to placeable tile
                    Instantiate(towerToSpawn, placePoint, Quaternion.identity, towerParent);
                    // Tile is no longer placeable
                    p.isAvailable = false;
                }

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
