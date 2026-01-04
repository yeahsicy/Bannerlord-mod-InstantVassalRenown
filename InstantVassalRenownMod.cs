using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace InstantVassalRenown
{
    public class InstantVassalRenownMod : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (game.GameType is Campaign)
            {
                CampaignGameStarter starter = (CampaignGameStarter)gameStarterObject;
                starter.AddBehavior(new InstantVassalRenownBehavior());
            }
        }
    }

    public class InstantVassalRenownBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener(this, EnsureEnoughRenown);
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, EnsureEnoughRenown);
        }

        void EnsureEnoughRenown(CampaignGameStarter starter)
        {
            EnsureEnoughRenown();
        }
        void EnsureEnoughRenown()
        {
            Campaign.Current.Clans.ForEach(c =>
            {
                if (c == Clan.PlayerClan)
                {
                    if (c.Renown < 150f)
                    {
                        c.ResetClanRenown();
                        c.AddRenown(150f);
                    }
                    return;
                }
            });
        }

        public override void SyncData(IDataStore dataStore) { }
    }
}
