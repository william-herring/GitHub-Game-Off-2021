using Cinemachine;
using UnityEngine;

public class TroopSelectionManager : MonoBehaviour
{
    [SerializeField] private int currentSelectionIndex;
    [SerializeField] private GameObject[] playerTroopPrefabs;
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private MinimapCamFollow minimapCam;
    [SerializeField] private GameManager gm;

    public void SetSelection(int i)
    {
        currentSelectionIndex = i;
    }

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(playerTroopPrefabs[currentSelectionIndex], new Vector3(-35f, 4.75f, 0f), Quaternion.identity);
        
        playerCam.Follow = player.transform;
        playerCam.LookAt = player.transform;
        minimapCam.target = player.transform;

        player.GetComponent<PlayerHealth>().gm = gm;
        
        gameObject.SetActive(false);
    }
}
