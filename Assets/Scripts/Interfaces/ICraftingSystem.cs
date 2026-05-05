public interface ICraftingSystem
{
    bool CanCraft(Recipe recipe);
    bool TryCraft(Recipe recipe);
}
