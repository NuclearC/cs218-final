using UnityEngine;

public class Room3DoorController : MonoBehaviour
{
    [SerializeField] GameObject doorObject;
    private int destroyedCount = 0;
    private int requiredDestructions = 2;

    public void ReportPowerBoxDestroyed()
    {
        destroyedCount++;
        Debug.Log($"[Room3Door] PowerBox destroyed: {destroyedCount}/{requiredDestructions}");

        if (destroyedCount >= requiredDestructions)
        {
            Debug.Log("[Room3Door] All powerboxes destroyed, opening door.");
            if (doorObject) doorObject.SetActive(false); // or Destroy(doorObject);
        }
    }
}
