using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerControl
{
    public class Game
    {
        private static List<Game> games;

        public string gameNameKr;
        public string[] gameId;

        public static List<Game> GetGames()
        {
            if(games == null)
            {
                games = new List<Game>();
                games.Add(new Game()
                {
                    gameNameKr = "메이플스토리",
                    gameId = new string[]{ "MapleStory" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "리그오브레전드",
                    gameId = new string[] { "LeagueClient", "League of Legends" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "카카오톡",
                    gameId = new string[] { "KakaoTalk" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "겟엠프드",
                    gameId = new string[] { "amped" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "블리자드",
                    gameId = new string[] { "Battle.net" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "디아블로3",
                    gameId = new string[] { "Diablo III64" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "던전앤파이터",
                    gameId = new string[] { "DNF" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "사이퍼즈",
                    gameId = new string[] { "Cyphers" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "스타크래프트",
                    gameId = new string[] { "StarCraft" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "스타크래프트2",
                    gameId = new string[] { "SC2" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "엘소드",
                    gameId = new string[] { "x2" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "크레이지아케이드",
                    gameId = new string[] { "CA" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "클로저스",
                    gameId = new string[] { "CW" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "버블파이터",
                    gameId = new string[] { "BubbleFighter" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "서든어택",
                    gameId = new string[] { "suddenattack" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "오버워치",
                    gameId = new string[] { "Overwatch" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "카운터스트라이크",
                    gameId = new string[] { "cstrike-online" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "카트라이더",
                    gameId = new string[] { "KartRider" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "하스스톤",
                    gameId = new string[] { "Hearthstone" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "테일즈런너",
                    gameId = new string[] { "trgame" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "피파온라인4 ",
                    gameId = new string[] { "fifa4zf" }
                });
                games.Add(new Game()
                {
                    gameNameKr = "넥슨",
                    gameId = new string[]{ "Nexon", "NexonPlug"}
                });
                games.Add(new Game()
                {
                    gameNameKr = "스팀",
                    gameId = new string[]{ "steamwebhelper", "SteamService", "Steam" }
                });
            }
            return games;
        }
    }

}
