using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveBagFormat
{
    public SaveBagFormat(string diceUID, int newAmount)
    {
        diceID = diceUID;
        amount = newAmount;
    }

    public string diceID;
    public int amount;
}

public class SaveManagerTest : MonoBehaviour
{
    private string BasePath = "";

    public DiceBag myBag;

    public List<SaveBagFormat> recreatedBag;

    public List<SaveBagFormat> tempBag = new List<SaveBagFormat>();

    public bool getBag;
    public bool getTemp;
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

        if (getTemp)
        {
            getTemp = false;
            tempBag = BagToSaveBag(myBag);
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
        recreatedBag = JsonUtility.FromJson<List<SaveBagFormat>>(data);
    }

    private void SaveBag()
    {
        var data = JsonUtility.ToJson(tempBag);

        var totalPath = BasePath + "/leBag.json";

        if (!File.Exists(totalPath))
        {
            File.Create(totalPath);
        }

        File.WriteAllText(totalPath, data);
    }

    public List<SaveBagFormat> BagToSaveBag(DiceBag bag)
    {
        var returnTempBag = new List<SaveBagFormat>();
        bool found = false;

        for (int i = 0; i < bag.bag.Count; i++)
        {
            found = false;

            for (int o = 0; o < returnTempBag.Count; o++)
            {
                if (returnTempBag[o].diceID == bag.bag[i].UID)
                {
                    returnTempBag[o].amount++;
                    found = true;
                }
            }

            if (!found)
            {
                returnTempBag.Add(new SaveBagFormat(bag.bag[i].UID, 1));
            }
        }

        return returnTempBag;
    }
}
