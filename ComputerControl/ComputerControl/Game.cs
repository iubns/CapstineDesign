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
        public string gameId;

        public static List<Game> GetGames()
        {
            if(games == null)
            {
                games = new List<Game>();
                games.Add(new Game()
                {
                    gameNameKr = "메이플스토리",
                    gameId = "MapleStory"
                });
                games.Add(new Game()
                {
                    gameNameKr = "리그오브레전드",
                    gameId = "LeagueClient"
                });
                games.Add(new Game()
                {
                    gameNameKr = "카카오톡",
                    gameId = "KakaoTalk"
                });
                games.Add(new Game()
                {
                    gameNameKr = "겟엠프드",
                    gameId = "amped"
                });
                games.Add(new Game()
                {
                    gameNameKr = "블리자드",
                    gameId = "Battle.net"
                });
                games.Add(new Game()
                {
                    gameNameKr = "디아블로3",
                    gameId = "Diablo III64"
                });
                games.Add(new Game()
                {
                    gameNameKr = "던전앤파이터",
                    gameId = "DNF"
                });
                games.Add(new Game()
                {
                    gameNameKr = "사이퍼즈",
                    gameId = "Cyphers"
                });
                games.Add(new Game()
                {
                    gameNameKr = "스타크래프트",
                    gameId = "StarCraft"
                });
                games.Add(new Game()
                {
                    gameNameKr = "스타크래프트2",
                    gameId = "SC2_x64"
                });
                games.Add(new Game()
                {
                    gameNameKr = "엘소드",
                    gameId = "x2"
                });
                games.Add(new Game()
                {
                    gameNameKr = "크레이지아케이드",
                    gameId = "CA"
                });
                games.Add(new Game()
                {
                    gameNameKr = "클로저스",
                    gameId = "CW"
                });
                games.Add(new Game()
                {
                    gameNameKr = "버블파이터",
                    gameId = "BubbleFighter"
                });
                games.Add(new Game()
                {
                    gameNameKr = "서든어택",
                    gameId = "suddenattack"
                });
                games.Add(new Game()
                {
                    gameNameKr = "오버워치",
                    gameId = "Overwatch"
                });
                games.Add(new Game()
                {
                    gameNameKr = "카운터스트라이크",
                    gameId = "cstrike-online"
                });
                games.Add(new Game()
                {
                    gameNameKr = "카트라이더",
                    gameId = "KartRider"
                });
                games.Add(new Game()
                {
                    gameNameKr = "하스스톤",
                    gameId = "Hearthstone"
                });
                games.Add(new Game()
                {
                    gameNameKr = "테일즈런너",
                    gameId = "trgame"
                });
                games.Add(new Game()
                {
                    gameNameKr = "피파온라인4 ",
                    gameId = "fifa4zf"
                });
            }
            return games;
        }
    }

}
