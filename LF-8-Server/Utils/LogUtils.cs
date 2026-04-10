using Microsoft.Extensions.Logging;

namespace LF_8_Server.Utils
{
	internal partial class LogUtils
	{
		public static ILogger Logger { get; set; }

		public static void Initialize()
		{
			StreamWriter logFileWriter = new(".\\events.log", append: true);
			ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
			{
				//Add console output
				builder.AddSimpleConsole(options =>
				{
					options.IncludeScopes = true;
					options.SingleLine = true;
					options.TimestampFormat = "HH:mm:ss ";
				});

				builder.AddProvider(new CustomFileLoggerProvider(logFileWriter));
			});

			Logger = loggerFactory.CreateLogger<Program>();
		}

		[LoggerMessage(Level = LogLevel.Warning, Message = "{Metric} alert for Client {Client}")]
		public static partial void LogAlert(ILogger logger, string client, string Metric);

		[LoggerMessage(Level = LogLevel.Warning, Message = "{Metric} alert for Client {Client} cleared")]
		public static partial void LogAlertCleared(ILogger logger, string client, string Metric);
	}
}
