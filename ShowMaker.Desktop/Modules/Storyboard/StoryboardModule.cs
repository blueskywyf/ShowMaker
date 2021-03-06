﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using OpenRcp;
using ShowMaker.Desktop.Modules.Storyboard.ViewModels;

namespace ShowMaker.Desktop.Modules.Storyboard
{
    /// <summary>
    /// 故事板：设备、时间线
    /// </summary>
    [ExportModule]
    public class StoryboardModule : ModuleBase
    {
        public const string MENU_VIEW_STORYBOARD = "MENU_VIEW_STORYBOARD";
        
        public const string STORYBOARD_NEW_AREA = "STORYBOARD_NEW_AREA";
        public const string STORYBOARD_ADD_DEVICE = "STORYBOARD_ADD_DEVICE";
        public const string STORYBOARD_ADD_OPERATION = "STORYBOARD_ADD_OPERATION";

        #region Override ModuleBase Methods

        protected override void RegisterMenus()
        {
            MainMenu[ShellModule.MENU_VIEW].Add(new CheckableMenuItem(MENU_VIEW_STORYBOARD, openDevicesToolBox));
        }

        #endregion

        private IEnumerable<IResult> openDevicesToolBox(bool isChecked)
        {
            if (isChecked)
            {
                yield return ResultsHelper.ShowTool<StoryboardViewModel>();
            }
            else
            {
                yield return ResultsHelper.HideTool<StoryboardViewModel>();
            }
        }
    }
}
