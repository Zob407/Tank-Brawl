using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;

    public int selectedSkinIndex = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectRed()
    {
        SelectSkin(0);
    }

    public void SelectBlue()
    {
        SelectSkin(1);
    }

    public void SelectYellow()
    {
        SelectSkin(2);
    }

    public void SelectGreen()
    {
        SelectSkin(3);
    }

    public void SelectTan()
    {
        SelectSkin(4);
    }

    void SelectSkin(int skinIndex)
    {
        selectedSkinIndex = skinIndex;

        PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
        PlayerPrefs.Save();

        Debug.Log("Selected tank skin: " + selectedSkinIndex);
    }
}