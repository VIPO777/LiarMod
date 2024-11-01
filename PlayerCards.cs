using LiarMod.OnGui;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LiarMod
{
    /// <summary>
    ///  https://www.unknowncheats.me/forum/other-games/667906-truth-bar-menu-v0-1-liars-bar-internal-cheat.html
    ///  No fucking sense to cheat in that game, and in general, cheating is bad, 
    ///  but only curious to use this in the spectator mod, anyway, I don't care. 
    ///  This is just a network possibility and decompiled code. 
    ///  Devs can fix this easily in the future, just by reading a couple of Mirror/Game Net security articles.
    ///  Like So: https://github.com/dogdie233/LiarsBarEnhance/blob/master/LiarsBarEnhance/Features/BlorfAntiCheat.cs
    /// </summary>
    internal static class PlayerCards
    {
         
#if DEBUG
        public static void FixedUpdate()
        {
            Manager manager = Manager.Instance;
            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            if (manager == null)
                return;

            if (manager.GameStarted && net.mode != NetworkManagerMode.Offline)
            {
                if (LiarMenu.ShowLiarsDeckCards &&
                    manager.mode == CustomNetworkManager.GameMode.LiarsDeck)
                {
                    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (gameObject.GetComponent<PlayerStats>() != null && !Players.Contains(gameObject.GetComponent<PlayerStats>()))
                        {
                            Players.Add(gameObject.GetComponent<PlayerStats>());
                        }
                    }

                    resetPlayerCards();
                    cardOnTable.Clear();
                    gamePlayManager = Object.FindObjectOfType<BlorfGamePlayManager>();
                    SyncList<int> lastRound = gamePlayManager.LastRound;
                    foreach (int num in lastRound)
                    {
                        string text = ConvertIntToCard(num);
                        cardOnTable.Add(text);
                    }
                    for (int i = 0; i < Players.Count; i++)
                    {
                        if (Players[i] != null)
                        {
                            addPlayerData("PlayerName", Players[i].PlayerName, i);
                            foreach (GameObject gameObject2 in Players[i].GetComponent<BlorfGamePlay>().Cards)
                            {
                                Component[] components = gameObject2.GetComponents(typeof(Component));
                                List<Card> list = new List<Card>();
                                foreach (Component obj in components)
                                {
                                    if (obj is Card)
                                    {
                                        Card card = (Card)obj;
                                        list.Add(card);
                                    }
                                }
                                Component[] components2 = gameObject2.GetComponents(typeof(Component));
                                foreach (Component obj2 in components2)
                                {
                                    if (obj2 is Card)
                                    {
                                        Card card2 = (Card)obj2;
                                        string text2 = ConvertIntToCard(card2.cardtype);
                                        if (i == 0)
                                        {
                                            if (Player1_Cards.Count == 5)
                                            {
                                                Player1_Cards.Clear();
                                            }
                                            Player1_Cards.Add(text2);
                                        }
                                        if (i == 1)
                                        {
                                            if (Player2_Cards.Count == 5)
                                            {
                                                Player2_Cards.Clear();
                                            }
                                            Player2_Cards.Add(text2);
                                        }
                                        if (i == 2)
                                        {
                                            if (Player3_Cards.Count == 5)
                                            {
                                                Player3_Cards.Clear();
                                            }
                                            Player3_Cards.Add(text2);
                                        }
                                        if (i == 3)
                                        {
                                            if (Player4_Cards.Count == 5)
                                            {
                                                Player4_Cards.Clear();
                                            }
                                            Player4_Cards.Add(text2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void OnGUI()
        {
            Manager manager = Manager.Instance;
            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            if (!LiarMenu.ShowLiarsDeckCards || manager == null)
                return;


            if (net.mode != NetworkManagerMode.Offline && manager.GameStarted)
            {
                if (manager.mode == CustomNetworkManager.GameMode.LiarsDeck)
                {
                    if (cardOnTable.Count<string>() >= 1)
                    {
                        float num = (float)Screen.height;
                        float num2 = (float)Screen.width;
                        playedCardWindowRect = GUI.Window(2, playedCardWindowRect, new GUI.WindowFunction(cardsPlayedWindow), "Cards on the table");
                    }

                    if (manager.GameStarted && Players.Count != 0)
                    {
                        playerCardWindowRect = GUI.Window(1, playerCardWindowRect, new GUI.WindowFunction(playerCardsWindow), "Player Card");
                    }
                }
            }
        }

        private static void resetPlayerCards()
        {
            Player1_Cards.Clear();
            Player2_Cards.Clear();
            Player3_Cards.Clear();
            Player4_Cards.Clear();
        }

        private static void getPlayerCards(List<Card> cards, int i)
        {
            int num = 0;
            foreach (Card card in cards)
            {
                string text = ConvertIntToCard(card.cardtype);
                if (i == 0)
                {
                    Player1_Cards.Add(card.cardtype.ToString() + "cardName: " + text);
                }
                else if (i == 1)
                {
                    Player2_Cards.Add(card.cardtype.ToString() + "cardName: " + text);
                }
                else if (i == 2)
                {
                    Player3_Cards.Add(card.cardtype.ToString() + "cardName: " + text);
                }
                else if (i == 3)
                {
                    Player4_Cards.Add(card.cardtype.ToString() + "cardName: " + text);
                }
                if (num == 0)
                {
                    addPlayerData("Card1", text, i);
                }
                else if (num == 1)
                {
                    addPlayerData("Card2", text, i);
                }
                else if (num == 2)
                {
                    addPlayerData("Card3", text, i);
                }
                else if (num == 3)
                {
                    addPlayerData("Card4", text, i);
                }
                else if (num == 4)
                {
                    addPlayerData("Card5", text, i);
                }
                num++;
            }
        }

        private static void addPlayerData(string valueType, string value, int playerId)
        {
            if (playerId == 0)
            {
                Player1_Datas[valueType] = value;
                return;
            }
            if (playerId == 1)
            {
                Player2_Datas[valueType] = value;
                return;
            }
            if (playerId == 2)
            {
                Player3_Datas[valueType] = value;
                return;
            }
            if (playerId == 3)
            {
                Player4_Datas[valueType] = value;
            }
        }

        private static string ConvertIntToCard(int cardType)
        {
            switch (cardType)
            {
                case -1:
                    return "DEVIL";
                case 1:
                    return "K";
                case 2:
                    return "Q";
                case 3:
                    return "A";
                case 4:
                    return "J";
            }
            return cardType.ToString();
        }

        private static void cardsPlayedWindow(int wId)
        {
            cardOnTable = formatCardColor(cardOnTable);
            GUIStyle guistyle = new GUIStyle(GUI.skin.label);
            guistyle.richText = true;
            GUI.Label(new Rect(10f, 30f, 100f, 30f), string.Join(", ", cardOnTable) ?? "", guistyle);
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
        }

        private static void playerCardsWindow(int wId)
        {
            Player1_Cards = formatCardColor(Player1_Cards);
            Player2_Cards = formatCardColor(Player2_Cards);
            Player3_Cards = formatCardColor(Player3_Cards);
            Player4_Cards = formatCardColor(Player4_Cards);
            GUIStyle guistyle = new GUIStyle(GUI.skin.label);
            guistyle.richText = true;
            GUI.Label(new Rect(10f, 30f, 400f, 30f), string.Concat(new string[]
            {
                Player1_Datas["PlayerName"],
                " >> Cards : ",
                string.Join(" | ", Player1_Cards),
                " | ",
                (Players[0].GetComponent<BlorfGamePlay>().Networkrevolverbulllet - Players[0].GetComponent<BlorfGamePlay>().Networkcurrentrevoler == 0) ? "Dead on next!" : string.Format("{0} / {1}", Players[0].GetComponent<BlorfGamePlay>().Networkcurrentrevoler, Players[0].GetComponent<BlorfGamePlay>().Networkrevolverbulllet + 1)
            }), guistyle);
            GUI.Label(new Rect(10f, 60f, 400f, 30f), string.Concat(new string[]
            {
                Player2_Datas["PlayerName"],
                " >> Cards : ",
                string.Join(" | ", Player2_Cards),
                " | ",
                (Players[1].GetComponent<BlorfGamePlay>().Networkrevolverbulllet - Players[1].GetComponent<BlorfGamePlay>().Networkcurrentrevoler == 0) ? "Dead on next!" : string.Format("{0} / {1}", Players[1].GetComponent<BlorfGamePlay>().Networkcurrentrevoler, Players[1].GetComponent<BlorfGamePlay>().Networkrevolverbulllet + 1)
            }), guistyle);
            GUI.Label(new Rect(10f, 90f, 400f, 30f), string.Concat(new string[]
            {
                Player3_Datas["PlayerName"],
                " >> Cards : ",
                string.Join(" | ", Player3_Cards),
                " | ",
                (Players[2].GetComponent<BlorfGamePlay>().Networkrevolverbulllet - Players[2].GetComponent<BlorfGamePlay>().Networkcurrentrevoler == 0) ? "Dead on next!" : string.Format("{0} / {1}", Players[2].GetComponent<BlorfGamePlay>().Networkcurrentrevoler, Players[2].GetComponent<BlorfGamePlay>().Networkrevolverbulllet + 1)
            }), guistyle);
            GUI.Label(new Rect(10f, 120f, 400f, 30f), string.Concat(new string[]
            {
                Player4_Datas["PlayerName"],
                " >> Cards : ",
                string.Join(" | ", Player4_Cards),
                " | ",
                (Players[3].GetComponent<BlorfGamePlay>().Networkrevolverbulllet - Players[3].GetComponent<BlorfGamePlay>().Networkcurrentrevoler == 0) ? "Dead on next!" : string.Format("{0} / {1}", Players[3].GetComponent<BlorfGamePlay>().Networkcurrentrevoler, Players[3].GetComponent<BlorfGamePlay>().Networkrevolverbulllet + 1)
            }), guistyle);
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
        }

        private static List<string> formatCardColor(List<string> cards)
        {
            List<string> list = new List<string>();
            foreach (string text in cards)
            {
                if (text.Equals("Q"))
                {
                    list.Add("<color=purple>Q</color>");
                }
                else if (text.Equals("A"))
                {
                    list.Add("<color=orange>A</color>");
                }
                else if (text.Equals("K"))
                {
                    list.Add("<color=red>K</color>");
                }
                else if (text.Equals("J"))
                {
                    list.Add("<color=white>J</color>");
                }
                else if (text.Equals("DEVIL"))
                {
                    list.Add("<color=red>DEVIL</color>");
                }
                else
                {
                    list.Add(text);
                }
            }
            return list;
        }

        internal static void OnSceneWasUnloaded(int buildindex, string sceneName)
        {
            if (sceneName == "Game")
            {
                Players.Clear();
                PlayerNames.Clear();
                Player1_Cards.Clear();
                Player2_Cards.Clear();
                Player3_Cards.Clear();
                Player4_Cards.Clear();
            }
        }

        public static void EditCards(PlayerStats player, int cardType)
        {
            foreach (GameObject gameObject in player.GetComponent<BlorfGamePlay>().Cards)
            {
                Component[] components = gameObject.GetComponents(typeof(Component));
                foreach (Card card in components.OfType<Card>())
                {
                    card.cardtype = cardType;
                    card.SetCard();
                }
            }
        }

        public static void ModifyBullet(int value)
        {

            PlayerStats mainPlayer = Manager.Instance.GetLocalPlayer();
            mainPlayer.GetComponent<BlorfGamePlay>().Networkrevolverbulllet = value;
        }

        public static string getLastBulletToString()
        {
            return Manager.Instance.GetLocalPlayer().GetComponent<BlorfGamePlay>().Networkrevolverbulllet.ToString();
        }

        private static BlorfGamePlayManager gamePlayManager = new BlorfGamePlayManager();
        private static List<PlayerStats> Players = new List<PlayerStats>();
        private static List<int> CardInfo = new List<int>();
        private static List<string> cardOnTable = new List<string>();
        private static List<string> PlayerNames = new List<string>();
        private static List<string> Player1_Cards = new List<string>();
        private static List<string> Player2_Cards = new List<string>();
        private static List<string> Player3_Cards = new List<string>();
        private static List<string> Player4_Cards = new List<string>();

        private static Dictionary<string, string> Player1_Datas = new Dictionary<string, string>
        {
            { "PlayerName", "" },
            { "Card1", "" },
            { "Card2", "" },
            { "Card3", "" },
            { "Card4", "" },
            { "Card5", "" }
        };

        private static Dictionary<string, string> Player2_Datas = new Dictionary<string, string>
        {
            { "PlayerName", "" },
            { "Card1", "" },
            { "Card2", "" },
            { "Card3", "" },
            { "Card4", "" },
            { "Card5", "" }
        };

        private static Dictionary<string, string> Player3_Datas = new Dictionary<string, string>
        {
            { "PlayerName", "" },
            { "Card1", "" },
            { "Card2", "" },
            { "Card3", "" },
            { "Card4", "" },
            { "Card5", "" }
        };

        private static Dictionary<string, string> Player4_Datas = new Dictionary<string, string>
        {
            { "PlayerName", "" },
            { "Card1", "" },
            { "Card2", "" },
            { "Card3", "" },
            { "Card4", "" },
            { "Card5", "" }
        };

        private static Rect playerCardWindowRect = new Rect(10f, 300f, 400f, 160f);
        private static Rect playedCardWindowRect = new Rect(10f, 200f, 200f, 60f);
#endif
    }
}
