﻿namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the title element.
    /// </summary>
    sealed class HTMLTitleElement : HTMLElement, IHtmlTitleElement
    {
        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        public HTMLTitleElement(Document owner)
            : base(owner, Tags.Title, NodeFlags.Special)
        {
        }

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }
    }
}
