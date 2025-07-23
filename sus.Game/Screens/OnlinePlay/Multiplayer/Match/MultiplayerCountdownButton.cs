// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using Humanizer;
using sus.Framework.Allocation;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.UserInterface;
using sus.Game.Graphics;
using sus.Game.Graphics.UserInterface;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Localisation;
using sus.Game.Online.Multiplayer;
using susTK;

namespace sus.Game.Screens.OnlinePlay.Multiplayer.Match
{
    public partial class MultiplayerCountdownButton : IconButton, IHasPopover
    {
        private static readonly TimeSpan[] available_delays =
        {
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(30),
            TimeSpan.FromMinutes(1),
            TimeSpan.FromMinutes(2)
        };

        public new required Action<TimeSpan> Action;
        public required Action CancelAction;

        [Resolved]
        private MultiplayerClient multiplayerClient { get; set; } = null!;

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        private readonly Drawable background;

        public MultiplayerCountdownButton()
        {
            Icon = FontAwesome.Regular.Clock;

            Add(background = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Depth = float.MaxValue
            });

            base.Action = this.ShowPopover;

            TooltipText = MultiplayerMatchStrings.CountdownSettings;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            background.Colour = colours.Green;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            multiplayerClient.RoomUpdated += onRoomUpdated;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            multiplayerClient.RoomUpdated -= onRoomUpdated;
        }

        private void onRoomUpdated() => Scheduler.AddOnce(() =>
        {
            bool countdownActive = multiplayerClient.Room?.ActiveCountdowns.Any(c => c is MatchStartCountdown) == true;

            if (countdownActive)
            {
                background
                    .FadeColour(colours.YellowLight, 100, Easing.In)
                    .Then()
                    .FadeColour(colours.YellowDark, 900, Easing.OutQuint)
                    .Loop();
            }
            else
            {
                background
                    .FadeColour(colours.Green, 200, Easing.OutQuint);
            }
        });

        public Popover GetPopover()
        {
            var flow = new FillFlowContainer
            {
                Width = 200,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(2),
            };

            foreach (var duration in available_delays)
            {
                flow.Add(new RoundedButton
                {
                    RelativeSizeAxes = Axes.X,
                    Text = MultiplayerMatchStrings.StartMatchWithCountdown(duration.Humanize()),
                    BackgroundColour = colours.Green,
                    Action = () =>
                    {
                        Action(duration);
                        this.HidePopover();
                    }
                });
            }

            if (multiplayerClient.Room?.ActiveCountdowns.Any(c => c is MatchStartCountdown) == true && multiplayerClient.IsHost)
            {
                flow.Add(new RoundedButton
                {
                    RelativeSizeAxes = Axes.X,
                    Text = MultiplayerMatchStrings.StopCountdown,
                    BackgroundColour = colours.Red,
                    Action = () =>
                    {
                        CancelAction();
                        this.HidePopover();
                    }
                });
            }

            return new OsuPopover { Child = flow };
        }
    }
}
