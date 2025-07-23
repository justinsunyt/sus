// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Net.Http;
using sus.Framework.IO.Network;
using sus.Game.Online.API.Requests.Responses;

namespace sus.Game.Online.API.Requests
{
    public class CommentDeleteRequest : APIRequest<CommentBundle>
    {
        public readonly long CommentId;

        public CommentDeleteRequest(long id)
        {
            CommentId = id;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Delete;
            return req;
        }

        protected override string Target => $@"comments/{CommentId}";
    }
}
