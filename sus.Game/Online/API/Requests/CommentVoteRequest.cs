// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.IO.Network;
using sus.Game.Online.API.Requests.Responses;
using System.Net.Http;

namespace sus.Game.Online.API.Requests
{
    public class CommentVoteRequest : APIRequest<CommentBundle>
    {
        private readonly long id;
        private readonly CommentVoteAction action;

        public CommentVoteRequest(long id, CommentVoteAction action)
        {
            this.id = id;
            this.action = action;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = action == CommentVoteAction.Vote ? HttpMethod.Post : HttpMethod.Delete;
            return req;
        }

        protected override string Target => $@"comments/{id}/vote";
    }

    public enum CommentVoteAction
    {
        Vote,
        UnVote
    }
}
