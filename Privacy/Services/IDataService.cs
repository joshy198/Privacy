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
        Task<bool> AllowContinue(UInt64 UserId,UInt64 GameId);
        Task<bool> AnswerQuestion(UInt64 UserId, UInt64 GameId, UInt64 QuestionId, bool YNAnswer, int? cnt_answer);
        Task<bool> ChangeLanguage(UInt64 UserId, UInt64 LanguageId);
        Task<bool> ChangeUserName(UInt64 UserId, string Name);
        Task<int> CountPlayersByGameId(UInt64 GameId);
        Task<ID> CreateUser(UInt64 LanguageId, string Name);
        Task<bool> ForceNextQuestion(UInt64 UserId, UInt64 GameId, ulong QuestionId);
        Task<IEnumerable<Player>> GetAnsweredUsers(UInt64 GameId);
        Task<IEnumerable<Language>> GetLanguages();
        Task<IEnumerable<Player>> GetPlayersInGame(ulong GameId);
        Task<Question> GetQuestionByUserAndGameId(UInt64 UserId, UInt64 GameId);
        Task<IEnumerable<Group>> GetQuestionGroupsByUserId(UInt64 UserId);
        Task<IEnumerable<ID>> GetQuestionIdsByGroupId(UInt64 GroupId);
        Task<IEnumerable<Statistic>> GetStatisticByGameId(UInt64 GameId);
        Task<Profile> GetUserprofile(ulong UserId);
        Task<bool> IsContinueAllowed(UInt64 GameId);
        Task<ID> JoinGame(UInt64 UserId, UInt64 GameId);
        Task<ID> NewGame(UInt64 UserId, UInt64 QuestionId);
        Task<bool> IsUserExisting(ulong UserId);
        Task<bool> IsGameExisting(ulong UserId);
        Task<bool> QuitGame(ulong UserId);
        Task<LangPCK> GetLanguagePack(ulong UserId);
        Task<PckVersion> GetLangVersion(UInt64 LangPckId,double vnr);
    }
}
