using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;
using NLog.Extensions.Logging;
using CommandLine;
using SeparatedTextFileReader.ConsoleApp.Options;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Infrastructure.FileHelpers;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Infrastructure.DataHelpers;

using SeparatedTextFileReader.ConsoleApp.Services;
using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Infrastructure.Services;

namespace SeparatedTextFileReader.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {



            var arguments = new ArgumentOptions();
            var result = Parser.Default.ParseArguments<ArgumentOptions>(args);


            if (result.Tag != ParserResultType.Parsed)
            {
                var argumentErrors = ((NotParsed<ArgumentOptions>)result).Errors.Select(i => i.Tag.ToString());
                Console.WriteLine(string.Join(", ", argumentErrors));
            }

            else
            {
                Parser.Default.ParseArguments<ArgumentOptions>(args)
                    .WithParsed(options => { arguments = options; });


                var builder = new ConfigurationBuilder();
                BuildConfig(builder);
                var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {

                        var attributeMappingsOptions = context.Configuration.GetSection("AttributeMappingsConfiguration").Get<AttributeMappingsOptions>();


                        services.AddLogging(loggingBuilder =>
                        {
                            loggingBuilder.ClearProviders();
                            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                            loggingBuilder.AddNLog();
                        });
                        services.AddTransient<IFileService>(s => new FileService(arguments.File));
                        services.AddTransient <ILineParser,LineParser>(); 
                        services.AddTransient <IDelimitedFileReaderService,DelimetedFileReaderService>();
                        services.AddTransient<IDeserializeRowData<Procurement>>
                            (s => new DeserializeRowData<Procurement>(attributeMappingsOptions.AttributeMappings));
                    })
                    .UseNLog()
                    .Build();

                var svc = ActivatorUtilities.CreateInstance<ReaderService>(host.Services);
                svc.Run();
            }
        }


        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}