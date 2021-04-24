using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit
{
    public static class BaseStats
    {
        public static double MinHealth => 1720;                     // Poseidon
        public static double MaxHealth => 2590;                     // Ymir
        public static double RangeHealth => MaxHealth - MinHealth;
        public static double AverageHealth => 2049.33628318584;

        public static double MinHP5 => 13.6;                        // Hel
        public static double MaxHP5 => 26;                          // Ymir
        public static double RangeHP5 => MaxHP5 - MinHP5;
        public static double AverageHP5 => 20.5008849557522;

        public static double MinMana => 840;                        // Ymir - not including Cu Chulainn or Yemoja
        public static double MaxMana => 1510;                       // Janus
        public static double RangeMana => MaxMana - MinMana;
        public static double AverageMana => 1040.95495495495;

        public static double MinMP5 => 8.5;                         // Kali - not including Cu Chulainn or Yemoja
        public static double MaxMP5 => 17;                          // Baba Yaga
        public static double RangeMP5 => MaxMP5 - MinMP5;
        public static double AverageMP5 => 12.3864864864865;

        public static double MinPhysProt => 60;                     // Anubis
        public static double MaxPhysProt => 91;                     // Jormungandr              
        public static double RangePhysProt => MaxPhysProt - MinPhysProt;
        public static double AveragePhysProt => 72.3451327433628;

        public static double MinMagicProt => 48;                    // All Characters
        public static double MaxMagicProt => 48;                    // All Characters              
        public static double RangeMagicProt => MaxMagicProt - MinMagicProt;
        public static double AverageMagicProt => 48;

        public static double MinPhysPower => 76;                    // Bellona - not including characters with 0 physical power
        public static double MaxPhysPower => 96;                    // Hou Yi
        public static double RangePhysPower => MaxPhysPower - MinPhysPower;
        public static double AveragePhysPower => 83.6033898305085;

        public static double MinMagicPower => 305;                    // Aphrodite - not including characters with 0 magical power
        public static double MaxMagicPower => 415;                    // Ao Kuang
        public static double RangeMagicPower => MaxMagicPower - MinMagicPower;
        public static double AverageMagicPower => 331.203703703704;

        public static double MinAttackSpeed => 1;                   // Isis
        public static double MaxAttackSpeed => 1.48;                // Mercury
        public static double RangeAttackSpeed =>  MaxAttackSpeed - MinAttackSpeed;
        public static double AverageAttackSpeed => 1.2212389380531;

        public static double MinSpeed => 355;                       // He Bo
        public static double MaxSpeed => 381;                       // Mercury
        public static double RangeSpeed => MaxSpeed - MinSpeed;
        public static double AverageSpeed => 366.867256637168;

        //Average Lvl 20 [PhysPower] is 43.6513274336283

    }
}