using UnityEngine;
using UnityEngine.EventSystems;

public class CaptainSlot : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CaptainSlotManager.Instance.PlaceCaptainCard(transform);
    }
}
