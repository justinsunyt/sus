// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Logging;
using sus.Framework.Platform;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Overlays;
using sus.Game.Tournament.Components;
using sus.Game.Tournament.IPC;
using susTK;

namespace sus.Game.Tournament.Screens.Setup
{
    public partial class StablePathSelectScreen : TournamentScreen
    {
        [Resolved]
        private TournamentSceneManager? sceneManager { get; set; }

        [Resolved]
        private MatchIPCInfo ipc { get; set; } = null!;

        [Cached]
        private OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Blue);

        private OsuDirectorySelector directorySelector = null!;
        private DialogOverlay? overlay;

        [BackgroundDependencyLoader(true)]
        private void load(Storage storage, OsuColour colours)
        {
            var initialStorage = (ipc as FileBasedIPC)?.IPCStorage ?? storage;
            string? initialPath = new DirectoryInfo(initialStorage.GetFullPath(string.Empty)).Parent?.FullName;

            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    Masking = true,
                    CornerRadius = 10,
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(0.5f, 0.8f),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = colours.GreySeaFoamDark,
                            RelativeSizeAxes = Axes.Both,
                        },
                        new GridContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            RowDimensions = new[]
                            {
                                new Dimension(),
                                new Dimension(GridSizeMode.Relative, 0.8f),
                                new Dimension(),
                            },
                            Content = new[]
                            {
                                new Drawable[]
                                {
                                    new OsuSpriteText
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Text = "Please select a new location",
                                        Font = OsuFont.Default.With(size: 40)
                                    },
                                },
                                new Drawable[]
                                {
                                    directorySelector = new OsuDirectorySelector(initialPath)
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                    }
                                },
                                new Drawable[]
                                {
                                    new FillFlowContainer
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Direction = FillDirection.Horizontal,
                                        Spacing = new Vector2(20),
                                        Children = new Drawable[]
                                        {
                                            new RoundedButton
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                Width = 300,
                                                Text = "Select stable path",
                                                Action = ChangePath
                                            },
                                            new RoundedButton
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                Width = 300,
                                                Text = "Auto detect",
                                                Action = AutoDetect
                                            },
                                        }
                                    }
                                }
                            }
                        }
                    },
                },
                new BackButton
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    State = { Value = Visibility.Visible },
                    Action = () => sceneManager?.SetScreen(typeof(SetupScreen))
                }
            });
        }

        protected virtual void ChangePath()
        {
            string target = directorySelector.CurrentPath.Value.FullName;
            var fileBasedIpc = ipc as FileBasedIPC;
            Logger.Log($"Changing Stable CE location to {target}");

            if (!fileBasedIpc?.SetIPCLocation(target) ?? true)
            {
                overlay = new DialogOverlay();
                overlay.Push(new IPCErrorDialog("This is an invalid IPC Directory", "Select a directory that contains an sus! stable cutting edge installation and make sure it has an empty ipc.txt file in it."));
                AddInternal(overlay);
                Logger.Log("Folder is not an sus! stable CE directory");
                return;
            }

            sceneManager?.SetScreen(typeof(SetupScreen));
        }

        protected virtual void AutoDetect()
        {
            var fileBasedIpc = ipc as FileBasedIPC;

            if (!fileBasedIpc?.AutoDetectIPCLocation() ?? true)
            {
                overlay = new DialogOverlay();
                overlay.Push(new IPCErrorDialog("Failed to auto detect", "An sus! stable cutting-edge installation could not be auto detected.\nPlease try and manually point to the directory."));
                AddInternal(overlay);
            }
            else
            {
                sceneManager?.SetScreen(typeof(SetupScreen));
            }
        }
    }
}
