using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.Model
{
    public class LangPCK
    {
        [JsonProperty("id")]
        public UInt64 Id { get; set; }
        [JsonProperty("lang_id")]
        public ulong LanguageID { get; set; }
        [JsonProperty("version")]
        public double Version { get; set; }
        [JsonProperty("pck_name")]
        public string PackageName { get; set; }
        [JsonProperty("yes_trans")]
        public string YesTranslation { get; set; }
        [JsonProperty("no_trans")]
        public string NoTranslation { get; set; }
        [JsonProperty("menu_trans")]
        public string MenuTranslation { get; set; }
        [JsonProperty("settings_trans")]
        public string SettingsTranslation { get; set; }
        [JsonProperty("profile_trans")]
        public string ProfileTranslation { get; set; }
        [JsonProperty("points_trans")]
        public string PointsTranslation { get; set; }
        [JsonProperty("language_trans")]
        public string LanguageTranslation { get; set; }
        [JsonProperty("name_trans")]
        public string NameTranslation { get; set; }
        [JsonProperty("quit_trans")]
        public string QuitTranslation { get; set; }
        [JsonProperty("home_trans")]
        public string HomeTranslation { get; set; }
        [JsonProperty("question_trans")]
        public string QuestionTranslation { get; set; }
        [JsonProperty("apply_trans")]
        public string ApplyTranslation { get; set; }
        [JsonProperty("discard_trans")]
        public string DiscardTranslation { get; set; }
        [JsonProperty("back_trans")]
        public string BackTranslation { get; set; }
        [JsonProperty("create_game_trans")]
        public string CreateGameTranslation { get; set; }
        [JsonProperty("category_trans")]
        public string CategoryTranslation { get; set; }
        [JsonProperty("join_trans")]
        public string JoinTranslation { get; set; }
        [JsonProperty("lobby_trans")]
        public string LobbyTranslation { get; set; }
        [JsonProperty("players_trans")]
        public string PlayersTranslation { get; set; }
        [JsonProperty("start_trans")]
        public string StartTranslation { get; set; }
        [JsonProperty("setup_trans")]
        public string SetupTranslation { get; set; }
        [JsonProperty("guess_trans")]
        public string GuessTranslation { get; set; }
        [JsonProperty("quit_conform")]
        public string QuitMsg { get; set; }
        [JsonProperty("setup_info")]
        public string SetupInfo { get; set; }
        [JsonProperty("main_info")]
        public string MainInfo { get; set; }
        [JsonProperty("create_info")]
        public string CreateInfo { get; set; }
        [JsonProperty("join_info")]
        public string JoiniInfo { get; set; }
        [JsonProperty("question_info")]
        public string QuestionInfo { get; set; }
        [JsonProperty("settings_info")]
        public string SettingsInfo { get; set; }
        [JsonProperty("about_info")]
        public string AboutInfo { get; set; }
        [JsonProperty("guess_info")]
        public string GuessInfo { get; set; }
        [JsonProperty("lobby_info1")]
        public string LobbyInfo1 { get; set; }
        [JsonProperty("lobby_info2")]
        public string LobbyInfo2 { get; set; }
        [JsonProperty("lobby_info3")]
        public string LobbyInfo3 { get; set; }
        [JsonProperty("credit_trans")]
        public string CreditTranslation { get; set; }
        [JsonProperty("game_id_trans")]
        public string GameIdTranslation { get; set; }
        [JsonProperty("game_trans")]
        public string GameTranslation { get; set; }
        [JsonProperty("answered_players_trans")]
        public string AnsweredPlayersTranslation { get; set; }
        [JsonProperty("y_vote_txt")]
        public string YesVoteText { get; set; }
        [JsonProperty("notification_trans")]
        public string NotificationTranslation { get; set; }
        [JsonProperty("lobby_msg1")]
        public string LobbyMsg1 { get; set; }
        [JsonProperty("lobby_msg2")]
        public string LobbyMsg2 { get; set; }
        [JsonProperty("lobby_msg3")]
        public string LobbyMsg3 { get; set; }
        [JsonProperty("congratulations_trans")]
        public string CongratulationsTranslation { get; set; }
        [JsonProperty("wrong_id_trans")]
        public string WrongIdTranslation { get; set; }
        [JsonProperty("guess_txt")]
        public string GuessText { get; set; }
        [JsonProperty("category_txt")]
        public string CategoryText { get; set; }
        [JsonProperty("confirm_trans")]
        public string ConfirmationTranslation { get; set; }
        [JsonProperty("load_trans")]
        public string LoadingTranslation { get; set; }
        [JsonProperty("load_msg1")]
        public string LoadMsg1 { get; set; }
        [JsonProperty("load_msg2")]
        public string LoadMsg2 { get; set; }
        [JsonProperty("load_msg3")]
        public string LoadMsg3 { get; set; }
        [JsonProperty("pick_lang_trans")]
        public string PickLanguageTranslation { get; set; }
        [JsonProperty("your_name_trans")]
        public string YourNameTranslation { get; set; }
        [JsonProperty("additional_info_trans")]
        public string AdditionalInformationTranslation { get; set; }
        [JsonProperty("allow_guessing_trans")]
        public string AllowGuessingTranslation { get; set; }
        [JsonProperty("allow_statistic_trans")]
        public string AllowStatisticTranslation { get; set; }
        [JsonProperty("continue_trans")]
        public string ContinueTranslation { get; set; }
        [JsonProperty("save_trans")]
        public string SaveTranslation { get; set; }
        [JsonProperty("lobby_host_info1")]
        public string LobbyHostInfo1 { get; set; }
        [JsonProperty("lobby_host_info2")]
        public string LobbyHostInfo2 { get; set; }
        [JsonProperty("lobby_host_info3")]
        public string LobbyHostInfo3 { get; set; }
    }

}
