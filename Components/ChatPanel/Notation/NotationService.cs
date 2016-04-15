using S22.Xmpp.Im;
using System;
using System.Collections.Generic;

namespace UFIP.EngChat.Components.ChatPanel.Notation
{
    /// <summary>
    /// Service allowing a teacher to record and affect a note to a conversation.
    /// The folder must be set in the parameters.
    /// </summary>
    public class NotationService
    {
        /// <summary>
        /// Writes the conversation into a text filer created in the folder set in parameters.
        /// Constructs the conversation to allow identification of the speaker.
        /// </summary>
        /// <param name="messages">The messages exchanged.</param>
        /// <param name="Note">The note given by the teacher.</param>
        /// <param name="jid">The jid of the student .</param>
        /// <param name="contactName">The name of the student.</param>
        public static void WriteConversation(List<Message> messages, string Note, S22.Xmpp.Jid jid, string contactName)
        {
            List<string> text = new List<string>();

            text.Add("Note : " + Note + "\r");

            // Determine who spoke first
            bool isTeacherLastSpeaker = false;
            if (messages[0].From != jid)
                isTeacherLastSpeaker = false;

            // Add the name of the speaker once for every messages in a row
            foreach (var mess in messages)
            {
                if (mess.From.GetBareJid() == jid.GetBareJid())
                {
                    if (isTeacherLastSpeaker)
                    {
                        text.Add("\r\n " + contactName + " : ");
                        isTeacherLastSpeaker = false;
                    }
                }
                else if (!isTeacherLastSpeaker)
                {
                    text.Add("\r\n Vous : ");
                    isTeacherLastSpeaker = true;
                }

                text.Add(mess.Body);
            }

            // Uses an instance of StreamWriter to insert the lines in a .txt
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(

                Properties.Settings.Default.FolderRecord + "/" +
                contactName + "_" +
                DateTime.Now.ToString("yyyyMMddmm") + ".txt"))
            {
                foreach (string line in text)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}