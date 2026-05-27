using UnityEngine;
using UnityEngine.UI;

public class DragItemUI : MonoBehaviour
{
    public static DragItemUI Instance;

    [SerializeField]
    private Image icon;

    public SlotUI DragSlot;

    private void Awake()
    {
        Instance = this;

        Hide();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void Show(Sprite sprite)
    {
        icon.enabled = true;
        icon.sprite = sprite;
    }

    public void Hide()
    {
        icon.enabled = false;

        DragSlot = null;
    }

    public void SetDragSlot(SlotUI slot)
    {
        DragSlot = slot;
    }
}