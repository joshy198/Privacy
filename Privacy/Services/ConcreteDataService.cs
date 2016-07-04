using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Privacy.Model;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Privacy.JsonObj;

namespace Privacy.Services
{
    public class ConcreteDataService : IDataService
    {
        private static string url = $"http://privacygame.soft-tec.net/";
        private readonly HttpClient client = new HttpClient();
        public async Task<bool> AllowCounting(ulong UserId, ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            
            var response = await client.PostAsync(url + "allow_counting.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync()); ;
        }

        public async Task<bool> AllowStatistics(ulong UserId, ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            
            var response = await client.PostAsync(url + "allow_statistics.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> AnswerQuestion(ulong UserId, ulong GameId, ulong QuestionId, bool YNAnswer, int? cnt_answer)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
                new KeyValuePair<string, string>("question_id", QuestionId.ToString()),
                new KeyValuePair<string, string>("yn_answer", YNAnswer.ToString()),
                new KeyValuePair<string, string>("cnt_answer", cnt_answer.ToString())

            });
            
            var response = await client.PostAsync(url + "answer_question.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> ChangeLanguage(ulong UserId, ulong LanguageId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("lang_id", LanguageId.ToString())
            });
            
            var response = await client.PostAsync(url + "change_language.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> ChangeUserName(ulong UserId, string Name)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("name", Name)
            });
            
            var response = await client.PostAsync(url + "change_user_name.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<int> CountPlayersByGameId(ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });
            
            var response = await client.PostAsync(url + "count_players_by_game_id.php", formContent);
            return JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ID> CreateUser(ulong LanguageId, string Name)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("lang_id", LanguageId.ToString()),
                new KeyValuePair<string, string>("name", Name)
            });
            var response = await client.PostAsync(url + "create_user.php", formContent);
            return JsonConvert.DeserializeObject<ID>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> ForceNextQuestion(ulong UserId, ulong GameId, ulong QuestionId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
                new KeyValuePair<string, string>("question_id", QuestionId.ToString())
            });
            var response = await client.PostAsync(url + "force_next_question.php", formContent);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public async Task<IEnumerable<Player>> GetAnsweredUsers(ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            
            var response = await client.PostAsync(url + "get_answered_users.php", formContent);
            return JsonConvert.DeserializeObject<JsonPlayers>(await response.Content.ReadAsStringAsync()).Players;
        }

        public async Task<IEnumerable<Language>> GetLanguages()
        {
            var answ = await client.GetStringAsync(url + "get_languages.php");
            var lang = JsonConvert.DeserializeObject<JsonLang>(answ);
            return lang.Langs;
        }

        public async Task<IEnumerable<Player>> GetPlayersInGame( ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            var response = await client.PostAsync(url + "get_players_in_game.php", formContent);
            return JsonConvert.DeserializeObject<JsonPlayers>(await response.Content.ReadAsStringAsync()).Players;
        }

        public async Task<Question> GetQuestionByUserAndGameId(ulong UserId, ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            
            var response = await client.PostAsync(url + "get_question_by_user_and_game_id.php", formContent);
            return JsonConvert.DeserializeObject<Question>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Group>> GetQuestionGroupsByUserId(ulong UserId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });
            
            var response = await client.PostAsync(url + "get_question_groups_by_user_id.php", formContent);
            return JsonConvert.DeserializeObject<JsonGroup>(await response.Content.ReadAsStringAsync()).QouestionGroups;
        }

        public async Task<IEnumerable<ID>> GetQuestionIdsByGroupId(ulong GroupId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grp_id", GroupId.ToString()),
            });
            
            var response = await client.PostAsync(url + "get_question_ids_by_grp_id.php", formContent);
            return JsonConvert.DeserializeObject<JsonIDs>(await response.Content.ReadAsStringAsync()).QuestionIDs;

        }

        public async Task<IEnumerable<Statistic>> GetStatisticByGameId(ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });
            
            var response = await client.PostAsync(url + "get_statistic_by_game_id.php", formContent);
            return JsonConvert.DeserializeObject<JsonStat>(await response.Content.ReadAsStringAsync()).Statistics;
        }

        public async Task<Profile> GetUserprofile(ulong UserId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });
            
            var response = await client.PostAsync(url + "get_user_profile.php", formContent);
            return JsonConvert.DeserializeObject<Profile>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> IsContinueAllowed(ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });
            
            var response = await client.PostAsync(url + "is_continue_allowed.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> IsUserExisting(ulong UserId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });
            var response = await client.PostAsync(url + "is_user_existing.php", formContent);
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> IsGameExisting(ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });
            var response = await client.PostAsync(url + "is_game_existing.php", formContent);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public async Task<ID> JoinGame(ulong UserId, ulong GameId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            
            var response = await client.PostAsync(url + "join_game.php", formContent);
            return JsonConvert.DeserializeObject<ID>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ID> NewGame(ulong UserId, ulong QuestionId)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("question_id", QuestionId.ToString())
            });
            var response = await client.PostAsync(url + "new_game.php", formContent);
            return JsonConvert.DeserializeObject<ID>(await response.Content.ReadAsStringAsync());
        }
    }
}
