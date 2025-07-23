// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Threading.Tasks;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Game.Online;
using sus.Game.Online.API;
using sus.Game.Online.Rooms;
using sus.Game.Screens.OnlinePlay.Lounge.Components;

namespace sus.Game.Screens.OnlinePlay.Lounge
{
    /// <summary>
    /// Polls for rooms for the main lounge listing.
    /// </summary>
    public partial class LoungeListingPoller : PollingComponent
    {
        [Resolved]
        private IAPIProvider api { get; set; } = null!;

        public required Action<Room[]> RoomsReceived { get; init; }
        public readonly IBindable<FilterCriteria?> Filter = new Bindable<FilterCriteria?>();

        private GetRoomsRequest? lastPollRequest;

        protected override Task Poll()
        {
            if (!api.IsLoggedIn)
                return base.Poll();

            if (Filter.Value == null)
                return base.Poll();

            lastPollRequest?.Cancel();

            var tcs = new TaskCompletionSource<bool>();
            var req = new GetRoomsRequest(Filter.Value);

            req.Success += result =>
            {
                RoomsReceived(result.Where(r => r.Category != RoomCategory.DailyChallenge).ToArray());
                tcs.SetResult(true);
            };

            req.Failure += _ => tcs.SetResult(false);

            api.Queue(req);

            lastPollRequest = req;

            return tcs.Task;
        }
    }
}
