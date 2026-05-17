using UnityEngine;

[System.Serializable]
public class SkinInfo
{
    public int Id;
    public bool IsUnlocked;
    public int Cost;
    public GameObject Skin;

    public SkinInfo(int id, bool isUnlocked, int cost, GameObject skin)
    {
        Id = id;
        IsUnlocked = isUnlocked;
        Cost = cost;
        Skin = skin;
    }
}
