namespace CvWebsite.Core.Models;

public record ProjectItem(
    string Slug,
    string Title,
    string ShortDescription,

    string Problem,
    string Solution,
    string Result,

    string Tech,
    string? RepoUrl = null,
    string? LiveUrl = null,
    string[]? Images = null
);

public record WorkItem(
    string Company,
    string Role,
    string Period,
    string Description);

public class NavLabels
{
    public string Home { get; init; } = "Home";
    public string WhoAmI { get; init; } = "Who am I";
    public string Projects { get; init; } = "Projects";
    public string Work { get; init; } = "Work";
}

public class CvData
{
    public string Name { get; init; } = "";
    public string Title { get; init; } = "";
    public string Location { get; init; } = "";
    public string Summary { get; init; } = "";

    public NavLabels Nav { get; init; } = new();

    public List<string> Highlights { get; init; } = [];
    public List<ProjectItem> Projects { get; init; } = [];
    public List<WorkItem> Work { get; init; } = [];
}