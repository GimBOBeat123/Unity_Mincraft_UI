using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerClickHandler
{
    public ItemData CurrentItem;

    [Header("UI")]
    [SerializeField]
    private Image icon;

    [Header("Start Item")]
    [SerializeField]
    private ItemData startItem;

    [Header("Slot Type")]
    [SerializeField]
    private SlotType slotType;

    public SlotType SlotType => slotType;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // 시작 아이템 세팅
        if (startItem != null)
        {
            SetItem(startItem);
        }
        else
        {
            icon.enabled = false;
        }
    }

    // 아이템 설정
    public void SetItem(ItemData item)
    {
        CurrentItem = item;

        if (item == null)
        {
            icon.enabled = false;
            icon.sprite = null;
            return;
        }

        icon.enabled = true;
        icon.sprite = item.icon;
    }

    // 슬롯 비우기
    public void Clear()
    {
        CurrentItem = null;

        icon.enabled = false;
        icon.sprite = null;
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentItem == null)
            return;

        // 드래그 아이콘 표시
        DragItemUI.Instance.Show(CurrentItem.icon);

        // 드래그 시작 슬롯 저장
        DragItemUI.Instance.SetDragSlot(this);

        // 반투명
        canvasGroup.alpha = 0.5f;
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {

    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 아이콘 숨김
        DragItemUI.Instance.Hide();

        // 원래 투명도
        canvasGroup.alpha = 1f;
    }

    // 드랍
    public void OnDrop(PointerEventData eventData)
    {
        SlotUI fromSlot = DragItemUI.Instance.DragSlot;

        if (fromSlot == null)
            return;

        // 자기 자신 제외
        if (fromSlot == this)
            return;

        // 인벤토리 -> 크래프트
        if (fromSlot.SlotType == SlotType.Inventory &&
           slotType == SlotType.Craft)
        {
            SetItem(fromSlot.CurrentItem);
        }
        else
        {
            // 일반 교환
            ItemData temp = CurrentItem;

            SetItem(fromSlot.CurrentItem);

            fromSlot.SetItem(temp);
        }

        // 조합 검사
        CraftManager craftManager = FindObjectOfType<CraftManager>();

        if (craftManager != null)
        {
            craftManager.CheckRecipe();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 우클릭
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Craft 슬롯만 비우기
            if (slotType == SlotType.Craft)
            {
                Clear();

                // 조합 다시 검사
                CraftManager craftManager = FindObjectOfType<CraftManager>();

                if (craftManager != null)
                {
                    craftManager.CheckRecipe();
                }
            }
        }
    }
}