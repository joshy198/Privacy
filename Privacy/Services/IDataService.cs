using Privacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Services
{
    public interface IDataService
    {
        bool AllowCounting(UInt64 UserId,UInt64 GameId);
        bool AllowStatistics(UInt64 UserId, UInt64 GameId);
        bool AnswerQuestion(UInt64 UserId, UInt64 GameId, UInt64 QuestionId, bool YNAnswer, int cnt_answer);
        bool ChangeLanguage(UInt64 UserId, UInt64 LanguageId);
        bool ChangeUserName(UInt64 UserId, string Name);
        int CountPlayersByGameId(UInt64 GameId);
        UInt64 CreateUser(UInt64 LanguageId, string Name);
        bool ForceNextQuestion(UInt64 UserId, UInt64 GameId);
        IEnumerable<Player> GetAnsweredUsers(UInt64 GameId);
        IEnumerable<Language> GetLanguages();
        IEnumerable<Player> GetPlayersInGame();
        Question GetQuestionByUserAndGameId(UInt64 UserId, UInt64 GameId);
        IEnumerable<Group> GetQuestionGroupsByUserId(UInt64 UserId);
        IEnumerable<ID> GetQuestionIdsByGroupId(UInt64 GroupId);
        IEnumerable<Statistic> GetStatisticByGameId(UInt64 GameId);
        bool IsContinueAllowed(UInt64 GameId);
        UInt64 JoinGame(UInt64 UserId, UInt64 GameId);
        UInt64 NewGame(UInt64 UserId, UInt64 QuestionId);
    }
}
