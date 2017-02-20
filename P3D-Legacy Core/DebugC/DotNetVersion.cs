using System;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Debug
{
    /// <summary>
    /// A class to supply .Net installation information.
    /// </summary>
    public class DotNetVersion
    {

        /// <summary>
        /// Returns .Net installation information.
        /// </summary>
        public static string GetInstalled()
        {
            string output = "";

            try
            {
                using (Microsoft.Win32.RegistryKey ndpKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\"))
                {
                    foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                    {
                        if (versionKeyName.StartsWith("v"))
                        {
                            Microsoft.Win32.RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                            string name = (string)versionKey.GetValue("Version", "");
                            string sp = versionKey.GetValue("SP", "").ToString();
                            string install = versionKey.GetValue("Install", "").ToString();
                            if (string.IsNullOrEmpty(install))
                            {
                                //no install info, ust be later
                                output += versionKeyName + "  " + name + Constants.vbNewLine;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sp) && install == "1")
                                {
                                    output += versionKeyName + "  " + name + "  SP" + sp + Constants.vbNewLine;
                                }
                            }
                            if (!string.IsNullOrEmpty(name))
                            {
                                continue;
                            }
                            foreach (string subKeyName in versionKey.GetSubKeyNames())
                            {
                                Microsoft.Win32.RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                                name = (string)subKey.GetValue("Version", "");
                                if (!string.IsNullOrEmpty(name))
                                {
                                    sp = subKey.GetValue("SP", "").ToString();
                                }
                                install = subKey.GetValue("Install", "").ToString();
                                if (string.IsNullOrEmpty(install))
                                {
                                    //no install info, ust be later
                                    output += versionKeyName + "  " + name + Constants.vbNewLine;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(sp) && install == "1")
                                    {
                                        output += "  " + subKeyName + "  " + name + "  SP" + sp + Constants.vbNewLine;
                                    }
                                    else if (install == "1")
                                    {
                                        output += "  " + subKeyName + "  " + name + Constants.vbNewLine;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                output += "Error getting .Net installation information.";
            }

            return output;
        }

    }
}
