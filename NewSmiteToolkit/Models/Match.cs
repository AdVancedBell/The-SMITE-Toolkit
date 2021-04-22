using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public class Match
    {
        /*
         
        {"ActiveId1":9041,"ActiveId2":8844,"Active_1":"Meditation Cloak","Active_2":"Blink Rune","Active_3":null,"Assists":8,"Ban1":"","Ban10":"","Ban10Id":0,"Ban1Id":0,"Ban2":"","Ban2Id":0,"Ban3":"","Ban3Id":0,"Ban4":"","Ban4Id":0,"Ban5":"","Ban5Id":0,"Ban6":"","Ban6Id":0,"Ban7":"","Ban7Id":0,"Ban8":"","Ban8Id":0,"Ban9":"","Ban9Id":0,
        "Creeps":52,"Damage":16421,"Damage_Bot":31599,"Damage_Done_In_Hand":3084,"Damage_Mitigated":21056,"Damage_Structure":453,"Damage_Taken":22567,"Damage_Taken_Magical":14415,"Damage_Taken_Physical":7505,"Deaths":2,"Distance_Traveled":410712,
        "First_Ban_Side":"","God":"Athena","GodId":1919,"Gold":13185,"Healing":1147,"Healing_Bot":0,"Healing_Player_Self":6312,"ItemId1":9633,"ItemId2":7641,"ItemId3":7791,"ItemId4":20224,"ItemId5":7832,"ItemId6":0,
        "Item_1":"Shoes of the Magi","Item_2":"Breastplate of Valor","Item_3":"Void Stone","Item_4":"Stone of Fal","Item_5":"Divine Ruin","Item_6":"","Killing_Spree":3,"Kills":5,"Level":16,"Map_Game":"Season 7 Joust 3v3",
        "Match":1149716241,"Match_Queue_Id":448,"Match_Time":"4\/20\/2021 2:26:47 AM","Minutes":21,"Multi_kill_Max":1,"Objective_Assists":4,"Queue":"Joust","Region":"NA","Skin":"Peacekeeper","SkinId":12791,"Surrendered":0,
        "TaskForce":1,"Team1Score":0,"Team2Score":0,"Time_In_Match_Seconds":1264,"Wards_Placed":2,"Win_Status":"Win","Winning_TaskForce":1,"playerId":709955972,"playerName":"Go Mallardd","ret_msg":null}

         */

        public string God { get; set; }
        public string Map_Game { get; set; }    // game mode

        public string Damage{ get; set; }
        public string Kills { get; set; }
        public string Assists { get; set; }
        public string Deaths { get; set; }

        public string Killing_Spree { get; set; }
        public string Multi_kill_Max { get; set; }

        public string Active_1 { get; set; }
        public string ActiveId1 { get; set; }
        public string Active_2 { get; set; }
        public string ActiveId2 { get; set; }

        public string Item_1 { get; set; }
        public string ItemId1 { get; set; }
        public string Item_2 { get; set; }
        public string ItemId2 { get; set; }
        public string Item_3 { get; set; }
        public string ItemId3 { get; set; }
        public string Item_4 { get; set; }
        public string ItemId4 { get; set; }
        public string Item_5 { get; set; }
        public string ItemId5 { get; set; }
        public string Item_6 { get; set; }
        public string ItemId6 { get; set; }

        public List<string> items 
        {
            get
            {
                return new List<string>() { Item_1, Item_2, Item_3, Item_4, Item_5, Item_6 };
            }
        }

        public List<string> actives
        {
            get
            {
                return new List<string>() { Active_1, Active_2 };
            }
        }

        public string Win_Status { get; set; }
        public string Surrendered { get; set; }
        public string Wards_Placed { get; set; }
    }
}