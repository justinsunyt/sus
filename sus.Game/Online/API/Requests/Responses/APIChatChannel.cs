// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using Newtonsoft.Json;
using sus.Game.Online.Chat;

namespace sus.Game.Online.API.Requests.Responses
{
    public class APIChatChannel
    {
        [JsonProperty(@"channel_id")]
        public int? ChannelID { get; set; }

        [JsonProperty(@"recent_messages")]
        public List<Message> RecentMessages { get; set; }
    }
}
