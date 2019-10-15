namespace Reliance.Web.Client.Api
{
    public class PackageDto : BaseDto
    {
        public int VersionMaster { get; set; }
        public int VersionMinor { get; set; }
        public int VersionPatch { get; set; }
        public string TargetFrameWork { get; set; }

        public string Description => $"{Name} - {VersionMaster}.{VersionMinor}.{VersionPatch} - {TargetFrameWork}";
    }
}
