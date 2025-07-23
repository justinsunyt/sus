// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Linq;
using Newtonsoft.Json;

namespace sus.Game.Online.API.Requests.Responses
{
    public class APIKudsusHistory
    {
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt;

        [JsonProperty("amount")]
        public int Amount;

        [JsonProperty("post")]
        public ModdingPost Post;

        public class ModdingPost
        {
            [JsonProperty("url")]
            public string Url;

            [JsonProperty("title")]
            public string Title;
        }

        [JsonProperty("giver")]
        public KudsusGiver Giver;

        public class KudsusGiver
        {
            [JsonProperty("url")]
            public string Url;

            [JsonProperty("username")]
            public string Username;
        }

        public KudsusSource Source;

        public KudsusAction Action;

        [JsonProperty("action")]
        private string action
        {
            set
            {
                // incoming action may contain a prefix. if it doesn't, it's a legacy forum event.

                string[] split = value.Split('.');

                if (split.Length > 1)
                    Enum.TryParse(split.First().Replace("_", ""), true, out Source);
                else
                    Source = KudsusSource.Forum;

                Enum.TryParse(split.Last(), true, out Action);
            }
        }
    }

    public enum KudsusSource
    {
        Unknown,
        AllowKudsus,
        Delete,
        DenyKudsus,
        Forum,
        Recalculate,
        Restore,
        Vote
    }

    public enum KudsusAction
    {
        Give,
        Reset,
        Revoke,
    }
}
