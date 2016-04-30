using Sharp.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YACA.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new XmppClient("fysh.in", "rubot", "1Ewpi36aLe"))
            {
                client.Message += OnNewMessage;

                client.Connect();
                System.Console.WriteLine("Connected as " + client.Jid + Environment.NewLine);
                System.Console.WriteLine(" Type 'send <JID> <Message>' to send a chat message, or 'quit' to exit.");
                System.Console.WriteLine(" Example: send user@domain.com Hello!");
                System.Console.WriteLine();

                while (true)
                {
                    System.Console.Write("> ");
                    string s = System.Console.ReadLine();
                    if (s.StartsWith("send "))
                    {
                        Match m = Regex.Match(s, @"^send\s(?<jid>[^\s]+)\s(?<message>.+)$");
                        if (!m.Success)
                            continue;
                        string recipient = m.Groups["jid"].Value, message = m.Groups["message"].Value;
                        // Send the chat-message.
                        client.SendMessage(recipient, message);
                    }
                    if (s == "quit")
                        return;
                }
            }
        }

        static void OnNewMessage(object sender, Sharp.Xmpp.Im.MessageEventArgs e)
        {
            System.Console.WriteLine("Message from <" + e.Jid + ">: " + e.Message.Body);
        }
    }
}
