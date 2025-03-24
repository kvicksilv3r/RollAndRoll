using System;
using System.IO;
using UnityEngine;

public class SaveManagerTest : MonoBehaviour
{
    private string BasePath = "";

    public DiceBag myBag;

    public DiceBag recreatedBag;

    public bool getBag;
    public bool saveBag;
    public bool loadBag;

    private void Start()
    {
        BasePath = Application.persistentDataPath;
    }

    private void OnValidate()
    {
        if (getBag)
        {
            getBag = false;
            myBag = DiceBagHelper.Instance.GetPlayerDiceBag();
        }

        if (saveBag)
        {
            saveBag = false;
            SaveBag();
        }

        if (loadBag)
        {
            loadBag = false;
            LoadBag();
        }
    }

    private void LoadBag()
    {
        var path = BasePath + "/leBag.json";

        if (!File.Exists(path))
        {
            return;
        }

        var data = File.ReadAllText(path);
        recreatedBag = JsonUtility.FromJson<DiceBag>(data);
    }

    private void SaveBag()
    {
        var data = JsonUtility.ToJson(myBag);

        var totalPath = BasePath + "/leBag.json";

        if (!File.Exists(totalPath))
        {
            File.Create(totalPath);
        }

        File.WriteAllText(totalPath, data);

    }
}
