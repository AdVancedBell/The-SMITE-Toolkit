using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public class Menuitem
    {
        [Key]
        public int id { get; set; }

        public string description { get; set; }
        public string value { get; set; }
    }

    public class Rankitem
    {
        [Key]
        public int id { get; set; }

        public string description { get; set; }
        public string value { get; set; }
    }

    public class AbilityDescription
    {
        [Key]
        public int id { get; set; }

        public string description { get; set; }
        public string secondaryDescription { get; set; }
        public List<Menuitem> menuitems { get; set; }
        public List<Rankitem> rankitems { get; set; }
        public string cooldown { get; set; }
        public string cost { get; set; }
    }

    public class AbilityRoot
    {
        [Key]
        public int id { get; set; }

        public AbilityDescription itemDescription { get; set; }
    }

    public class Ability
    {
        public AbilityRoot Description { get; set; }
        public int Id { get; set; }
        public string Summary { get; set; }
        public string URL { get; set; }
    }

    public class God
    {
        public double Lvl20Health => Health + (HealthPerLevel * 20);
        public double HealthBarPercentage =>  Math.Min(100, ((-2 * BaseStats.AverageHealth) + Lvl20Health + BaseStats.MaxHealth) / BaseStats.RangeHealth * 100);

        public double Lvl20HP5 => HealthPerFive + (HP5PerLevel * 20);
        public double HP5BarPercentage => Math.Min(100, ((-2 * BaseStats.AverageHP5) + Lvl20HP5 + BaseStats.MaxHP5) / BaseStats.RangeHP5 * 100);

        public double Lvl20Mana => Mana + (ManaPerLevel * 20);
        public double ManaBarPercentage => Lvl20Mana < BaseStats.MinMana ? 0 : Math.Min(100, ((-2 * BaseStats.AverageMana) + Lvl20Mana + BaseStats.MaxMana) / BaseStats.RangeMana * 100);

        public double Lvl20MP5 => ManaPerFive + (MP5PerLevel * 20);
        public double MP5BarPercentage => Lvl20MP5 < BaseStats.MinMP5 ? 0 : Math.Min(100, ((-2 * BaseStats.AverageMP5) + Lvl20MP5 + BaseStats.MaxMP5) / BaseStats.RangeMP5 * 100);

        public double Lvl20PhysProt => PhysicalProtection + (PhysicalProtectionPerLevel * 20);
        public double PhysProtBarPercentage => Math.Min(100, ((-2 * BaseStats.AveragePhysProt) + Lvl20PhysProt + BaseStats.MaxPhysProt) / BaseStats.RangePhysProt * 100);

        public double Lvl20MagicProt => MagicProtection + (MagicProtectionPerLevel * 20);
        public double MagicProtBarPercentage => Math.Min(100, ((-2 * BaseStats.AverageMagicProt) + Lvl20MagicProt + BaseStats.MaxMagicProt) / BaseStats.RangeMagicProt * 100);

        public double Lvl20PhysPower => PhysicalPower + (PhysicalPowerPerLevel * 20);
        public double PhysPowerBarPercentage => Lvl20PhysPower < BaseStats.MinPhysPower ? 0 : Math.Min(100, ((-2 * BaseStats.AveragePhysPower) + Lvl20PhysPower + BaseStats.MaxPhysPower) / BaseStats.RangePhysPower * 100);

        public double Lvl20MagicPower => MagicalPower + (MagicalPowerPerLevel * 20);
        public double MagicPowerBarPercentage => Lvl20MagicPower < BaseStats.MinMagicPower ? 0 : Math.Min(100, ((-2 * BaseStats.AverageMagicPower) + Lvl20MagicPower + BaseStats.MaxMagicPower) / BaseStats.RangeMagicPower * 100);

        public double Lvl20AttackSpeed => AttackSpeed + (AttackSpeedPerLevel * 20);
        public double AttackSpeedBarPercentage => Math.Min(100, ((-2 * BaseStats.AverageAttackSpeed) + Lvl20AttackSpeed + BaseStats.MaxAttackSpeed) / BaseStats.RangeAttackSpeed * 100);

        public double SpeedBarPercentage => Math.Min(100, ((-2 * BaseStats.AverageSpeed) + Speed + BaseStats.MaxSpeed) / BaseStats.RangeSpeed * 100);

        public string FormattedLore => Regex.Replace(Lore, @"\\n", @"<br />");

        // deserialized from api response (stored in DB) vvv
        public int id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Pantheon { get; set; }
        public string Type { get; set; }
        public string Roles { get; set; }
        public string Pros { get; set; }
        public string Lore { get; set; }

        public string godCard_URL { get; set; }
        public string godIcon_URL { get; set; }

        public int Health { get; set; }
        public Double HealthPerLevel { get; set; }
        public Double Speed { get; set; }
        [Display(Name = "HP5")]
        public Double HealthPerFive { get; set; }
        public Double HP5PerLevel { get; set; }
        public Double Mana { get; set; }
        public Double ManaPerLevel { get; set; }
        [Display(Name = "MP5")]
        public Double ManaPerFive { get; set; }
        public Double MP5PerLevel { get; set; }
        [Display(Name = "Physical Protection")]
        public Double PhysicalProtection { get; set; }
        public Double PhysicalProtectionPerLevel { get; set; }
        [Display(Name = "Magical Protection")]
        public Double MagicProtection { get; set; }
        public Double MagicProtectionPerLevel { get; set; }
        [Display(Name = "Physical Power")]
        public Double PhysicalPower { get; set; }
        public Double PhysicalPowerPerLevel { get; set; }
        [Display(Name = "Magical Power")]
        public Double MagicalPower { get; set; }
        public Double MagicalPowerPerLevel { get; set; }
        [Display(Name = "Attack Speed")]
        public Double AttackSpeed { get; set; }
        public Double AttackSpeedPerLevel { get; set; }

        public AbilityRoot basicAttack { get; set; }
        public Ability Ability_1 { get; set; }
        public Ability Ability_2 { get; set; }
        public Ability Ability_3 { get; set; }
        public Ability Ability_4 { get; set; }
        public Ability Ability_5 { get; set; }          // Passive

        public List<Ability> Abilities
        { 
            get
            {
                return new List<Ability> { Ability_1, Ability_2, Ability_3, Ability_4, };
            }
        }

        public string OnFreeRotation { get; set; }
        public string ret_msg { get; set; }
    }

    public class GodsDBContext : DbContext
    {
        public GodsDBContext()
        {

        }
        public DbSet<God> Gods { get; set; }
    }
}

