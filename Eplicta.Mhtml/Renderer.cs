using System;
using System.IO;

namespace Eplicta.Mhtml;

public class Renderer
{
    private readonly PageData _pageData;

    public Renderer(PageData pageData)
    {
        _pageData = pageData;
    }

    public MemoryStream GetStream()
    {
        using var stream = new MemoryStream();
        return stream;
    }
}