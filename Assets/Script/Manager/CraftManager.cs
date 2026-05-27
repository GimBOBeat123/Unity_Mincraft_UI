using UnityEngine;

public class CraftManager : MonoBehaviour
{
    [SerializeField]
    private SlotUI[] craftSlots;

    [SerializeField]
    private ResultSlotUI resultSlot;

    [SerializeField]
    private RecipeData[] recipes;

    public void CheckRecipe()
    {
        ItemData[,] grid = new ItemData[3, 3];

        // 슬롯 -> 2차원 배열
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;

                grid[x, y] = craftSlots[index].CurrentItem;
            }
        }

        foreach (RecipeData recipe in recipes)
        {
            if (IsMatch(grid, recipe))
            {
                resultSlot.SetResult(recipe.output);
                return;
            }
        }

        resultSlot.Clear();
    }

    private bool IsMatch(ItemData[,] grid, RecipeData recipe)
    {
        int minX = 999;
        int minY = 999;
        int maxX = -1;
        int maxY = -1;

        // 사용된 범위 찾기
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (grid[x, y] != null)
                {
                    minX = Mathf.Min(minX, x);
                    minY = Mathf.Min(minY, y);

                    maxX = Mathf.Max(maxX, x);
                    maxY = Mathf.Max(maxY, y);
                }
            }
        }

        // 빈 조합칸
        if (maxX == -1)
            return false;

        int width = maxX - minX + 1;
        int height = maxY - minY + 1;

        if (width != recipe.width || height != recipe.height)
            return false;

        // 압축 비교
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ItemData current = grid[minX + x, minY + y];

                ItemData target =
                    recipe.pattern[y * recipe.width + x];

                if (current != target)
                    return false;
            }
        }

        return true;
    }
}