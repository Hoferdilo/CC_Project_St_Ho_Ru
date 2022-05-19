using System;
namespace CloudComputingProject.Service
{
	public interface IQueueService
	{
		void SendMessage(string queueName, string message);
	}
}

