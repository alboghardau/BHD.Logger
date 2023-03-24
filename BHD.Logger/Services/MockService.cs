﻿using System;
using BHD.Logger.Interfaces;
using BHD.Logger.Mock;

namespace BHD.Logger.Services
{
	public class MockService : IMockService
	{
		private ILogGenerator logGenerator;
        private LoggerService loggerService;
        private Thread mockThread;
        private bool stopRequested = false;

		public MockService(LogGenerator logGenerator, LoggerService loggerService)
		{
			this.logGenerator = logGenerator;
            this.loggerService = loggerService;
		}

        public void Start()
        {
            this.stopRequested = false;
            this.mockThread = new Thread(GenerateLog);
            this.mockThread.Start();
        }

        public void Stop()
        {
            stopRequested = true;
            this.mockThread.Join();
        }

        private void GenerateLog()
        {
            while (!stopRequested)
            {
                var log = this.logGenerator.GetRandomLog();
                this.loggerService.AddLog(log);

                //TO DO configurable
                Thread.Sleep(1000);
            }
        }
    }
}

