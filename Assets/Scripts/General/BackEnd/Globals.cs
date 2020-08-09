using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Globals : MonoBehaviour
{
    private int version = 1;
    public const int countLevels = 100;
    public const int countSwords = 100;
    private long hash = 276543242;
    public static long money = 0;
    private static long money_backup1 = 0;
    private static bool cheater = false;
    BinaryReader sr;
    BinaryWriter sw;
    public UnityEvent MoneyChange;
    public UnityEvent Cheater;
    public static int currSword = 0;
    public static int currLevel = 0;
    public static int currMagic = 0;
    public static int currSkin = 0;
    public bool[] levels = new bool[countLevels];
    public bool[] swords = new bool[countSwords];
    public bool[] magics = new bool[countSwords];
    public bool[] skins = new bool[countSwords];
    public int[] stats = new int[countSwords];
    public static int entities = 0;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "\\extra.bin"))
        {
            if (version > 0)
            {
                sr = new BinaryReader(File.Open(Application.persistentDataPath + "\\extra.bin", FileMode.Open));
                //print("Opened");

                money = sr.ReadInt64();
                //print("Read money");
                money_backup1 = sr.ReadInt64();
                //print("Read money backup");
                cheater = sr.ReadBoolean();
                //print("Read cheat info");
                for (int i = 0; i < countLevels; ++i)
                {
                    levels[i] = sr.ReadBoolean();
                }
                //print("Read levels");
                for (int i = 0; i < countSwords; ++i)
                {
                    swords[i] = sr.ReadBoolean();
                }
                //print("Read swords");
                for (int i = 0; i < countSwords; ++i)
                {
                    magics[i] = sr.ReadBoolean();
                }
                //print("Read magics");
                for (int i = 0; i < countSwords; ++i)
                {
                    skins[i] = sr.ReadBoolean();
                }
                //print("Read skins");
                for (int i = 0; i < countSwords; ++i)
                {
                    stats[i] = sr.ReadInt32();
                }
                //print("Read stats");
                currSword = sr.ReadInt32();
                currLevel = sr.ReadInt32();
                currMagic = sr.ReadInt32();
                currSkin = sr.ReadInt32();
                sr.Close();
                MoneyChange.Invoke();
                if (!Check())
                {
                    cheater = true;
                }
                if (cheater)
                {
                    Cheater.Invoke();
                }
            }
        }
        else
        {
            levels[0] = true;
            swords[0] = true;
            magics[0] = true;
            skins[0] = true;
            Hash(money);
        }
    }

    private bool Check()
    {
        if (money != (money_backup1 + hash) / 23)
        {
            ReturnToNormal();
            return false;
        }
        return true;
    }

    private void ReturnToNormal()
    {
        money = (money_backup1 + hash) / 23;
    }

    public void ChangeMoney(long t)
    {
        money = t;
        Hash(t);
        MoneyChange.Invoke();
    }
    public void ChangeMoney(DeathCounter t)
    {
        money += t.totalScore;
        Hash(money);
        MoneyChange.Invoke();
    }

    private void Hash(long t)
    {
        money_backup1 = t * 23 - hash;
    }

    public void WriteData()
    {
        sw = new BinaryWriter(File.Open(Application.persistentDataPath + "\\extra.bin", FileMode.OpenOrCreate));
        sw.Write(money);
        sw.Write(money_backup1);
        sw.Write(cheater);
        for (int i = 0; i < countLevels; i++)
        {
            sw.Write(levels[i]);
        }
        for (int i = 0; i < countSwords; i++)
        {
            sw.Write(swords[i]);
        }
        for (int i = 0; i < countSwords; i++)
        {
            sw.Write(magics[i]);
        }
        for (int i = 0; i < countSwords; i++)
        {
            sw.Write(skins[i]);
        }
        for (int i = 0; i < countSwords; i++)
        {
            sw.Write(stats[i]);
        }
        sw.Write(currSword);
        sw.Write(currLevel);
        sw.Write(currMagic);
        sw.Write(currSkin);
        sw.Close();
    }

    public void DecEnt()
    {
        entities--;
    }

    public void OnDestroy()
    {
        entities = 0;
    }
}
