namespace InstagramBot.Data.SQL
{
  /*  public class CreateNewLicense
    {
        public MySqlDatabase SQL = new MySqlDatabase("SERVER=DESKTOP-VBFBI8T;DATABASE=LicenseDataBase;Trusted_Connection=True");

        public bool State { get; private set; }
        public CreateNewLicense(UserData info) : this(info,"")
        {
            
        }
        public CreateNewLicense(UserData info, string skype)
        {
            int pid, sid;
            SQL.GetLicense(info.HardwareData, out pid);
            if(pid != -1)
            {
                State = false;
                return;
            }
            SQL.InsertToLicense(info.HardwareData);
            SQL.GetLicense(info.HardwareData, out pid);
            SQL.GetSoftwareID(info.SoftwareName, out sid);
            SQL.InsertToLicenseState(pid, sid, false);
            SQL.InsertToLicenseTime(pid, sid, UnixTime.ToDateTime(0));
            SQL.InsertToLink(pid, skype);
            SQL.InsertToMD5(pid, sid, info.SoftwareMD5);
            SQL.InsertToOnlineState(pid, false);
            State = true;
        }
    }*/
}
