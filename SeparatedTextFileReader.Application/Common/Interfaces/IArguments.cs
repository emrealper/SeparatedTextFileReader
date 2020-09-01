namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IArguments
    {
        int? Project { get; set; }

        string File { get; set; }

        bool SortByStartDate { get; set; }
    }
}