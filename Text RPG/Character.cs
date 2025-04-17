using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Character
    {
        public List<Item> Inventory { get; set; } = new List<Item>();

        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int Hp { get; set; } = 100;
        public int Gold { get; set; } = 1500;

        public void DisplayStatus()
        {
            int totalAttack = Attack;
            int totalDefense = Defense;
            int bonusAttack = 0;
            int bonusDefense = 0;

            foreach (var item in Inventory)
            {
                if (item.IsEquipped)
                {
                    if (item.Type == ItemType.Weapon)
                    {
                        totalAttack += item.Value;
                        bonusAttack += item.Value;
                    }
                    else if (item.Type == ItemType.Armor)
                    {
                        totalDefense += item.Value;
                        bonusDefense += item.Value;
                    }
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. 0{Level}");
            Console.WriteLine($"{Name} ( {Job} )");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"공격력 : {totalAttack}");
            if (bonusAttack > 0) Console.Write($" (+{bonusAttack})");
            Console.WriteLine();

            Console.Write($"방어력 : {totalDefense}");
            if (bonusDefense > 0) Console.Write($" (+{bonusDefense})");
            Console.WriteLine();

            Console.WriteLine($"체 력 : {Hp}");
            Console.WriteLine($"Gold   : {Gold} G");
            Console.ResetColor();

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        }

    }
}
