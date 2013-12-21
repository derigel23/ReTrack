using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ReTrack.Engine;

namespace ReTrack.UI.Infrastructure
{
  public class CommentProcessor
  {
    private const CallingConvention convention = CallingConvention.Cdecl;

    [DllImport("Typografix.Bitmap.dll", CharSet = CharSet.Unicode, CallingConvention = convention)]
    public static extern int RenderMarkup(IntPtr dst, string markup, int width,
      int height, int stride, string fontFamily, float fontSize, bool smallCaps, bool italic,
      bool contextualLigatures, bool standardLigatures, bool swash, int stylisticSet,
      float lineSpacing, float baseline, bool retina = false);

    public static Bitmap Crop(Bitmap bmp)
    {
      return Crop(bmp, false);
    }


    [DllImport("Typografix.Bitmap.dll", CallingConvention = convention)]
    public static extern RECT MeasureCropArea(IntPtr src, int width, int height, int stride, int color);

    /// <summary>
    /// This overload exists because WPF is messed up and is unable
    /// to properly render bitmaps that have odd sizes.
    /// </summary>
    /// <param name="bmp">The bitmap to crop.</param>
    /// <param name="forceEvenSize">If set, forces sizes to be even.</param>
    /// <returns></returns>
    public static Bitmap Crop(Bitmap bmp, bool forceEvenSize)
    {
      Rectangle rc = new Rectangle(0, 0, bmp.Width, bmp.Height);
      BitmapData bd = bmp.LockBits(rc, ImageLockMode.ReadWrite, bmp.PixelFormat);
      Rectangle crop = rc;
      try
      {
        crop = MeasureCropArea(bd.Scan0, bd.Width, bd.Height, bd.Stride, Color.White.ToArgb());
      }
      catch (Exception ex)
      {
        throw new Exception("Failed to crop bitmap", ex);
      }
      bmp.UnlockBits(bd);

      if (forceEvenSize)
      {
        if ((crop.Width & 1) == 1) ++crop.Width;
        if ((crop.Height & 1) == 1) ++crop.Height;
      }

      Bitmap result = new Bitmap(Math.Max(1, crop.Width), Math.Max(1, crop.Height));
      using (Graphics g = Graphics.FromImage(result))
      {
        g.DrawImage(bmp, new Rectangle(0, 0, crop.Width, crop.Height), crop, GraphicsUnit.Pixel);
      }
      return result;
    }

    public static Bitmap RenderFreeformText(int maxWidth, int maxHeight, string markup)
    {
      var rc = new Rectangle(0, 0, maxWidth, maxHeight);
      using (Bitmap bmp = new Bitmap(maxWidth, maxHeight))
      {
        var bd = bmp.LockBits(rc, ImageLockMode.ReadWrite, bmp.PixelFormat);
        try
        {
          int hr = RenderMarkup(bd.Scan0, markup, bd.Width, bd.Height, bd.Stride, "Gentium", 20.0f, false, false,
                                     false, true, false, 0, 0, 0);
        }
        finally
        {
          bmp.UnlockBits(bd);
        }
        return Crop(bmp);
      }
    }

    // TODO: get rid of this. this is only for testing.
    public static void Process(string comment, YouTrackProxy proxy, string issueId)
    {
      var renderedComments = new List<String>();
      var guids = new List<Guid>();
      var sb = new StringBuilder();

      for (int i = 0; i < comment.Length; ++i)
      {
        if (comment.Substring(i).StartsWith("[{"))
        {
          int end = comment.Substring(i).IndexOf("}]");
          if (end != -1)
          {
            string toRasterize = comment.Substring(i + 2, end - 2);
            renderedComments.Add(toRasterize);
            Guid g = Guid.NewGuid();
            sb.Append('!').Append(g.ToString()).Append(".png!");
            guids.Add(g);
            i = end + 2;
            continue;
          }
        }
        sb.Append(comment[i]);
      }

      // well okay now render all that stuff
      var tempPath = Path.GetTempPath();
      var filesToDelete = new List<string>();
      for (int i = 0; i < renderedComments.Count; ++i)
      {
        using (var bmp = RenderFreeformText(776, 1024, renderedComments[i]))
        {
          string whereToPut = Path.Combine(tempPath, guids[i].ToString() + ".png");
          bmp.Save(whereToPut, ImageFormat.Png);
          filesToDelete.Add(whereToPut);

          // now send it
          proxy.AttachFile(issueId, whereToPut);
        }
      }

      proxy.SubmitComment(issueId, sb.ToString());
    }
  }

  [Serializable, StructLayout(LayoutKind.Sequential)]
  public struct RECT
  {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public RECT(int left, int top, int right, int bottom)
    {
      Left = left;
      Top = top;
      Right = right;
      Bottom = bottom;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is RECT))
      {
        return false;
      }
      return Equals((RECT)obj);
    }

    public bool Equals(RECT value)
    {
      return Left == value.Left &&
             Top == value.Top &&
             Right == value.Right &&
             Bottom == value.Bottom;
    }

    public int Height
    {
      get { return Bottom - Top; }
    }

    public int Width
    {
      get { return Right - Left; }
    }

    public Size Size
    {
      get { return new Size(Width, Height); }
    }

    public Point Location
    {
      get { return new Point(Left, Top); }
    }

    /// <summary>
    /// Handy method for converting to a System.Drawing.Rectangle
    /// </summary>
    /// <returns></returns>
    public Rectangle ToRectangle()
    {
      return Rectangle.FromLTRB(Left, Top, 1 + Right, 1 + Bottom);
    }

    public static RECT FromRectangle(Rectangle rectangle)
    {
      return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
    }

    public override int GetHashCode()
    {
      return Left ^ ((Top << 13) | (Top >> 0x13))
             ^ ((Width << 0x1a) | (Width >> 6))
             ^ ((Height << 7) | (Height >> 0x19));
    }

    #region Operator overloads

    public static implicit operator Rectangle(RECT rect)
    {
      return rect.ToRectangle();
    }

    public static implicit operator RECT(Rectangle rect)
    {
      return FromRectangle(rect);
    }

    #endregion
  }
}
