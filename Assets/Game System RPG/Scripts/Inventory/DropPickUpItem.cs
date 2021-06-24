using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUpItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private Camera camera;

    [SerializeField] private GameObject weapon;

    [SerializeField] private GameObject hat;

    [SerializeField] private GameObject food;

    [SerializeField] private GameObject CantUse;

    private void Update()
    {
        Debug.DrawRay(transform.position+new Vector3(0,1.5f,0), transform.forward * 1f, Color.red);

        if (Input.GetKeyDown(KeyCode.F))
        {            
            // This will hold this seleced info of the item.
            //RaycastHit hitInfo;

            RaycastHit hit;
            // 50 is how far away the item is.
            
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1.5f))

                //if (Physics.Raycast(ray, out hitInfo, 50f))
            {
                Debug.Log(hit.transform.name);
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
    public void UseItem()
    {
        if (inventory.selectedItem == null)
        {
            return;
        }
        else if (inventory.selectedItem.Type == Item.ItemType.Weapon)
        {
            if(!weapon.gameObject.activeSelf)
                weapon.gameObject.SetActive(true);
        }
        else if (inventory.selectedItem.Type == Item.ItemType.Hat)
        {
            if (!hat.gameObject.activeSelf)
                hat.gameObject.SetActive(true);
        }
        else if (inventory.selectedItem.Type == Item.ItemType.Food)
        {

            if (!food.gameObject.activeSelf)
                food.gameObject.SetActive(true);
            inventory.selectedItem.Amount--;
            if (inventory.selectedItem.Amount <= 0)
            {
                inventory.RemoveItem(inventory.selectedItem);
                inventory.selectedItem = null;
                food.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!CantUse.gameObject.activeSelf)
                CantUse.gameObject.SetActive(true);
        }
    }
}
