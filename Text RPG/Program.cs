using System;
using System.Collections.Generic;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character();

            player.Inventory.Add(new Item
            {
                Name = "무쇠갑옷",
                Type = ItemType.Armor,
                Value = 5,
                Description = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                IsEquipped = true
            });
            player.Inventory.Add(new Item
            {
                Name = "스파르타의 창",
                Type = ItemType.Weapon,
                Value = 7,
                Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                IsEquipped = true
            });
            player.Inventory.Add(new Item
            {
                Name = "낡은 검",
                Type = ItemType.Weapon,
                Value = 2,
                Description = "쉽게 볼 수 있는 낡은 검 입니다."
            });

            ShowStartScreen(player);
        }

        static void ShowStartScreen(Character player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.ResetColor();

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowStatus(player);
                    break;
                case "2":
                    ShowInventory(player);
                    break;
                case "3":
                    ShowShop(player);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다");
                    Console.ResetColor();
                    break;
            }

            Console.WriteLine("\n아무 키나 누르면 계속합니다...");
            Console.ReadKey();
            ShowStartScreen(player);
        }

        static void ShowStatus(Character player)
        {
            while (true)
            {
                player.DisplayStatus();

                string input = Console.ReadLine();
                if (input == "0")
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
                Console.WriteLine("아무 키나 누르면 다시 시도합니다...");
                Console.ReadKey();
            }
        }

        static void ShowInventory(Character player)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("인벤토리");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");

                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("아이템이 없습니다.");
                    Console.WriteLine("\n0. 나가기");
                }
                else
                {
                    foreach (Item item in player.Inventory)
                    {
                        Console.WriteLine(item.GetDisplayInfo());
                    }

                    Console.WriteLine("\n1. 장착 관리");
                    Console.WriteLine("2. 나가기");
                }

                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (player.Inventory.Count == 0)
                {
                    if (input == "0") break;
                }
                else
                {
                    if (input == "2") break;
                    else if (input == "1")
                    {
                        ManageEquipments(player);
                    }
                }
            }
        }

        static void ManageEquipments(Character player)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    Console.WriteLine($"- {i + 1} {(item.IsEquipped ? "[E]" : "   ")}{item.Name,-12} | {(item.Type == ItemType.Weapon ? $"공격력 +{item.Value}" : $"방어력 +{item.Value}")} | {item.Description}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") break;

                if (int.TryParse(input, out int index))
                {
                    if (index >= 1 && index <= player.Inventory.Count)
                    {
                        player.Inventory[index - 1].IsEquipped = !player.Inventory[index - 1].IsEquipped;
                    }
                    else
                    {
                        ShowInvalidInput();
                    }
                }
                else
                {
                    ShowInvalidInput();
                }
            }

            static void ShowInvalidInput()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n잘못된 입력입니다");
                Console.ResetColor();
                Console.WriteLine("아무 키나 누르면 계속합니다...");
                Console.ReadKey();
            }
        }

        static void ShowShop(Character player)
        {
            List<ShopItem> shopItems = new List<ShopItem>
            {
                new ShopItem { ItemData = new Item { Name = "수련자 갑옷", Type = ItemType.Armor, Value = 5, Description = "수련에 도움을 주는 갑옷입니다." }, Price = 1000 },
                new ShopItem { ItemData = new Item { Name = "무쇠갑옷", Type = ItemType.Armor, Value = 9, Description = "무쇠로 만들어져 튼튼한 갑옷입니다." }, Price = 1200 },
                new ShopItem { ItemData = new Item { Name = "스파르타의 갑옷", Type = ItemType.Armor, Value = 15, Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다." }, Price = 3500 },
                new ShopItem { ItemData = new Item { Name = "낡은 검", Type = ItemType.Weapon, Value = 2, Description = "쉽게 볼 수 있는 낡은 검 입니다." }, Price = 600 },
                new ShopItem { ItemData = new Item { Name = "청동 도끼", Type = ItemType.Weapon, Value = 5, Description = "어디선가 사용됐던거 같은 도끼입니다." }, Price = 1500 },
                new ShopItem { ItemData = new Item { Name = "스파르타의 창", Type = ItemType.Weapon, Value = 7, Description = "스파르타의 전사들이 사용했다는 전설의 창입니다." }, Price = 1800 },
            };

            foreach (var shopItem in shopItems)
            {
                foreach (var ownedItem in player.Inventory)
                {
                    if (shopItem.ItemData.Name == ownedItem.Name)
                    {
                        shopItem.IsPurchased = true;
                        break;
                    }
                }
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("상점");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{player.Gold} G\n");
                Console.ResetColor();

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Count; i++)
                {
                    Console.WriteLine(shopItems[i].GetDisplayInfo(i));
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") break;
                else if (input == "1")
                {
                    Console.Write("\n구매할 아이템 번호를 입력해주세요.\n>> ");
                    string buyInput = Console.ReadLine();

                    bool isValid = int.TryParse(buyInput, out int index);
                    bool isInRange = index >= 1 && index <= shopItems.Count;

                    if (!isValid || !isInRange)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n잘못된 입력입니다.");
                        Console.ResetColor();
                    }
                    else
                    {
                        var selectedItem = shopItems[index - 1];

                        if (selectedItem.IsPurchased)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n이미 구매한 아이템입니다");
                            Console.ResetColor();
                        }
                        else if (player.Gold < selectedItem.Price)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nGold가 부족합니다.");
                            Console.ResetColor();
                        }
                        else
                        {
                            player.Gold -= selectedItem.Price;
                            player.Inventory.Add(selectedItem.ItemData);
                            selectedItem.IsPurchased = true;

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n구매를 완료했습니다.");
                            Console.ResetColor();
                        }
                    }

                    Console.WriteLine("\n아무 키나 누르면 계속합니다...");
                    Console.ReadKey();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine("아무 키나 누르면 계속합니다...");
                    Console.ReadKey();
                }
            }
        }
    }
}