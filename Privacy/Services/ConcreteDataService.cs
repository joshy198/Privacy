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
        #region variables
        private static string url = $"http://privacygame.soft-tec.net/";
        private readonly HttpClient client = new HttpClient();

        #region noweb variables
        #endregion
        private int retry;
        private int countplayers;
        private IEnumerable<Player> answeredUsers;
        private IEnumerable<Player> playersInGame;
        private IEnumerable<Statistic> getStatistic;
        private Question question;
        private Profile profile;
        private bool userExisting;
        private bool isGameExisting;
        #endregion

        /// <summary>
        /// Calls the php script allow_continue, hands over the given parameters and processes it's return value, which is a json object to an bool
        /// </summary>
        /// <param name="UserId">The users ID</param>
        /// <param name="GameId">The ID of the game, the user is currently in</param>
        /// <returns>returns true if continue is allowed, else it returns false</returns>
        public async Task<bool> AllowContinue(ulong UserId, ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
            
            var response = await client.PostAsync(url + "allow_continue.php", formContent);
            
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the php script answer_question, hands over the given parameters and processes it's return value, which is a json object to an bool
        /// </summary>
        /// <param name="UserId">The user's ID</param>
        /// <param name="GameId">The ID f the game, the user is currently in</param>
        /// <param name="QuestionId">The id of the question the user wants to answer</param>
        /// <param name="YNAnswer">his answer as bool value</param>
        /// <param name="cnt_answer">his guess, how much of the other players voted for yes</param>
        /// <returns>Returns true if the question could be answered, returns false if the question is not the current question or the question couldn't be answered</returns>
        public async Task<bool> AnswerQuestion(ulong UserId, ulong GameId, ulong QuestionId, bool YNAnswer, int? cnt_answer)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
                new KeyValuePair<string, string>("question_id", QuestionId.ToString()),
                new KeyValuePair<string, string>("yn_answer", YNAnswer? "1":"0"),
                new KeyValuePair<string, string>("cnt_answer", cnt_answer.ToString())

            });

                var response = await client.PostAsync(url + "answer_question.php", formContent);
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the php script chage_language, hands over the given parameters and processes it's return value, which is a json object to an bool
        /// </summary>
        /// <param name="UserId">The Users ID</param>
        /// <param name="LanguageId">The ID of the language, the user prefers</param>
        /// <returns>returns true if the language was successfully changed</returns>
        public async Task<bool> ChangeLanguage(ulong UserId, ulong LanguageId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("lang_id", LanguageId.ToString())
            });

                var response = await client.PostAsync(url + "change_language.php", formContent);
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the php script change_user_name, hands over the given parameters and processes it's return value, which is a json object to an bool
        /// </summary>
        /// <param name="UserId">The user's ID</param>
        /// <param name="Name">the name, the user wants to use from now on</param>
        /// <returns>returns true if the name was successfully changed</returns>
        public async Task<bool> ChangeUserName(ulong UserId, string Name)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("name", Name)
            });

                var response = await client.PostAsync(url + "change_user_name.php", formContent);
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the php script count_players_by_game_id, hands over the given parameters and processes it's return value, which is a json object to an int
        /// </summary>
        /// <param name="GameId">The ID of the game, the user is currently in</param>
        /// <returns>returns the number of players in game (0 if the game does not exist)</returns>
        public async Task<int> CountPlayersByGameId(ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });
                var response = await client.PostAsync(url + "count_players_by_game_id.php", formContent);
                countplayers= JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
                return countplayers;
            }
            catch (Exception ex)
            {
                return countplayers;
            }
        }

        /// <summary>
        /// Calls the php script new_user, hands over the given parameters and processes it's return value, which is a json object to an ID
        /// </summary>
        /// <param name="LanguageId">The ID of the language, the user wants to use</param>
        /// <param name="Name">The name, the player wants to be displayed</param>
        /// <returns>returns an object of type id, containing the ID of the user</returns>
        public async Task<ID> CreateUser(ulong LanguageId, string Name)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("lang_id", LanguageId.ToString()),
                new KeyValuePair<string, string>("name", Name)
            });
                var response = await client.PostAsync(url + "create_user.php", formContent);
                return JsonConvert.DeserializeObject<ID>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ID();
            }
        }

        /// <summary>
        /// Calls the php script get_answered_users, hands over the given parameters and processes it's return value, which is a json object to an bool
        /// </summary>
        /// <param name="UserId">The User's ID</param>
        /// <param name="GameId">The ID of the game, the user is currently in</param>
        /// <param name="QuestionId">Id of the next question</param>
        /// <returns>returns true if the new question was pushed to the server, else returns false</returns>
        public async Task<bool> ForceNextQuestion(ulong UserId, ulong GameId, ulong QuestionId)
        {
            try
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
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the php script get_answered_users, hands over the given parameters and processes it's return value, which is a json object to a list of Players
        /// </summary>
        /// <param name="GameId">The user's ID</param>
        /// <returns>returns a List of objects of the type Players</returns>
        public async Task<IEnumerable<Player>> GetAnsweredUsers(ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });

                var response = await client.PostAsync(url + "get_answered_users.php", formContent);
                answeredUsers= JsonConvert.DeserializeObject<JsonPlayers>(await response.Content.ReadAsStringAsync()).Players;
                return answeredUsers;
            }
            catch (Exception ex)
            {
                if (answeredUsers == null)
                    answeredUsers = new List<Player>();
                return answeredUsers;
            }
        }

        /// <summary>
        /// Calls the php script get_languages, hands over the given parameters and processes it's return value, which is a json object of a List containing Languages
        /// </summary>
        /// <returns>retuns a List of objects of type Language</returns>
        public async Task<IEnumerable<Language>> GetLanguages()
        {
            try
            {
                var answ = await client.GetStringAsync(url + "get_languages.php");
                var lang = JsonConvert.DeserializeObject<JsonLang>(answ);
                return lang.Langs;
            }
            catch (Exception ex)
            {
                return new List<Language>();
            }
        }

        /// <summary>
        /// Calls the php script get_players_in_game, hands over the given parameters and processes it's return value, which is a json object to an List of Players
        /// </summary>
        /// <param name="GameId">The Game ID of the game the player is currently in</param>
        /// <returns>returns a List of objects of type Player which are </returns>
        public async Task<IEnumerable<Player>> GetPlayersInGame( ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });
                var response = await client.PostAsync(url + "get_players_in_game.php", formContent);
                playersInGame = JsonConvert.DeserializeObject<JsonPlayers>(await response.Content.ReadAsStringAsync()).Players;
                return playersInGame;
            }
            catch (Exception ex)
            {
                if (playersInGame == null)
                    playersInGame = new List<Player>();
                return playersInGame;
            }
        }

        /// <summary>
        /// Calls the php script get_question_by_user_and_game_id, hands over the given parameters and processes it's return value, which is a json object to a Question
        /// </summary>
        /// <param name="UserId">The user's ID</param>
        /// <param name="GameId">The ID of the game the player is currently in</param>
        /// <returns>retuns an object of type Question</returns>
        public async Task<Question> GetQuestionByUserAndGameId(ulong UserId, ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });

                var response = await client.PostAsync(url + "get_question_by_user_and_game_id.php", formContent);
                question= JsonConvert.DeserializeObject<Question>(await response.Content.ReadAsStringAsync());
                return question;
            }
            catch (Exception ex)
            {
                if (question == null)
                    question = new Question();
                return question;
            }
        }

        /// <summary>
        /// Calls the php script get_question_groups_by_user_id, hands over the given parameters and processes it's return value, which is a json object to a List of Group
        /// </summary>
        /// <param name="UserId">The user's ID</param>
        /// <returns>Returns a List of objects of type group</returns>
        public async Task<IEnumerable<Group>> GetQuestionGroupsByUserId(ulong UserId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });

                var response = await client.PostAsync(url + "get_question_groups_by_user_id.php", formContent);
                return JsonConvert.DeserializeObject<JsonGroup>(await response.Content.ReadAsStringAsync()).QouestionGroups;
            }
            catch (Exception ex)
            {
                return new LinkedList<Group>();
            }
        }

        /// <summary>
        /// Calls the php script get_question_ids_by_grp_id, hands over the given parameters and processes it's return value, which is a json object to a list of ID
        /// </summary>
        /// <param name="GroupId">ID of the group, the user wants to use</param>
        /// <returns>Returns a List of objects of type ID, which contains all the ID's of the questions in the selected category</returns>
        public async Task<IEnumerable<ID>> GetQuestionIdsByGroupId(ulong GroupId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grp_id", GroupId.ToString()),
            });

                var response = await client.PostAsync(url + "get_question_ids_by_grp_id.php", formContent);
                return JsonConvert.DeserializeObject<JsonIDs>(await response.Content.ReadAsStringAsync()).QuestionIDs;
            }
            catch (Exception ex)
            {
                return new LinkedList<ID>();
            }

        }

        /// <summary>
        /// Calls the php script get_statistic_by_game_ide, hands over the given parameters and processes it's return value, which is a json object to a list of Statistic
        /// </summary>
        /// <param name="GameId">The ID of the game, the user is currently in</param>
        /// <returns>Returns a list of objects of type Statistic</returns>
        public async Task<IEnumerable<Statistic>> GetStatisticByGameId(ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });

                var response = await client.PostAsync(url + "get_statistic_by_game_id.php", formContent);
                getStatistic= JsonConvert.DeserializeObject<JsonStat>(await response.Content.ReadAsStringAsync()).Statistics;
                if (getStatistic.FirstOrDefault().ID == 0)
                    getStatistic = new List<Statistic>();
                return getStatistic;
            }
            catch (Exception ex)
            {
                if (getStatistic == null)
                    getStatistic = new List<Statistic>();
                return getStatistic;
            }
        }

        /// <summary>
        /// Calls the php script get_user_profile, hands over the given parameters and processes it's return value, which is a json object to an Profile
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>Returns an object of type Profile, containing all the userdata</returns>
        public async Task<Profile> GetUserprofile(ulong UserId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });

                var response = await client.PostAsync(url + "get_user_profile.php", formContent);
                profile= JsonConvert.DeserializeObject<Profile>(await response.Content.ReadAsStringAsync());
                return profile;
            }
            catch (Exception ex)
            {
                if (profile == null)
                    profile = new Profile() { Lang = new Language() };
                return profile;
            }
        }

        /// <summary>
        /// Calls the php script is_continue_allowed, hands over the given parameters and processes it's return value, which is a json object to an int
        /// </summary>
        /// <param name="GameId">The id of the game, the user is currently in</param>
        /// <returns>returns true if it's allowed, ele it returns false</returns>
        public async Task<bool> IsContinueAllowed(ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });

                var response = await client.PostAsync(url + "is_continue_allowed.php", formContent);
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Calls the php script is_user_existing, hands over the given parameters and processes it's return value, which is a json object to a bool
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>
        /// returns true if a use with the given id exists, ele it returns false
        /// </returns>
        public async Task<bool> IsUserExisting(ulong UserId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });
                var response = await client.PostAsync(url + "is_user_existing.php", formContent);
                userExisting= JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                retry = 0;
                return userExisting;
            }
            catch (Exception ex)
            {
                if (userExisting)
                    return true;
                if (retry < 30)
                {
                    retry++;
                    await Task.Delay(1000);
                    return await IsUserExisting(UserId);
                }
                return false;
            }
        }

        /// <summary>
        /// Calls the php script quit_game, hands over the given parameters and processes it's return value, which is a json object to a bool
        /// </summary>
        /// <param name="UserId">the user's id</param>
        /// <returns>returns true when finished</returns>
        public async Task<bool> QuitGame(ulong UserId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
            });
                var response = await client.PostAsync(url + "quit_game.php", formContent);
                var ret= JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                retry = 0;
                return ret;
            }
            catch (Exception ex)
            {
                if (retry < 30)
                {
                    retry++;
                    await Task.Delay(1000);
                    return await QuitGame(UserId);
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Calls the php script is_game_existing, hands over the given parameters and processes it's return value, which is a json object to a bool
        /// </summary>
        /// <param name="GameId">the id of the game, which should be checked</param>
        /// <returns>returns true if the game exists, otherwise retuns false</returns>
        public async Task<bool> IsGameExisting(ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("game_id", GameId.ToString()),
            });
                var response = await client.PostAsync(url + "is_game_existing.php", formContent);
                isGameExisting = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                retry = 0;
                return isGameExisting;
            }
            catch (Exception ex)
            {
                if (isGameExisting)
                    return isGameExisting;
                else
                {
                    if (retry < 30)
                    {
                        retry++;
                        await Task.Delay(1000);
                        return await IsGameExisting(GameId);
                    }
                    else
                        return false;
                }
            }

        }

        /// <summary>
        /// Calls the php script join_game, hands over the given parameters and processes it's return value, which is a json object to an ID
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="GameId">the id of the game, the user is currently in</param>
        /// <returns>returns an object of type id containing id of the game, if successfull, otherwise containing 0</returns>
        public async Task<ID> JoinGame(ulong UserId, ulong GameId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("game_id", GameId.ToString())
            });

                var response = await client.PostAsync(url + "join_game.php", formContent);
                var data = JsonConvert.DeserializeObject<ID>(await response.Content.ReadAsStringAsync());
                retry = 0;
                return data;
            }
            catch (Exception ex)
            {
                if (retry < 30)
                {
                    retry++;
                    await Task.Delay(1000);
                    return await JoinGame(UserId, GameId);
                }
                return new ID { };
            }
        }

        /// <summary>
        /// Calls the php script new_game, hands over the given parameters and processes it's return value, which is a json object to an ID
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="QuestionId">the id of the first question</param>
        /// <returns>returns an object of type id containing the id of the game</returns>
        public async Task<ID> NewGame(ulong UserId, ulong QuestionId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("user_id", UserId.ToString()),
                new KeyValuePair<string, string>("question_id", QuestionId.ToString())
            });
                var response = await client.PostAsync(url + "new_game.php", formContent);
                var ret = JsonConvert.DeserializeObject<ID>(await response.Content.ReadAsStringAsync());
                retry = 0;
                return ret;
            }
            catch (Exception ex)
            {
                if (retry < 30)
                {
                    retry++;
                    await Task.Delay(1000);
                    return await NewGame(UserId, QuestionId);
                }
                return new ID { };
            }
        }
    }
}
