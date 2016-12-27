namespace InstagramBot.Data.SQL
{
    public class CheckPermissions
    {
    /*    public Permissons Permissons { get; set; }
        OOGHost Host { get; }

        public CheckPermissions(OOGHost host)
        {
            Host = host;
            Permissons = Check();
        }

        Permissons Check()
        {
            Host.ConnectionInfo.SqlDB.GetLicenseState(
                Host.ConnectionInfo.AccountInfo.LicenseID,
                Host.ConnectionInfo.AccountInfo.SoftwareID,
                out Host.ConnectionInfo.AccountInfo.State);

            Host.ConnectionInfo.SqlDB.GetLicenseTime(
                Host.ConnectionInfo.AccountInfo.LicenseID,
                Host.ConnectionInfo.AccountInfo.SoftwareID,
                out Host.ConnectionInfo.AccountInfo.SoftTime);

            if (!Host.ConnectionInfo.AccountInfo.State) return Permissons = Permissons.Block;

            return Permissons = (Host.ConnectionInfo.AccountInfo.SoftTime - UnixTime.Now).Timestamp > 0
                ? Permissons.Success
                : Permissons.TimeOff;
        }*/
    }
}
