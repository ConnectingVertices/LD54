using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject panel;

    public int MulchPrice;
    public int StonePrice;
    public int WoodPrice;
    public int sporeCost;
    public bool isFood;

    public ResourceManager resourceManager;
    public Button button;

    void Start()
    {
        panel.SetActive(false);
    }
    void Update()
    {

        button.interactable = !(MulchPrice > resourceManager.Mulch ||
                               StonePrice > resourceManager.Mulch ||
                               WoodPrice > resourceManager.Wood ||
                               sporeCost + resourceManager.UsedSpores > resourceManager.Spores);
        if(resourceManager.Food < 0)
        {
            if(!isFood)
                button.interactable = false;
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}
