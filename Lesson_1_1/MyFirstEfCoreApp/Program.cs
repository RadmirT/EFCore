using Microsoft.Extensions.Logging;
using MyFirstEfCoreApp;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = factory.CreateLogger<Program>();
Commands.WipeCreateSeed(false);
Commands.ListAll();
Commands.ChangeWebUrl();

