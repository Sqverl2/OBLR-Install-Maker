//#define DEBUG_CONFIG
using System.Collections.Generic;

namespace OBLRInstall.Data
{
#if DEBUG_CONFIG
    internal class DefaultConfiguration : PackagesConfiguration
    {
        public DefaultConfiguration()
        {
            Version = "1.1";
            Stages = new List<InstallationStage>
            {
                new InstallationStage() { Name = "Step 0", Steps = new List<InstallationStep>{} },
                new InstallationStage() { Name = "Step 1", Steps = new List<InstallationStep>{} },
                new InstallationStage() { Name = "Step 2", Steps = new List<InstallationStep>{} },
                new InstallationStage() { Name = "Step 3", Steps = new List<InstallationStep>{} },
                new InstallationStage() { Name = "Step 4", Steps = new List<InstallationStep>{} },
                new InstallationStage() { Name = "Step 5", Steps = new List<InstallationStep>{} },
                new InstallationStage() { Name = "Step 6", Steps = new List<InstallationStep>{} },
            };
        }
    }
#else

    internal class DefaultConfiguration : PackagesConfiguration
    {
        public DefaultConfiguration()
        {
            Version = "1.1";
            Stages = new List<InstallationStage>
            {
                new InstallationStage()
                {
                    Name = "Step 0",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "4GB RAM Patch",
                            Description = "Increases the amount of RAM the game is allowed to use.",
                            FileName = "4gb_Patch_plus_Ram_optimization_readme-45576-1-0-0-1.zip",
                            Operation = StepType.UNPACK,
                        },
                        new InstallationStep()
                        {
                            Name = "Oblivion Mod Manager",
                            Description = "The only application you will need to manage the mods.",
                            FileName = "obmm 1_1_12 full installer-2097.zip",
                            Operation = StepType.UNPACK,
                        },
                        new InstallationStep()
                        {
                            Name = "Oblivion Script Extender",
                            Description = "Increases Oblivion's scripting capabilities.",
                            FileName = "OBSE 0021-37952-0021.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Oblivion Script Extender",
                        },
                    },
                },
                new InstallationStage()
                {
                    Name = "Step 1",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "A Tweaked ENB",
                            Description = "Resource that overhauls the games lighting.",
                            FileName = "ATE181_1_beta-44146-1beta.zip",
                            Operation = StepType.UNPACK,
                            Include = { @"\*.fx", @"\*.bmp", @"\*.tga", @"01Optional effects\Film filter\*", @"01Optional effects\SMAA\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "Oblivion Reloaded",
                            Description = "Changes many game mechanics such as adding the ability to dodge by double tapping W,A,S, or D and adding gasping/heartbeat noises whenever your fatigue/health gets low, making the game more immersive. Also enhances graphics.",
                            FileName = "OR7.1.0.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"Docs\" },
                        },
                        new InstallationStep()
                        {
                            Name = "Oblivion Stutter Remover",
                            Description = "Removes stutter, I guess...",
                            FileName = "OSR_4-1-37-23208-4-1-37.zip",
                            Operation = StepType.UNPACK,
                        },
                    },
                },
                new InstallationStage()
                {
                    Name = "Step 2",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "Better Cities",
                            Description = "Completely expands upon the major cities in the game. Vanilla stuff hasn't moved to disrupt the game but the surrounding ares are enhanced with stores, people, docks, boats, farms, etc.",
                            FileName = "Better Cities v6.0.11a-16513-6-0-11-1581427434.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"00 Core\*", @"01 Better Cities Full\*", @"02 Better Imperial City Full\*", @"07 IC Imperial Isle\*", @"30 Knights of the Nine Chorrol\*", @"31 Oblivion Uncut\*", @"40 Bravil Vanilla\*" },
                            Ignore = { @"00 Core\Docs\" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Better Dungeons BSA version",
                            Description = "Redesigns most of the dungeons in the game to be more diverse and dynamic.",
                            FileName = "Better Dungeons BSA-40392-12.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Better Dungeons Patch",
                            Description = "Patch for better dungeons.",
                            FileName = "Better Dungeons-40392-v13.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"*.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "Oblivion Uncut",
                            Description = "Adds all of the cut content back to the game.",
                            FileName = "Oblivion Uncut - X-47191-X-1580561945.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                    },
                },
                new InstallationStage()
                {
                    Name = "Step 3",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "Qarls Texture Pack",
                            Description = "HD texture pack.",
                            FileName = "QTP3 Redimized 1.0 Patched-45666-1-0.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.txt", @"textures\architecture\bravil\bravilentrancegate01.dds", @"textures\architecture\bravil\bravilentrancegate01_n.dds" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Character Overhaul",
                            Description = "HD character model texture pack.",
                            FileName = "Oblivion Character Overhaul v203-44676-2-03.zip",
                            Operation = StepType.UNPACK,
                            Include = { @"Data\*" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Improved Trees and Flora 2",
                            Description = "HD texture pack for trees and flora (only trees used)",
                            FileName = "Improved Trees and Flora 2-11891.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"textures\trees\" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Improved Trees and Flora 2 - Bark",
                            Description = "Package of HD tree bark textures",
                            FileName = "ITFBark2012-11891-1.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"textures\trees\" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Improved Trees and Flora 2 - Update",
                            Description = "Update with reworkeded color saturation of several textures and new flora/tree textures.",
                            FileName = "ITF2Update-11891.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"textures\",  @"meshes\" },
                            Ignore = { @"textures\plants\",  @"meshes\plants\" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Grass Overhaul",
                            Description = "HD grass texture pack.",
                            FileName = "Grass Overhaul v61-42400-v6-1.zip",
                            Operation = StepType.UNPACK,
                            Ignore = { @"Grass Overhaul_xUL_Imperial_Isle_patch.esp" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Elven Map Redux",
                            Description = "HD color maps for Tamriel.",
                            FileName = "Elven Map Redux-3002.rar",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "ElvenElven Map For Shivering Isles",
                            Description = "HD color maps for Tamriel.",
                            FileName = "Elven Map For Shivering Isles-16640.zip",
                            Operation = StepType.UNPACK,
                            Ignore = {  @"\*.txt", @"\*.jpg" },
                        },
                        new InstallationStep()
                        {
                            Name = "Blockhead",
                            Description = "An OBSE plugin that adds support for model/animationt/texture overriding.",
                            FileName = "Blockhead 11.0-43752-11-0.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"OBSE\" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Better Cities - Vanilla Ferns",
                            Description = "Changes the ferns (type of plants) placed by Better Cities to use the vanilla Oblivion meshes which should fix large performance degradation experienced when using Better Cities together with a mod that changes those plants to more detailed meshes (ex.Grass Overhaul).",
                            FileName = "Better Cities - Vanilla Ferns-47880-0-5.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.txt" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Alive Waters",
                            Description = "Adds realistic underwater environments.",
                            FileName = "AliveWaters-6914.rar",
                            Operation = StepType.UNPACK,
                            Ignore = { @"AliveWaters - Slaughterfish Addon.esp", @"\*.txt" },
                            DestinationFolder = "Data",
                        },
                    },
                },
                new InstallationStage()
                {
                    Name = "Step 4",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "A Brotherhood Renewed",
                            Description = "Expands upon the Dark Brotherhood quest line.",
                            FileName = "A Brotherhood Renewed-31773-1-1-2-1557803995.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.txt" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Arena Alive",
                            Description = "Makes the Arena realistic with tons of new NPCs in the stands with different animations. Also allows nightime fighting and looting.",
                            FileName = "ArenaAlive24HrV2-13099.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.txt" },
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Fighters Guild Quests",
                            Description = "Expands upon the Fighters Guild quests by adding more quest givers in each of the halls.",
                            FileName = "Fighters Guild Quests 1_8-41012-1-7.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"Fighters Guild Quests 1_8\Data\*" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Knights of the Nine Revelation",
                            Description = "Expands upon the Knights of the Nine DLC. Upon completion of the DLC, sleep in the master bed in the Priory of the Nine.",
                            FileName = "Knights of the Nine Revelation 1_3_7 Full Install-42490-1-3-7.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.pdf" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Lost Paladins of the Divines",
                            Description = "A whole new branch of quests to dive into for a great reward! Go to the first edition and ask for the Lost Paladin of the Divines.",
                            FileName = "Lost Paladins of the Divines 1_0 OMOD-Ready-10505.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"omod conversion data\", @"\*.txt", @"\*.jpg" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Mannimarco Resurrection",
                            Description = "Expands upon the Mages Guild quest line.",
                            FileName = "Mannimarco Resurrection 2_5-15251.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"MannimarcoResurrection\*.bsa", @"MannimarcoResurrection\*.esp" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "The Imperial Legion Version 4 - Part 1",
                            Description = "Lets you become an Oblivion cop. lol. just talk to the Imperial jailors once your'e a decent citizen.",
                            FileName = "The Imperial Legion Version 4 Part 1-39695-4.rar",
                            Operation = StepType.UNPACK,
                            Include = { @"The Imperial Legion v4 Part 1\*" },
                            Ignore = { @"The Imperial Legion v4 Part 1\*.txt" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "The Imperial Legion Version 4 - Part 2",
                            Description = "Oblivion cop vol.2",
                            FileName = "The Imperial Legion V4 Part 2-40537-4-2.rar",
                            Operation = StepType.UNPACK,
                            Include = { @"The Imperial Legion v4 Part 2\*" },
                            Ignore = { @"The Imperial Legion v4 Part 2\*.txt" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "The Imperial Legion Version 4 - Part 3",
                            Description = "Oblivion cop vol.3",
                            FileName = "The Imperial Legion V4 Part 3 Non OMOD-40823-4-3.rar",
                            Operation = StepType.UNPACK,
                            Include = { @"The Imperial Legion v4 Part 3\*.bsa", @"The Imperial Legion v4 Part 3\*.esp" },
                            DestinationFolder = @"Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Thieves Guild HQ",
                            Description = "Expands upon the Thieves Guild quest line.",
                            FileName = "Thieves Guild HQ - Unhealthy Competition-34465.7z",
                            Operation = StepType.UNPACK,
                            Ignore = { @"\*.txt" },
                            DestinationFolder = @"Data",
                        },
                    },
                },
                new InstallationStage()
                {
                    Name = "Step 5",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "All Plus Five Attribute Modifiers",
                            Description = "Makes it to where the three attributes you choose when you level up are now maxed to 5",
                            FileName = "AllPlusFiveAttributeModifiers.zip-2691.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Bag of Holding",
                            Description = "Adds a 'holdable' container, basically Infinite inventory space. Check out Vilverin",
                            FileName = "Bag of Holding-4065.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.html" },
                        },
                        new InstallationStep()
                        {
                            Name = "Command Mount",
                            Description = "Adds a spell to command owned mount.",
                            FileName = "CommandMount-27957.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Crowded Cities",
                            Description = "Adds more people so there's no more ghost towns.",
                            FileName = "Crowded Cities 12-6665.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Crowded Cities 15.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "Crowded Roads",
                            Description = "Adds people to the roads throughout the lands.",
                            FileName = "Crowded Roads Revamped-4805.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Enchantment Mastery",
                            Description = "Releases some of the restrictions of vanilla enchanting like enchanting arrows",
                            FileName = "Enchantment Mastery 107-24915.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"\*.esp", @"\*.ini", },
                        },
                        new InstallationStep()
                        {
                            Name = "Enhanced Economy",
                            Description = "Balances the economy",
                            FileName = "Enhanced Economy 5_4_3-25078.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"00 Core\*", @"40 Better Cities - House price x 7\*", },
                        },
                        new InstallationStep()
                        {
                            Name = "Faster Horses",
                            Description = "Makes 4 legged vehicle transportation tolerable.",
                            FileName = "Faster Horses-20265.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Immersive Weapons",
                            Description = "Adds lore-friendly, medieval weapons to the game.",
                            FileName = "Immersive Weapons 2-43879-2.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "ImpeREAL Forts",
                            Description = "Adds Imperial forts around the world that feel like 'local police stations'",
                            FileName = "ImpeREAL Empire Unique Forts v2_61-22965-v2-61.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Jail Break Escape Routes",
                            Description = "Adds secret routes in every castle for an enhanced escaping experience ",
                            FileName = "Jail Break Escape Routes v1_2-39385-v1-2.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Journal Mod",
                            Description = "You now have a journal in your inventory that you can write in",
                            FileName = "Journal Mod v3x2x1-15294.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"menus\", @"textures\", @"meshes\", @"\*.esp", @"book_menu (vanilla-or-BTmod).xml" },
                        },
                        new InstallationStep()
                        {
                            Name = "Journal Mod HOTFIX",
                            Description = "HOTFIX for Journal Mod.",
                            FileName = "Journal Mod v3x2x1 HOTFIX-15294.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Keychain",
                            Description = "Adds a keychain to contain your keys",
                            FileName = "Keychain v5_00-3409.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"textures\", @"meshes\", @"\*.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "Let There Be Darkness",
                            Description = "Remove all of the artificial light in the dungeons.",
                            FileName = "Let There Be Darkness v1dot2-22819.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"meshes\", @"Let There Be Darkness - Cyrodiil + SI.esp", @"Let There Be Darkness - Knights.esp", @"Let There Be Darkness - Mehrunes Razor.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "Lightweight Potions",
                            Description = "Makes user created potion weight static instead of ingredient based.",
                            FileName = "Lightweight Potions 1_1-25714.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"\*.esp", @"\*.ini", },
                        },
                        new InstallationStep()
                        {
                            Name = "Loading Screens",
                            Description = "Adds cinematic, lore-friendly screenshots to the loading screen pool that reflect the vanilla text.",
                            FileName = "LoadingScreens-13012.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"LoadingScreens\*" },
                            Ignore = { @"LoadingScreens\*.jpg", @"LoadingScreens\*.htm" },
                        },
                        new InstallationStep()
                        {
                            Name = "Menuque",
                            Description = "An OBSE Plugin that adds various additional UI, quest, and misc functions.",
                            FileName = "MenuQue v16b-32200-v16b.zip",
                            Operation = StepType.UNPACK,
                            Include = { @"Data\OBSE\" },
                        },
                        new InstallationStep()
                        {
                            Name = "My Voice Extender",
                            Description = "Adds multiple different passive dialogue options for the player via spell.",
                            FileName = "My Voice Extender 1_4_3-29067-1.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"textures\", @"meshes\", @"sound\", @"\*.esp", @"\*.ini" },
                        },
                        new InstallationStep()
                        {
                            Name = "Necromancy and Lichdom",
                            Description = "Let you become a true necromancer and 'create' your own followers. Starts when you find a book in  the quest Where spirits have lease.",
                            FileName = "Necromancy and Lichdom 2.1-49700-2-1-1575728703.7z",
                            Operation = StepType.UNPACK,
                        },
                        new InstallationStep()
                        {
                            Name = "Pet Your Animals",
                            Description = "Literally the most important mod on the list. Leave no goodest boy unpetted.",
                            FileName = "Pet your animals-48398-3-0-1542606226.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"pet your animals\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "Pluggy",
                            Description = "A multifunction OBSE plugin that adds new functions to Oblivion, and is a further extension to Oblivion Script Extender.",
                            FileName = "Pluggy v125b-23979.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = @"Data\OBSE\Plugins",
                            Include = { @"*.dll", @"*.dlx" },
                        },
                        new InstallationStep()
                        {
                            Name = "Populated Prisons",
                            Description = "Here, have an inmate",
                            FileName = "Populated Prisons 12-25232-1-2.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Artifacts Proper",
                            Description = "Adds more artifacts to find that act as buffs located throughout the game.",
                            FileName = "PTArtifactsProperV11-29931.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Reznod Mannequins",
                            Description = "Unleash your inner creepy hat lady. Adds Mannequins you can place anywhere and outfit with your gear. Available for sale in every major city.",
                            FileName = "Reznod Mannequins-2055.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Sounds of Cyrodil",
                            Description = "Adds ambient sounds for immersion such as city noises and environmental noises.",
                            FileName = "SoC Full Version -39804-1-1.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Sound\", @"\*.esp" },
                            Ignore = { @"Sound\fx\Cliffworms\SoundsOfCyrodiil\Taverns\" },
                        },
                        new InstallationStep()
                        {
                            Name = "Spell Delete And Item Remove",
                            Description = "Lets you delete spells you don't use anymore and glitched quest items Just press delete key.",
                            FileName = "Spell Delete And Item Remove 4_0-23069.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Training Uncap",
                            Description = "Max out all your skills with MONEY. Ooh yeah, Oblivion is now pay to win baby.",
                            FileName = "Training Uncap-16869.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Unlimited Amulets and Rings",
                            Description = "Be a pimp, wear a ring on evey finger + 1.",
                            FileName = "Unlimited Amulets and Rings-2246.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.rtf" },
                        },
                        new InstallationStep()
                        {
                            Name = "Weapon Improvement Project",
                            Description = "Adds HD textures for the vanilla weapons.",
                            FileName = "Weapon Improvement Project-43852-1-9.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"Docs\" },
                        },

                    },
                },
                new InstallationStage()
                {
                    Name = "Step 6",
                    Steps = new List<InstallationStep>
                    {
                        new InstallationStep()
                        {
                            Name = "Better Cities - The Lost Spires",
                            Description = "Enhanced The Lost Spires cities.",
                            FileName = "Better Cities v6.0.11a-16513-6-0-11-1581427434.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"30 The Lost Spires\*" },
                            SuggestedSourcePath = "2",
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Enhanced Economy - Bartholm House Price",
                            Description = "Balances real estate economy of Bartholm.",
                            FileName = "Enhanced Economy 5_4_3-25078.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"30 Bartholm - House price x 7\*" },
                            SuggestedSourcePath = "5",
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Adaman Fortress",
                            Description = "One of the only added player homes northwest of Anvil on the border of Cyrodil.",
                            FileName = "Adaman Fortress v1_01-24831.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Bartholm",
                            Description = "Adds a new major city in the Niben Valley with its own quests to enjoy.",
                            FileName = "Bartholm70 Full-5022.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Brutal Underground",
                            Description = "Adds an underground arena to conquer. Just ask around the Merchants Inn in the Imperial City.",
                            FileName = "Brutal Underground - main file-39877-1-1.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Children of Cyrodil",
                            Description = " Adds kid NPCs (voiced)(and annoying)(and talking to strangers and willing to take crap from them)(and hellbent on following you through space and time)(and did I mentione annoying?)(sadly, the towns do feel dead without the buggers).",
                            FileName = "Children of Cyrodiil ver 1 1-45948-.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Creature Diversity",
                            Description = "Makes it to where creatures have different looks so every animal doesn't look like a clone. (Read: more types of dogs, there is always space for more types of dogs.)",
                            FileName = "Creature Diversity 1.14-26634-1-14.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Cloud Ruler Temple Alive",
                            Description = "Enhances Cloud Ruler Temple to be more immersive and realistic",
                            FileName = "CRTAlive-42929-.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Cloud Ruler Temple Alive\*" },
                            Ignore = { @"Cloud Ruler Temple Alive\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Dibellas Watch",
                            Description = "A whole new continent to explore. Ask an Orc at the Anvil docks or an Argonian at the Imperial City Waterfront to charter you there.",
                            FileName = "Dibellas Watch-37120-1.zip",
                            Operation = StepType.UNPACK,
                            Include = { @"DibellasWatch\*" },
                            Ignore = { @"DibellasWatch\Data\*.txt", @"DibellasWatch\Data\DWOptional\"  },
                        },
                        new InstallationStep()
                        {
                            Name = "Dibellas Watch Silent Voice",
                            Description = "A longer silent file for voice. Due to mod having a lot of written non-voiced text.",
                            FileName = "Dibellas Watch-37120-1.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"DibellasWatch\Data\DWOptional\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "Dibellas Watch Patch",
                            Description = "A patch for Dibellas Watch, I'm not sure how this can get confusing.",
                            FileName = "Dibellas Watch Patch-37120.zip",
                            Operation = StepType.UNPACK,
                            Include = { @"DibellasWatchPatch\*" },
                            Ignore = { @"DibellasWatchPatch\Data\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Dive Rock Revisited",
                            Description = "Adds an abandoned building you can claim that was lived in before Underfrykte Matron made it available.",
                            FileName = "Dive Rock Revisited-10522.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Dive Rock Revisited.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "Archmages Study",
                            Description = "Adds a study in the top of the University tower",
                            FileName = "DJZ_Archmages_Study-28130.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Elsweyr Anequia﻿﻿﻿﻿﻿",
                            Description = "Have you ever wanted to travel elsewhere? Well now you can. This mod adds the region of Elsweyr packed full of quests and content.",
                            FileName = "Elsweyr Anequina-25023-March-2014-1561735850.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"00 ElsweyrAnequina-ReadMe-Quests-Guide\", @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Hackdirt The Deep Ones",
                            Description = "Expands upon the little community and their beliefs",
                            FileName = "Hackdirt The Deep Ones 2_9-36224-2-9.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"HackdirtTheDeepOnes2.9\Data\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "ImpeREAL Forts - Lost Spires",
                            Description = "The Lost Spires patch for ImpeREAL Forts",
                            FileName = "ImpeREAL Forts - Lost Spires-34010.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Ivellon",
                            Description = "I swear this dude made it install just to screw with me the mod. But I guesss this is a huge quest mod with a whole story to experience. The Countess of Skingrad keeps a sacred book about it...",
                            FileName = "Ivellon-1.7a-English.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = @"..\Ivelon(remove after installing)",
                            Include = { @"Setup.exe" },
                        },
                        new InstallationStep()
                        {
                            Name = "Ivellon Installation",
                            Description = "The f* u and ur auto matation mod installation step. Go watch the video at 37:46 ( https://youtu.be/jS08eR8AKEU?t=2266 ). You can resume after copying the files.",
                            FileName = "Setup.exe",
                            Operation = StepType.RUN,
                            Include = { "Ivellon.esp" },
                            DestinationFolder = "Data",
                            SuggestedSourcePath = @"Ivelon(remove after installing)",
                        },
                        new InstallationStep()
                        {
                            Name = "Ivellon Patch",
                            Description = "A patch for the \"overly complicated installation process\" the mod. Just so it can have more steps.",
                            FileName = "Ivellon_Patch_1.8.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Ivellon Patch 1.8\English\*" },
                            Ignore = { @"Ivellon Patch 1.8\English\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "Kvatch Rebuilt",
                            Description = "If only there was a more descriptive name for what this does. And an even spoilery description. I guess Dumbledore dies, Han Solo too... Basically, this mod allows you to bring Kvatch back to life after the first oblivion gate. You rebuild the city through a series of quests.",
                            FileName = "Kvatch Rebuilt 3.0 RC3-15412-3-0RC3-1565022556.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Midas Magic Spells of Aurum",
                            Description = @"Adds tons of new spells with unique effects and animations with a whole new spellcrafting system (doesn't affect vanilla stuff) Also new worlds to explore and quests to experience. He's located at the Arcane University.",
                            FileName = "MIDAS MAGIC SPELLS OF AURUM 0995-9562.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"MidasSpells.bsa" },
                        },
                        new InstallationStep()
                        {
                            Name = "Midas Magic Spells of Aurum Patch",
                            Description = @"A patch for that one mod, I wonder which one it was... I think it had something to do with gold. And for some reason I remember screaming. Weird.",
                            FileName = "MIDAS MAGIC SPELLS OF AURUM 0995 Patch A-9562.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Shadowcrest Vineyard",
                            Description = "Ever wanted to make your own wine in Oblivion? Me neither, but the house is nice. This adds a functioning vineyard player-home. It all starts with a note left in the chapel in Skingrad.",
                            FileName = "Shadowcrest Vineyard -22810.7z",
                            Operation = StepType.UNPACK,
                            Include = { @"Data" },
                        },
                        new InstallationStep()
                        {
                            Name = "Shadowcrest Vineyard Guard Addon",
                            Description = @"That wine you are never going to make needs guarding.",
                            FileName = "SV_Guard_Addon-22810.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Data\*" },
                            Ignore = { @"Data\Shadowcrest_Vineyard_GuardCOBL.esp" }
                        },
                        new InstallationStep()
                        {
                            Name = "Spire of Eternity",
                            Description = @"Adds a raid dungeon located east of Vilverin",
                            FileName = "Spire of Eternity-49763-1-0-1576272654.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                        },
                        new InstallationStep()
                        {
                            Name = "Trade and Commerce",
                            Description = @"Allows you to own your own store and sell your loot. Because let us be honest, no NPC ever can afford all of it.",
                            FileName = "TaC 1_5-39255-V1-5.rar",
                            Operation = StepType.UNPACK,
                            Include = { @"Data\textures\", @"Data\meshes\", @"Data\ini\", @"Data\Trade and Commerce.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "The Forgotten Realm Part 1",
                            Description = @"A quest mod near Leyawiin. Look for a red robed man near the North East Gate",
                            FileName = "The Forgotten Realm Part 1 of 3-35696-1-6.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"TheForgottenRealm 1_6 Part 1 of 3\Data\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "The Forgotten Realm Part 2",
                            Description = @"A quest mod near Leyawiin. Look for a red robed man near the North East Gate",
                            FileName = "The Forgotten Realm Part 2 of 3-35696.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"TheForgottenRealm 1_3 Part 2 of 3\Data\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "The Forgotten Realm Part 3",
                            Description = @"A quest mod near Leyawiin. Look for a red robed man near the North East Gate",
                            FileName = "The Forgotten Realm Part 3 of 3-35696.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"TheForgottenRealm 1_3 Part 3 of 3\Data\*" },
                        },
                        new InstallationStep()
                        {
                            Name = "The Lost Spires",
                            Description = @"What's with all dem spires popping out everywhere? A huge quest mod with a whole story to experience. Just join the Archaeology guild.",
                            FileName = "thelostspiresv14.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"*.bsa", @"*.esp" },
                        },
                        new InstallationStep()
                        {
                            Name = "Tower Meshes",
                            Description = @"Archmages Study fixed meshes and textures for towers. (If you are not sure what a msh is - it's a 3d model)",
                            FileName = "Tower Meshes-28130.zip",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"textures\", @"meshes\" },
                        },
                        new InstallationStep()
                        {
                            Name = "Throne of The Emperor",
                            Description = @"Give the man a seat.",
                            FileName = "TTOTE-15419.rar",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Ignore = { @"\*.txt" },
                        },
                        new InstallationStep()
                        {
                            Name = "The Province of Zedar",
                            Description = @"A whole new world with its own story. A portal lies open in Cyrodil somewhere near the Imperial city.",
                            FileName = "Zedar v1_0-17018.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"The lost province of  Zedar\*" },
                            Ignore = { @"The lost province of  Zedar\Readme\" },
                        },
                        new InstallationStep()
                        {
                            Name = "The Province of Zedar Update",
                            Description = @"An update for Zedar mod.",
                            FileName = "Update - v1_0 to v1_1-17018.7z",
                            Operation = StepType.UNPACK,
                            DestinationFolder = "Data",
                            Include = { @"Zedar-Update v1.1\*" },
                            Ignore = { @"Zedar-Update v1.1\Readme\" },
                        },
                        new InstallationStep()
                        {
                            Name = "Done",
                            FileName = string.Empty,
                            Description = @"Go watch the video at about 41 minute mark https://youtu.be/jS08eR8AKEU?t=2462. And proceeed from there.",
                            Operation = StepType.RUN,
                        },
                    },
                },
            };
        }
    }
#endif // DEBUG_CONFIG
}
