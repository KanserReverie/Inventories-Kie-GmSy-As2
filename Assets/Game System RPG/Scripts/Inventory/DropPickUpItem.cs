using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUpItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private Camera camera;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {            
            // This will hold this seleced info of the item.
            //RaycastHit hitInfo;

            RaycastHit hit;
            // 50 is how far away the item is.

            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 5f))

                //if (Physics.Raycast(ray, out hitInfo, 50f))
            {
                DroppedItem droppedItem = hit.collider.gameObject.GetComponent<DroppedItem>();
                if (droppedItem != null)
                {
                    inventory.AddItem(droppedItem.item);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void DropItem()
    {
        if(inventory.selectedItem== null)
        {
            return;
        }

        GameObject mesh = inventory.selectedItem.Mesh;
        if(mesh != null)
        {
            GameObject spawnedMesh = Instantiate(mesh, null);
            spawnedMesh.transform.position = dropPoint.position;
            DroppedItem droppedItem = mesh.GetComponent<DroppedItem>();

            if (droppedItem == null)
            {
                droppedItem = spawnedMesh.AddComponent<DroppedItem>();
            }

            if(droppedItem !=null)
            {
                droppedItem.item = new Item(inventory.selectedItem, 1);
            }
        }

        inventory.selectedItem.Amount--;
        if(inventory.selectedItem.Amount <= 0)
        {
            inventory.RemoveItem(inventory.selectedItem);
            inventory.selectedItem = null;
        }
    }
}
