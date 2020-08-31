using System;
using System.IO;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;
using NLog.Extensions.Logging;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Application.Services.Procurement.Handlers;
using SeparatedTextFileReader.ConsoleApp.Options;
using SeparatedTextFileReader.ConsoleApp.Services;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Infrastructure.DataHelpers;
using SeparatedTextFileReader.Infrastructure.FileHelpers;
using SeparatedTextFileReader.Infrastructure.Services;

namespace SeparatedTextFileReader.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var arguments = new ArgumentOptions();
            var result = Parser.Default.ParseArguments<ArgumentOptions>(args);


            if (result.Tag != ParserResultType.Parsed)
            {
                var argumentErrors = ((NotParsed<ArgumentOptions>) result).Errors.Select(i => i.Tag.ToString());
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
                        services.AddLogging(loggingBuilder =>
                        {
                            loggingBuilder.ClearProviders();
                            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                            loggingBuilder.AddNLog();
                        });

                        services.AddTransient<IFileService, FileService>();
                        services.AddTransient<IFilterByArgumentQueryHandler, FilterByArgumentQueryHandler>();
                        services.AddTransient<ILineParser, LineParser>();
                        services.AddTransient<IDelimitedFileReaderService, DelimetedFileReaderService>();
                        services.AddTransient<IDeserializeRowData<Procurement>>
                            (s => new DeserializeRowData<Procurement>());
                    })
                    .UseNLog()
                    .Build();

                var svc = ActivatorUtilities.CreateInstance<ReaderService>(host.Services);
                svc.Run(arguments);
            }
        }


        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true)
                .AddEnvironmentVariables();
        }
    }
}