namespace DungeonCrawler.Items
{
    internal class HealthPotionI : UsableItem
    {
        public HealthPotionI()
        {
            Name = "Health Potion I";
            Description = "Heals player for 50 amount of hp.";
        }
        public override void Use(Player player)
        {
            // TODO add something like player.Health += 50;
        }
    }
}