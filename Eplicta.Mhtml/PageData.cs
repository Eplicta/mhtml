using System;

namespace Eplicta.Mhtml;

public record PageData
{
    public Uri MainUri { get; set; }
    public string Title { get; set; }
    public string MainContent { get; set; }
}