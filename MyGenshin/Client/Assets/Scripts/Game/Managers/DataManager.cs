using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System;
using System.IO;

using Common.Data;

using Newtonsoft.Json;


namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        public Dictionary<int, MapDefine>                         Maps        = null;
        public Dictionary<int, CharacterDefine>                   Characters  = null;
        public Dictionary<int, TeleporterDefine>                  Teleporters = null;
        public Dictionary<int, Dictionary<int, SpawnPointDefine>> SpawnPoints = null;
        public Dictionary<int, NPCDefine>                         NPCs        = null;
        public Dictionary<int, ItemDefine>                        Items       = null;
        public Dictionary<int, TalkDefine>                        Talks       = null;
        public Dictionary<int, StoreDefine>                       Stores      = null;
        public Dictionary<int, GoodsDefine>                       Goods       = null;
        public Dictionary<int, EquipDefine>                       Equips      = null;
        public Dictionary<int, PropertyDefine>                    Properties  = null;
        public Dictionary<int, SpecialDefine>                     Specials    = null;
        public Dictionary<int, QuestDefine>                       Quests      = null;
        public Dictionary<int, ChapterDefine>                     Chapters    = null;
        public DataManager()
        {
#if UNITY_EDITOR
            Load();
#endif
            Debug.LogFormat("DataManager > DataManager()");

        }

        public void Load()
        {
            string json = File.ReadAllText(SysDefine.PATH_DEFINE_MAP);
            this.Maps = JsonConvert.DeserializeObject<Dictionary<int, MapDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_CHARACTER);
            this.Characters = JsonConvert.DeserializeObject<Dictionary<int, CharacterDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_TELEPORTER);
            this.Teleporters = JsonConvert.DeserializeObject<Dictionary<int, TeleporterDefine>>(json);

            //json = File.ReadAllText(SysDefine.PATH_DEFINE_SPAWNPOINT);
            //this.SpawnPoints = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, SpawnPointDefine>>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_NPC);
            this.NPCs = JsonConvert.DeserializeObject<Dictionary<int, NPCDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_TALK);
            Talks = JsonConvert.DeserializeObject<Dictionary<int, TalkDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_STORE);
            Stores = JsonConvert.DeserializeObject<Dictionary<int, StoreDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_GOODS);
            Goods = JsonConvert.DeserializeObject<Dictionary<int, GoodsDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_ITEM);
            this.Items = JsonConvert.DeserializeObject<Dictionary<int, ItemDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_EQUIP);
            this.Equips = JsonConvert.DeserializeObject<Dictionary<int, EquipDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_PROPERTY);
            this.Properties = JsonConvert.DeserializeObject<Dictionary<int, PropertyDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_SPECIAL);
            Specials = JsonConvert.DeserializeObject<Dictionary<int, SpecialDefine>>(json);

            json = File.ReadAllText(SysDefine.PATH_DEFINE_QUEST);
            Quests = JsonConvert.DeserializeObject<Dictionary<int, QuestDefine>>(json);
        }


        public IEnumerator LoadData(Action< string ,long,long> addData)

        {
            int total = 13;

            string json = File.ReadAllText(SysDefine.PATH_DEFINE_MAP);
            this.Maps = JsonConvert.DeserializeObject<Dictionary<int, MapDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_CHARACTER);
            this.Characters = JsonConvert.DeserializeObject<Dictionary<int, CharacterDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_TELEPORTER);
            this.Teleporters = JsonConvert.DeserializeObject<Dictionary<int, TeleporterDefine>>(json);
            addData(null, 1, total);
            yield return null;

            //json = File.ReadAllText(SysDefine.PATH_DEFINE_SPAWNPOINT);
            //this.SpawnPoints = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, SpawnPointDefine>>>(json);
            //yield return null;

            json = File.ReadAllText(SysDefine.PATH_DEFINE_NPC);
            this.NPCs = JsonConvert.DeserializeObject<Dictionary<int, NPCDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_TALK);
            Talks = JsonConvert.DeserializeObject<Dictionary<int, TalkDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_STORE);
            Stores = JsonConvert.DeserializeObject<Dictionary<int, StoreDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_GOODS);
            Goods = JsonConvert.DeserializeObject<Dictionary<int, GoodsDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_ITEM);
            this.Items = JsonConvert.DeserializeObject<Dictionary<int, ItemDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_EQUIP);
            this.Equips = JsonConvert.DeserializeObject<Dictionary<int, EquipDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_PROPERTY);
            this.Properties = JsonConvert.DeserializeObject<Dictionary<int, PropertyDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_SPECIAL);
            Specials = JsonConvert.DeserializeObject<Dictionary<int, SpecialDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_QUEST);
            Quests = JsonConvert.DeserializeObject<Dictionary<int, QuestDefine>>(json);
            addData(null, 1, total);
            yield return null;
            json = File.ReadAllText(SysDefine.PATH_DEFINE_CHAPTER);
            Chapters = JsonConvert.DeserializeObject<Dictionary<int, ChapterDefine>>(json);
            addData("�ļ���ȡ���", 1, total);
            yield return new WaitForSeconds(0.5f);

        }

#if UNITY_EDITOR
        public void SaveTeleporters()
        {
            string json = JsonConvert.SerializeObject(this.Teleporters, Formatting.Indented);
            File.WriteAllText(SysDefine.PATH_DEFINE_TELEPORTER, json);
        }

        public void SaveSpawnPoints()
        {
            string json = JsonConvert.SerializeObject(this.SpawnPoints, Formatting.Indented);
            File.WriteAllText(SysDefine.PATH_DEFINE_SPAWNPOINT, json);
        }

#endif
    }
}