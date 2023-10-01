using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndPlace : MonoBehaviour
{
    public GameObject placementPrefab;
    public Collider placementCollider;
    public GameObject[] instantiationPrefab;
    public Material placementMaterialGreen;
    public Material placementMaterialRed;
    public Renderer[] placementRenderer;

    private bool isPlacing = false;
    private int placementLayerMask;
    private int prefabType;

    public AudioSource flapSound;

    public ResourceManager resourceManager;
    public List<GameObject> prefabList = new List<GameObject>();

    void Start()
    {
        isPlacing = false;
        placementPrefab.SetActive(false);
        placementLayerMask = 1 << LayerMask.NameToLayer("Grass");
        placementRenderer[0].material = placementMaterialRed;
        placementRenderer[1].material = placementMaterialRed;
    }

    void Update()
    {
        if (isPlacing)
        {
            PlaceMode();
        }

        if (Input.GetMouseButtonDown(1) && isPlacing)
        {
            isPlacing = false;
            placementPrefab.SetActive(false);
        }

    }

    private void PlaceMode()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
        {
            placementPrefab.SetActive(true);
            placementPrefab.transform.position = hit.point;
            placementPrefab.transform.rotation = Quaternion.identity;


            Collider[] colliders = Physics.OverlapSphere(hit.point, 0.6f);
            bool canPlace = true;

            foreach (Collider collider in colliders)
            {
                if (collider != null && collider != placementCollider)
                {
                    if (placementCollider.bounds.Intersects(collider.bounds))
                    {
                        canPlace = false;
                        break;
                    }
                }
            }

            if (canPlace)
            {
                placementRenderer[0].material = placementMaterialGreen;
                placementRenderer[1].material = placementMaterialGreen;
            }
            else
            {
                placementRenderer[0].material = placementMaterialRed;
                placementRenderer[1].material = placementMaterialRed;
            }

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                flapSound.Play();
                GameObject temp = Instantiate(instantiationPrefab[prefabType + Random.Range(0, 3)], hit.point, Quaternion.identity);
                prefabList.Add(temp);
                temp.GetComponent<ResourceNode>().prefabType = prefabType;
                resourceManager.PayFor(prefabType);
                isPlacing = false;
                placementPrefab.SetActive(false);
            }
        }
        else
        {
            placementPrefab.SetActive(false);
        }
    }

    public void OnButtonClicked(int type)
    {
        prefabType = type;
        placementPrefab.SetActive(true);
        isPlacing = true;
    }

    internal void DeSpawn(float v)
    {

        for (int i = prefabList.Count - 1; i >= 0; i--)
        {
            GameObject obj = prefabList[i];

            if (obj.transform.position.y < v)
            {
                resourceManager.BackTrack(obj.GetComponent<ResourceNode>().prefabType);
                prefabList.RemoveAt(i);
                Destroy(obj);
            }
        }
    }

    public void Tick()
    {
        for (int i = prefabList.Count - 1; i >= 0; i--)
        {
            GameObject obj = prefabList[i];
            obj.GetComponent<ResourceNode>().OnTick();
        }
    }
}
