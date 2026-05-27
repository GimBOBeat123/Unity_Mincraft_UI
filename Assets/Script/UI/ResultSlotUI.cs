using UnityEngine;
using UnityEngine.UI;

public class ResultSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    private ItemData currentResult;

    public void SetResult(ItemData item)
    {
        currentResult = item;

        icon.enabled = true;
        icon.sprite = item.icon;
    }

    public void Clear()
    {
        currentResult = null;
        icon.enabled = false;
    }
}