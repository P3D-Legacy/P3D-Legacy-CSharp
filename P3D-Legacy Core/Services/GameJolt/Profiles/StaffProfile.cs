using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3D.Legacy.Core.GameJolt.Profiles
{
    public class StaffProfile
    {


        public static List<StaffProfile> Staff = new List<StaffProfile>();
        public static void SetupStaff()
        {
            Staff.Add(new StaffProfile("17441", "Creator", "nilllzz", new[] {
                StaffArea.GlobalAdmin,
                StaffArea.GTSAdmin,
                StaffArea.GTSDaily,
                StaffArea.MailManagement
            }));
            Staff.Add(new StaffProfile("32943", "Programmator", "dracohouston", new[] { StaffArea.GlobalAdmin }));
            Staff.Add(new StaffProfile("32349", "Dark", "darkfire", new[] {
                StaffArea.GlobalAdmin,
                StaffArea.GTSAdmin,
                StaffArea.GTSDaily,
                StaffArea.MailManagement
            }));
            Staff.Add(new StaffProfile("33742", "Prince", "princevade", new[] {
                StaffArea.GlobalAdmin,
                StaffArea.GTSAdmin,
                StaffArea.GTSDaily,
                StaffArea.MailManagement
            }));
            Staff.Add(new StaffProfile("1", "GameJolt", "cros", new StaffArea[] { }));
            Staff.Add(new StaffProfile("35947", "", "", new[] { StaffArea.GTSDaily }));
        }

        public enum StaffArea
        {
            GTSAdmin,
            GTSDaily,
            MailManagement,
            GlobalAdmin
        }

        public List<StaffArea> StaffAreas = new List<StaffArea>();
        public string RankName = "";
        public string Sprite = "";

        public string GameJoltID = "";
        public StaffProfile(string GameJoltID, string RankName, string Sprite, StaffArea[] StaffAreas)
        {
            this.RankName = RankName;
            this.GameJoltID = GameJoltID;
            this.Sprite = Sprite;
            this.StaffAreas = StaffAreas.ToList();
        }

    }
}
