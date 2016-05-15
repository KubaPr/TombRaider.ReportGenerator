using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Xml.Linq;

namespace TombRaider.ReportGenerator
{
    class QueueMessageReceiver
    {
        List<string> usedMethods = new List<string>();

        public void CheckTheQueue()
        {
            MessageQueue messageQueue = new MessageQueue(@".\Private$\GravesControllerQueue");
            Message[] messages = messageQueue.GetAllMessages();

            foreach (Message message in messages)
            {
                StreamReader sr = new StreamReader(message.BodyStream);

                string msg = "";

                while (sr.Peek() >= 0)
                {
                    msg += sr.ReadLine();
                }

                XElement msgElement = XElement.Parse(msg);
                string methodUsed = (string)msgElement;
                usedMethods.Add(methodUsed);
            }
            // after all processing, delete all the messages from the queue
            messageQueue.Purge();
        }

        public string ComposeEmailMessage()
        {
            string message = "Witaj, " + Environment.NewLine + Environment.NewLine;
            message += "Oto Twoje statystki częstotliwości użycia funkcjonalności TombRaider API:" + Environment.NewLine + Environment.NewLine;
            message += "Sprawdzenie grobu po ID " + WriteMethodUsedCount("Get") + Environment.NewLine;
            message += "Wyszukiwanie grobu " + WriteMethodUsedCount("FindGraves") + Environment.NewLine;
            message += "Zapisanie grobu do bazy grobów użytkownika " + WriteMethodUsedCount("Insert") + Environment.NewLine;
            message += "Sprawdzenie listy grobów użytkownika " + WriteMethodUsedCount("UserGraves") + Environment.NewLine;
            message += Environment.NewLine + "Pozdrawiamy, " + Environment.NewLine + "TombRaider";
            return message;
        }

        private string WriteMethodUsedCount(string methodName)
        {
            int count = usedMethods.Count(m => m.Equals(methodName));
            string message = "(funkcja " + methodName + "): " + count.ToString();
            return message;
        }
    }
}
