using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectableTroopUI : MonoBehaviour
{
    public TroopSelectionManager troopSelectionManager;
    
    [SerializeField] private Color availableColor;
    [SerializeField] private Color unavailableColor;
    [SerializeField] private GameObject selectionIndicator;
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject[] otherSelectionIndicators;

    private bool _isAvailable;

    public int itemCost;
    public int index;

    private void Start()
    {
        _isAvailable = gm.credits >= itemCost;
        
        GetComponent<Image>().color = _isAvailable ? availableColor : unavailableColor;
    }

    public void OnClick()
    {
        if (!_isAvailable) return; //Returns to reduce nesting
        
        selectionIndicator.SetActive(true);
        troopSelectionManager.SetSelection(index);

        foreach (var i in otherSelectionIndicators)
        {
            i.SetActive(false);
        }

        gm.credits -= itemCost;
    } 
}
