using UnityEngine;

[CreateAssetMenu(menuName = "Craft/Recipe")]
public class RecipeData : ScriptableObject
{
    public ItemData[] pattern;
    public int width;
    public int height;

    public ItemData output;
}