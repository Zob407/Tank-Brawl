using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public Renderer tankRenderer;
    public Material[] tankSkins;

    void Start()
    {
        if (tankRenderer == null)
        {
            tankRenderer = GetComponentInChildren<Renderer>();
        }

        int selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);

        if (SkinManager.instance != null)
        {
            selectedSkin = SkinManager.instance.selectedSkinIndex;
        }

        if (tankRenderer != null && selectedSkin >= 0 && selectedSkin < tankSkins.Length)
        {
            tankRenderer.material = tankSkins[selectedSkin];
        }
    }
}