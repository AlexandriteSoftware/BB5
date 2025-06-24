namespace BB5.Services;

public interface IMarkdownRenderer
{
    string ToHtml(
        string markdown);
}
