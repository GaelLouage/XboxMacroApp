using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace XboxMacroApp.Extensions
{
    public static class ListViewExtensions
    {
        public static void RemoveListViewBackgroundBorderOnFocus(this ListView ls)
        {
            Style listViewItemStyle = new Style(typeof(ListViewItem));

            // Set the TargetType for the style
            listViewItemStyle.TargetType = typeof(ListViewItem);

            // Create the ControlTemplate for the ListViewItem
            ControlTemplate template = new ControlTemplate(typeof(ListViewItem));

            // Create the root Border element (similar to the default ListViewItem template)
            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.Name = "Bd";
            border.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Border.BorderBrushProperty));
            border.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(Border.BorderThicknessProperty));
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Border.BackgroundProperty));
            border.SetValue(UIElement.SnapsToDevicePixelsProperty, true);

            // Create the ContentPresenter element (similar to the default ListViewItem template)
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.ContentProperty, new TemplateBindingExtension(ContentPresenter.ContentProperty));
            contentPresenter.SetValue(ContentPresenter.ContentStringFormatProperty, new TemplateBindingExtension(ContentPresenter.ContentStringFormatProperty));
            contentPresenter.SetValue(ContentPresenter.ContentTemplateProperty, new TemplateBindingExtension(ContentPresenter.ContentTemplateProperty));
            contentPresenter.SetValue(FrameworkElement.HorizontalAlignmentProperty, new TemplateBindingExtension(FrameworkElement.HorizontalAlignmentProperty));
            contentPresenter.SetValue(UIElement.SnapsToDevicePixelsProperty, new TemplateBindingExtension(UIElement.SnapsToDevicePixelsProperty));
            contentPresenter.SetValue(FrameworkElement.VerticalAlignmentProperty, new TemplateBindingExtension(FrameworkElement.VerticalAlignmentProperty));

            // Add the ContentPresenter to the Border
            border.AppendChild(contentPresenter);

            // Set the ControlTemplate's VisualTree to the root Border
            template.VisualTree = border;

            // Set the ControlTemplate to the style
            listViewItemStyle.Setters.Add(new Setter(Control.TemplateProperty, template));

            // Remove the IsMouseOver trigger (the trigger responsible for background color change on hover)
            listViewItemStyle.Triggers.Clear();
            ls.ItemContainerStyle = listViewItemStyle;
        }

    }
}
