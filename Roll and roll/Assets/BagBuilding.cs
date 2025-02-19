using UnityEngine;

public class BagBuilding : MonoBehaviour
{
    public DiceBag bag = new DiceBag();

    public void MakeAndSaveBag()
    {
        bag = DiceCollectionHelper.Instance.GetUnlockedDice();

        DiceBagHelper.Instance.SaveDiceBag(bag);
    }
}
