using System;
using TombRaider.ReportGenerator;

namespace ReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueMessageReceiver queueMessagesReceiver = new QueueMessageReceiver();
            Email email = new Email();

            queueMessagesReceiver.CheckTheQueue();
            string newEmail = queueMessagesReceiver.ComposeEmailMessage();
            email.Send(newEmail);
            Console.WriteLine(newEmail);
            Console.WriteLine("Report e-mail sent!");
            Console.Read();
        }
    }
}
