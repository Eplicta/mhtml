using System;
using System.IO;
using System.Text;

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
        var key = Guid.NewGuid();

        var sb = new StringBuilder();
        sb.AppendLine("From: <Saved by Eplicta>");
        sb.AppendLine($"Snapshot-Content-Location: {_pageData.MainUri}");
        sb.AppendLine($"Title: {_pageData.Title}");
        sb.AppendLine("Date: Sat, 24 Sep 2022 00:34:32 -0000"); //TODO: Set to current date in correct format
        sb.AppendLine("MIME-Version: 1.0");
        sb.AppendLine("Content-Type: multipart/related;");
        sb.AppendLine("\ttype=\"text/html\";");
        sb.AppendLine($"\tboundary=\"----MultipartBoundary--{key}----\"");
        sb.AppendLine("");

        sb.AppendLine($"------MultipartBoundary--{key}----");
        sb.AppendLine("Content-Type: text/html");
        sb.AppendLine("Content-ID: <frame-211DC617BE30E8F7381F1F873D9F5310@mhtml.eplicta>");
        sb.AppendLine("Content-Transfer-Encoding: quoted-printable");
        sb.AppendLine($"Content-Location: {_pageData.MainUri}");
        sb.AppendLine("");

        sb.AppendLine(QuotedPrintableEncode(_pageData.MainContent, Encoding.UTF8));

        sb.AppendLine("");
        sb.AppendLine($"------MultipartBoundary--{key}------");

        using var stream = new MemoryStream();

        var writer = new StreamWriter(stream);
        writer.Write(sb.ToString());
        writer.Flush();
        stream.Position = 0;

        return stream;
    }

    private static string QuotedPrintableEncode(string s, Encoding e)
    {
        var lastSpace = 0;
        var lineLength = 0;
        var lineBreaks = 0;
        var sb = new StringBuilder();

        if (string.IsNullOrEmpty(s))
        {
            return "";
        }

        foreach (var c in s)
        {
            var ascii = Convert.ToInt32(c);

            if ((ascii == 61) | (ascii > 126))
            {
                if (ascii <= 255)
                {
                    sb.Append("=");
                    sb.Append(Convert.ToString(ascii, 16).ToUpper());
                    lineLength += 3;
                }
                else
                {
                    foreach (var b in e.GetBytes(c.ToString()))
                    {
                        sb.Append("=");
                        sb.Append(Convert.ToString(b, 16).ToUpper());
                        lineLength += 3;
                    }
                }
            }
            else
            {
                sb.Append(c);
                lineLength += 1;
                if (ascii == 32) lastSpace = sb.Length;
            }

            if (lineLength >= 73)
            {
                if (lastSpace == 0)
                {
                    sb.Insert(sb.Length, "=" + Environment.NewLine);
                    lineLength = 0;
                }
                else
                {
                    sb.Insert(lastSpace, "=" + Environment.NewLine);
                    lineLength = sb.Length - lastSpace - 1;
                }

                lineBreaks += 1;
                lastSpace = 0;
            }
        }

        if (lineBreaks > 0)
        {
            if (Equals(sb[sb.Length - 1], ' '))
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append("=20");
            }
        }

        return sb.ToString();
    }
}