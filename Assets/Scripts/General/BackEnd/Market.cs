using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    const int countLevels = 100;
    const int countSwords = 100;
    private long hash = 187533243;
    public Globals gs;
    private long money;
    private long money_backup1;
    public int currSword = 0;
    public bool[] levels = new bool[countLevels];
    public bool[] swords = new bool[countSwords];
    public bool[] magics = new bool[countSwords];
    public bool[] skins = new bool[countSwords];
    public int[] priceLevels;
    public int[] priceSwords;
    public int[] priceMagics;
    public int[] priceSkins;
    public ScrollElems selectLevel;
    public ScrollElems selectSwords;
    public ScrollElems selectMagic;
    public List<GameObject> MoneyForLevel;
    public List<GameObject> MoneyForSword;
    public List<GameObject> MoneyForMagic;
    public List<GameObject> MoneyForSkins;

    void Start()
    {
        money = Globals.money;
        Hash();
        levels = gs.levels;
        swords = gs.swords;
        magics = gs.magics;
        skins = gs.skins;
        for (int i = 0; i < MoneyForLevel.Count; ++i)
        {
            if (levels[i])
            {
                MoneyForLevel[i].SetActive(false);
            }
        }
        for (int i = 0; i < MoneyForSword.Count; ++i)
        {
            if (i == Globals.currSword)
            {
                MoneyForSword[i].GetComponent<Text>().text = "Current";
            }
            else if (swords[i])
            {
                MoneyForSword[i].SetActive(false);
            }
        }
        for (int i = 0; i < MoneyForMagic.Count; ++i)
        {
            if (i == Globals.currMagic)
            {
                MoneyForMagic[i].GetComponent<Text>().text = "Current";
            }
            else if (magics[i])
            {
                MoneyForMagic[i].SetActive(false);
            }
        }
        for (int i = 0; i < MoneyForMagic.Count; ++i)
        {
            if (i == Globals.currSkin)
            {
                MoneyForSkins[i].GetComponent<Text>().text = "Current";
            }
            else if (skins[i])
            {
                MoneyForSkins[i].SetActive(false);
            }
        }
    }

    private void Hash()
    {
        money_backup1 = money * 17 - hash;
    }

    private void ReturnToNormal()
    {
        money = (money_backup1 + hash) / 17;
    }
    private bool Check()
    {
        if (money != (money_backup1 + hash) / 17)
        {
            print(money);
            print((money_backup1 + hash) / 17);
            print(money_backup1);
            ReturnToNormal();
            return false;
        }
        return true;
    }

    public void BuyOrPlayLevel()
    {
        if (!levels[selectLevel.indexMinDist])
        {
            if (money >= priceLevels[selectLevel.indexMinDist])
            {
                if (Check())
                {
                    levels[selectLevel.indexMinDist] = true;
                    money -= priceLevels[selectLevel.indexMinDist];
                    gs.ChangeMoney(money);
                    Hash();
                    gs.levels[selectLevel.indexMinDist] = true;
                    Globals.currLevel = selectLevel.indexMinDist;
                    MoneyForLevel[selectLevel.indexMinDist].SetActive(false);
                    gs.WriteData();
                }
                else
                {
                    gs.Cheater.Invoke();
                }
            }
            else
            {

            }
        }
        else
        {
            PlayLevel.LoadLevel(selectLevel);
        }
    }

    public void BuyOrPlayLevel(int level)
    {
        selectLevel.indexMinDist = level;
        BuyOrPlayLevel();
    }
    public void BuyOrSelectSword(int sword)
    {
        selectSwords.indexMinDist = sword;
        BuyOrSelectSword();
    }
    public void BuyOrSelectMagic(int magic)
    {
        selectMagic.indexMinDist = magic;
        BuyOrSelectMagic();
    }

    public void BuyOrSelectSkin(int skin)
    {
        print(money);
        print((money_backup1 + hash) / 17);
        if (!skins[skin])
        {
            if (money >= priceSkins[skin])
            {
                if (Check())
                {
                    skins[skin] = true;
                    money -= priceSkins[skin];
                    gs.ChangeMoney(money);
                    Hash();
                    gs.skins[skin] = true;
                    print(skin);
                    MoneyForSkins[Globals.currSkin].SetActive(false);
                    Globals.currSkin = skin;
                    MoneyForSkins[skin].GetComponent<Text>().text = "Current";
                    gs.WriteData();
                    print("Bought and Selected");
                }
                else
                {
                    gs.Cheater.Invoke();
                }
            }
            else
            {
                //Globals.currSword = priceSwords[selectSwords.indexMinDist];
                print("no money");
                print(money);
                print(priceSkins[skin]);
            }
        }
        else
        {
            MoneyForSkins[Globals.currSkin].SetActive(false);
            Globals.currSkin = skin;
            MoneyForSkins[skin].SetActive(true);
            MoneyForSkins[skin].GetComponent<Text>().text = "Current";
            print(skin);
            gs.WriteData();
            print("Current");
        }
    }

    public void BuyOrSelectSword()
    {
        print(money);
        print((money_backup1 + hash) / 17);
        if (!swords[selectSwords.indexMinDist])
        {
            if (money >= priceSwords[selectSwords.indexMinDist])
            {
                if (Check())
                {
                    swords[selectSwords.indexMinDist] = true;
                    money -= priceSwords[selectSwords.indexMinDist];
                    gs.ChangeMoney(money);
                    Hash();
                    gs.swords[selectSwords.indexMinDist] = true;
                    print(selectSwords.indexMinDist);
                    MoneyForSword[Globals.currSword].SetActive(false);
                    Globals.currSword = selectSwords.indexMinDist;
                    MoneyForSword[selectSwords.indexMinDist].GetComponent<Text>().text = "Current";
                    gs.WriteData();
                    print("Bought and Selected");
                }
                else
                {
                    gs.Cheater.Invoke();
                }
            }
            else
            {
                //Globals.currSword = priceSwords[selectSwords.indexMinDist];
                print("no money");
                print(money);
                print(priceSwords[selectSwords.indexMinDist]);
            }
        }
        else
        {
            MoneyForSword[Globals.currSword].SetActive(false);
            Globals.currSword = selectSwords.indexMinDist;
            MoneyForSword[selectSwords.indexMinDist].SetActive(true);
            MoneyForSword[selectSwords.indexMinDist].GetComponent<Text>().text = "Current";
            print(selectSwords.indexMinDist);
            gs.WriteData();
            print("Current");
        }
    }

    public void BuyOrSelectMagic()
    {
        if (!magics[selectMagic.indexMinDist])
        {
            print(selectMagic.indexMinDist);
            if (money >= priceMagics[selectMagic.indexMinDist])
            {
                if (Check())
                {
                    magics[selectMagic.indexMinDist] = true;
                    money -= priceMagics[selectMagic.indexMinDist];
                    gs.ChangeMoney(money);
                    Hash();
                    gs.magics[selectMagic.indexMinDist] = true;
                    print(selectMagic.indexMinDist);
                    MoneyForMagic[Globals.currMagic].SetActive(false);
                    Globals.currMagic = selectMagic.indexMinDist;
                    MoneyForMagic[selectMagic.indexMinDist].GetComponent<Text>().text = "Current";
                    gs.WriteData();
                    print("Bought and Selected");
                }
                else
                {
                    gs.Cheater.Invoke();
                }
            }
            else
            {
                //Globals.currSword = priceSwords[selectSwords.indexMinDist];
                print("no money");
                print(money);
                print(priceMagics[selectMagic.indexMinDist]);
            }
        }
        else
        {
            MoneyForMagic[Globals.currMagic].SetActive(false);
            Globals.currMagic = selectMagic.indexMinDist;
            MoneyForMagic[Globals.currMagic].SetActive(true);
            MoneyForMagic[selectMagic.indexMinDist].GetComponent<Text>().text = "Current";
            print(selectMagic.indexMinDist);
            gs.WriteData();
            print("Current");
        }
    }

    public void BuyOrSelectSkins(int skin)
    {
        if (!skins[skin])
        {
            print(skin);
            if (money >= priceMagics[skin])
            {
                if (Check())
                {
                    skins[skin] = true;
                    money -= priceSkins[skin];
                    gs.ChangeMoney(money);
                    Hash();
                    gs.magics[skin] = true;
                    print(skin);
                    MoneyForSkins[Globals.currSkin].SetActive(false);
                    Globals.currSkin = skin;
                    MoneyForSkins[skin].GetComponent<Text>().text = "Current";
                    gs.WriteData();
                    print("Bought and Selected");
                }
                else
                {
                    gs.Cheater.Invoke();
                }
            }
            else
            {
                //Globals.currSword = priceSwords[selectSwords.indexMinDist];
                print("no money");
                print(money);
                print(priceSkins[skin]);
            }
        }
        else
        {
            MoneyForSkins[Globals.currSkin].SetActive(false);
            Globals.currSkin = skin;
            MoneyForSkins[Globals.currSkin].SetActive(true);
            MoneyForSkins[skin].GetComponent<Text>().text = "Current";
            print(skin);
            gs.WriteData();
            print("Current");
        }
    }

    public void PlayTutorial()
    {
        PlayLevel.LoadLevel(1);
    }
}
