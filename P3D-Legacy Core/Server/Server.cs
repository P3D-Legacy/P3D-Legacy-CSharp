using System;

namespace P3D.Legacy.Core.Server
{
    public abstract class Server
    {
        public string IP { get; protected set; }
        public string Port { get; protected set; }
        public virtual string IdentifierName { get; set; }


        public Server(string address)
        {
            if (!address.Contains(":"))
            {
                IP = address;
                Port = "15124";
            }
            else
            {
                IP = address.Split(Convert.ToChar(":"))[0];
                Port = address.Split(Convert.ToChar(":"))[1];
            }
        }


        public abstract string GetAddressString();
        public abstract string GetName();
    }
}
