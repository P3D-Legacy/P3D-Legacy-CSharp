using System;

using P3D.Legacy.Core.Extensions;

namespace P3D.Legacy.Core.Pokemon
{
    /// <summary>
    /// The basic item that represents a Mail Item.
    /// </summary>
    public abstract class MailItem : Item
    {
        public struct MailData
        {
            public int MailID;
            public string MailHeader;
            public string MailText;
            public string MailSender;
            public int MailAttachment;
            public string MailSignature;
            public string MailOriginalTrainerOT;
            public bool MailRead;
        }

        public override bool CanBeUsedInBattle { get; }
        public override ItemTypes ItemType { get; }
        public override int PokeDollarPrice { get; }

        public static MailData GetMailDataFromString(string s)
        {
            if (s.Contains("|"))
                s = s.Replace("|", "\\,");

            var data = s.Split(new [] {"\\," }, StringSplitOptions.None);

            return new MailData
            {
                MailID = Convert.ToInt32(data[0]),
                MailSender = data[1],
                MailHeader = data[2],
                MailText = data[3],
                MailSignature = data[4],
                MailAttachment = Convert.ToInt32(data[5]),
                MailOriginalTrainerOT = data[6],
                MailRead = Convert.ToBoolean(Convert.ToInt32(data[7]))
            };
        }

        public static string GetStringFromMail(MailData mail) => mail.MailID + "\\," + mail.MailSender + "\\," + mail.MailHeader + "\\," + mail.MailText + "\\," + mail.MailSignature + "\\," + mail.MailAttachment + "\\," + mail.MailOriginalTrainerOT + "\\," + mail.MailRead.ToNumberString();
    }
}
