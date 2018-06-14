﻿using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Pages;
using System;

namespace SharePointPnP.Modernization.Framework.Transform
{
    /// <summary>
    /// Information used to configure the page transformation process
    /// </summary>
    public class PageTransformationInformation
    {
        #region Construction
        /// <summary>
        /// Instantiates the page transformation class
        /// </summary>
        /// <param name="sourcePage">Page we want to transform</param>
        public PageTransformationInformation(ListItem sourcePage) : this(sourcePage, null, false)
        {
        }

        /// <summary>
        /// Instantiates the page transformation class
        /// </summary>
        /// <param name="sourcePage">Page we want to transform</param>
        /// <param name="targetPageName">Name of the target page</param>
        public PageTransformationInformation(ListItem sourcePage, string targetPageName) : this(sourcePage, targetPageName, false)
        {
        }

        /// <summary>
        /// Instantiates the page transformation class
        /// </summary>
        /// <param name="sourcePage">Page we want to transform</param>
        /// <param name="targetPageName">Name of the target page</param>
        /// <param name="overwrite">Do we overwrite the target page if it already exists</param>
        public PageTransformationInformation(ListItem sourcePage, string targetPageName, bool overwrite)
        {
            SourcePage = sourcePage;
            TargetPageName = targetPageName;
            Overwrite = overwrite;
            HandleWikiImagesAndVideos = true;
            TargetPageTakesSourcePageName = false;
            SetDefaultTargetPagePrefix();
            SetDefaultSourcePagePrefix();
        }
        #endregion

        #region Page Properties
        /// <summary>
        /// Source wiki/webpart page we want to transform
        /// </summary>
        public ListItem SourcePage { get; set; }
        /// <summary>
        /// Name for the transformed page
        /// </summary>
        public string TargetPageName { get; set; }
        /// <summary>
        /// Target page will get the source page name, source page will be renamed. Used in conjunction with SourcePagePrefix
        /// </summary>
        public bool TargetPageTakesSourcePageName { get; set; }
        /// <summary>
        /// Overwrite the target page if it already exists?
        /// </summary>
        public bool Overwrite { get; set; }
        /// <summary>
        /// Prefix used to name the target page
        /// </summary>
        public string TargetPagePrefix { get; set; }
        /// <summary>
        /// Prefix used to name the source page. Used in conjunction with TargetPageTakesSourcePageName
        /// </summary>
        public string SourcePagePrefix { get; set; }
        /// <summary>
        /// Configuration of the page header to apply
        /// </summary>
        public ClientSidePageHeader PageHeader { get; set; }
        #endregion

        #region Webpart replacement configuration
        /// <summary>
        /// If true images and videos embedded in wiki text will get a placeholder + a configured image/video client side web part at the end of the page
        /// </summary>
        public bool HandleWikiImagesAndVideos { get; set; }

        /// <summary>
        /// If the page to be transformed is the web's home page then replace with stock modern home page instead of transforming it
        /// </summary>
        public bool ReplaceHomePageWithDefaultHomePage { get; set; }
        #endregion

        #region Override properties
        /// <summary>
        /// Custom function callout that can be triggered to provide a tailored page title
        /// </summary>
        public Func<string, string> PageTitleOverride { get; set; }
        /// <summary>
        /// Custom layout transformator to be used for this page
        /// </summary>
        public Func<ClientSidePage, ILayoutTransformator> LayoutTransformatorOverride { get; set; }
        /// <summary>
        /// Custom content transformator to be used for this page
        /// </summary>
        public Func<ClientSidePage, PageTransformation, IContentTransformator> ContentTransformatorOverride { get; set; }
        #endregion

        #region Functionality
        public void SetDefaultTargetPagePrefix()
        {
            this.TargetPagePrefix = "Migrated_";
        }

        public void SetDefaultSourcePagePrefix()
        {
            this.SourcePagePrefix = "Previous_";
        }
        #endregion

    }
}
