using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Privacy.Model;

namespace Privacy.Services
{
    public class ConcreteDataService : IDataService
    {
        public bool AllowCounting(ulong UserId, ulong GameId)
        {
            return true;
        }

        public bool AllowStatistics(ulong UserId, ulong GameId)
        {
            return true;
        }

        public bool AnswerQuestion(ulong UserId, ulong GameId, ulong QuestionId, bool YNAnswer, int cnt_answer)
        {
            return true;
        }

        public bool ChangeLanguage(ulong UserId, ulong LanguageId)
        {
            return true;
        }

        public bool ChangeUserName(ulong UserId, string Name)
        {
            return true;
        }

        public int CountPlayersByGameId(ulong GameId)
        {
            return 5;
        }

        public ulong CreateUser(ulong LanguageId, string Name)
        {
            return 10;
        }

        public bool ForceNextQuestion(ulong UserId, ulong GameId)
        {
            return true;
        }

        public IEnumerable<Player> GetAnsweredUsers(ulong GameId)
        {
            return new List<Player>
            {
                new Player { title = "Player 1", show_stat=true },
                new Player { title = "Player 2", show_stat=true },
                new Player { title = "Player 3", show_stat=true },
                new Player { title = "Player 4", show_stat=true },
                new Player { title = "Player 5", show_stat=true }
            };
        }

        public IEnumerable<Language> GetLanguages()
        {
            return new List<Language> {
                new Language { id = 1, title = "English" },
                new Language { id = 1, title = "German" }
            };
        }

        public IEnumerable<Player> GetPlayersInGame()
        {
            return new List<Player>
            {
                new Player { title = "Player 1", show_stat=true },
                new Player { title = "Player 2", show_stat=true },
                new Player { title = "Player 3", show_stat=true },
                new Player { title = "Player 4", show_stat=true },
                new Player { title = "Player 5", show_stat=true }
            };
        }

        public Question GetQuestionByUserAndGameId(ulong UserId, ulong GameId)
        {
            return new Question { id = 1, title = "Hast du jemals eine Frage beantwortet?" };
        }

        public IEnumerable<Group> GetQuestionGroupsByUserId(ulong UserId)
        {
            return new List<Group>
            {
                new Group { id = 1, title = "Category 1" },
                new Group { id = 2, title = "Category 2" },
                new Group { id = 3, title = "Category 3" }
            };
        }

        public IEnumerable<ID> GetQuestionIdsByGroupId(ulong GroupId)
        {
            return new List<ID> {
                new ID { id = 1 },
                new ID { id = 2 },
                new ID { id = 3 },
                new ID { id = 4 },
                new ID { id = 5 },
                new ID { id = 70 }
            };
        }

        public IEnumerable<Statistic> GetStatisticByGameId(ulong GameId)
        {
            return new List<Statistic>
            {
                new Statistic { guessed=3, name="Player 1", points=10210, yeses=3 },
                new Statistic { guessed=2, name="Player 2", points=40, yeses=3 },
                new Statistic { guessed=5, name="Player 3", points=27, yeses=3 },
                new Statistic { guessed=1, name="Player 4", points=19, yeses=3 },
                new Statistic { guessed=2, name="Player 5", points=58, yeses=3 },
            };
        }

        public bool IsContinueAllowed(ulong GameId)
        {
            return true;
        }

        public ulong JoinGame(ulong UserId, ulong GameId)
        {
            return 8;
        }

        public ulong NewGame(ulong UserId, ulong QuestionId)
        {
            return 4;
        }
    }
}
