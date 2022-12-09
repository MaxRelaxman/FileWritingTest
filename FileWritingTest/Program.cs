using FileWritingTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

//configure 
PersonLib.Settings.PersonFilename = config.GetValue<string>("personSettings:PersonFilename") ?? "Persons.txt";
PersonLib.Settings.FilePath = config.GetValue<string>("personSettings:FilePath") ?? Directory.GetCurrentDirectory();
PersonLib.Settings.SpouseFilePath = config.GetValue<string>("personSettings:SpouseFilePath") ?? Directory.GetCurrentDirectory();
PersonLib.Settings.MaxSurNameLen = config.GetValue<int>("personSettings:MaxSurnameLen");
PersonLib.Settings.MaxFirstNameLen = config.GetValue<int>("personSettings:MaxFirstNameLen");
PersonLib.Settings.MinAge = config.GetValue<int>("personSettings:MinAge");
PersonLib.Settings.MaxAgeOverrideReq = config.GetValue<int>("personSettings:MaxAgeOverrideReq");


GetPerson gp = new GetPerson();
gp.Start();
