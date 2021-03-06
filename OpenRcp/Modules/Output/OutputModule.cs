﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;

namespace OpenRcp
{
    [ExportModule]
    public class OutputModule : ModuleBase, IHandle<IMessage>
    {
        public const string MENU_VIEW_OUTPUT = "MENU_VIEW_OUTPUT";

        #region Module Info const

        public const string OUTPUT_MODULEINFO_NAME = "OUTPUT_MODULEINFO_NAME";
        public const string OUTPUT_MODULEINFO_HEADER = "OUTPUT_MODULEINFO_HEADER";
        public const string OUTPUT_MODULEINFO_AUTHORS = "OUTPUT_MODULEINFO_AUTHORS";
        public const string OUTPUT_MODULEINFO_DESCRIPTION = "OUTPUT_MODULEINFO_DESCRIPTION";
        public const string OUTPUT_MODULEINFO_COPYRIGHT = "OUTPUT_MODULEINFO_COPYRIGHT";

        #endregion

        #region Override ModuleBase Methods

        protected override void PreInit()
        {
            var outputTool = IoC.Get<IOutput>();
            Shell.ShowTool(outputTool);

            SubcribeMessage(this);
            PublishMessage(new ModuleInitMessage
            {
                Content = "Loading Output Module"
            });
        }

        protected override void RegisterMenus()
        {
            MainMenu[ShellModule.MENU_VIEW].Add(new CheckableMenuItem(OutputModule.MENU_VIEW_OUTPUT, OpenOutput).Checked());
        }

        protected override ModuleInfoItem GetModuleInfo()
        {
            IResourceService rs = IoC.Get<IResourceService>();
            var item = new ModuleInfoItem
            {
                Name = rs.GetString(OUTPUT_MODULEINFO_NAME),
                HeaderInfo = rs.GetString(OUTPUT_MODULEINFO_HEADER),
                Authors = rs.GetString(OUTPUT_MODULEINFO_AUTHORS),
                Description = rs.GetString(OUTPUT_MODULEINFO_DESCRIPTION),
                Version = string.Format(rs.GetString(ModuleBase.MODULEINFO_VERSION), Assembly.GetExecutingAssembly().GetName().Version, IntPtr.Size * 8),
                CopyrightNotice = rs.GetString(OUTPUT_MODULEINFO_COPYRIGHT),
                Rights = rs.GetString(ModuleBase.MODULEINFO_RIGHTS)
            };
            return item;
        }

        #endregion

        private IEnumerable<IResult> OpenOutput(bool isChecked)
        {
            if (isChecked)
                yield return ResultsHelper.ShowTool<IOutput>();
            else
                yield return ResultsHelper.HideTool<IOutput>();
        }

        public void Handle(IMessage message)
        {
            IoC.Get<IOutput>().AppendLine(message.Content);
        }
    }
}