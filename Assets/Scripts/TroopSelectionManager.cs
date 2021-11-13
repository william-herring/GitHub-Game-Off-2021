using UnityEngine;

public class TroopSelectionManager : MonoBehaviour
{
    [SerializeField] private int currentSelectionIndex;
    [SerializeField] private GameObject[] playerTroopPrefabs;

    public void SetSelection(int i)
    {
        currentSelectionIndex = i;
    }
}
