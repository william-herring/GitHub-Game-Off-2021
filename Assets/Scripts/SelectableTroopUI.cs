using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectableTroopUI : MonoBehaviour
{
    [SerializeField] private Color availableColor;
    [SerializeField] private Color unavailableColor;
    [SerializeField] private GameObject selectionIndicator;
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject[] otherSelectionIndicators;

    private bool isAvailable;

    public int itemCost;

    private void Start()
    {
        isAvailable = gm.credits >= itemCost;
        
        GetComponent<Image>().color = isAvailable ? availableColor : unavailableColor;
    }

    public void OnClick()
    {
        if (isAvailable)
        {
            selectionIndicator.SetActive(true);

            foreach (var i in otherSelectionIndicators)
            {
                i.SetActive(false);
            }
        }
    } 
}
