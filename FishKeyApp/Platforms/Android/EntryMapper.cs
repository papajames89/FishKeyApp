using Android.Graphics.Drawables;
using FishKeyApp.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace FishKeyApp.Platforms.Android
{
    class EntryMapper
    {
        public static void Map(IElementHandler handler, IElement view)
        {
            if (view is CustomEntry)
            {
                var casted = (EntryHandler)handler;
                var viewData =(CustomEntry)view;

                var gd = new GradientDrawable();
                gd.SetCornerRadius((int)handler.MauiContext?.Context.ToPixels(viewData.CornerRadius));
                gd.SetStroke((int)handler.MauiContext?.Context.ToPixels(viewData.BorderThickness), viewData.BorderColor.ToAndroid());

                casted.PlatformView?.SetBackgroundDrawable(gd);

                var padTop = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Top);
                var padBottom = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Bottom);
                var padLeft = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Left);
                var padRight = (int)handler.MauiContext?.Context.ToPixels(viewData.Padding.Right);

                casted.PlatformView?.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
}
