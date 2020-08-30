using CommandLine;
using SeparatedTextFileReader.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.ConsoleApp.Options
{
   public class ArgumentOptions : IArguments
    {

        [Option("file", Required = true, HelpText = "Full path to the input file")]
        public string File { get; set; }
        [Option("sortByStartDate", HelpText = "Sort results by column 'Start date' in ascending order", Default = false)]
        public bool SortByStartDate { get; set; }
        [Option("project", HelpText = "Filter results by column 'Project'", Default = null)]
        public int? Project { get; set; }

        public string[] Errors { get; set; }

    }
}
