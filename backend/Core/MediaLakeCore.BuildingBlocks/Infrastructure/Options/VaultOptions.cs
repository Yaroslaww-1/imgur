namespace MediaLakeCore.BuildingBlocks.Infrastructure.Options
{
    public class VaultOptions
    {
        public const string Location = "VaultOptions";

        public string Address { get; set; }
        public string Token { get; set; }
        public string MountPoint { get; set; }
    }
}
