﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using OpenRcp;
using System.Windows;
using Caliburn.Micro;
using ShowMaker.Desktop.Domain;
using ShowMaker.Desktop.Parser;

namespace ShowMaker.Desktop.Modules.Storyboard.ViewModels
{
    [Export(typeof(StoryboardViewModel))]
    public class StoryboardViewModel : Tool, ILocalizableDisplay
    {
        #region View Data

        private Exhibition exhibition;

        public Exhibition SelectedExhibition
        {
            get { return exhibition; }
            set{
                exhibition = value;
                NotifyOfPropertyChange(() => SelectedExhibition);
            }
        }

        private Area selectedArea;
        private Device selectedDevice;

        #endregion

        public StoryboardViewModel()
        {
        }

        #region Override Tool Methods

        public override string Name
        {
            get
            {
                return StoryboardModule.MENU_VIEW_STORYBOARD;
            }
        }

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        #endregion

        #region Interaction

        public void OnAddNewArea()
        {
            SelectedExhibition.AreaItems.Add(new Area("某展区"));
        }

        public void OnAddNewDevice()
        {
            selectedArea.DeviceItems.Add(new Device());
        }

        public void OnDeviceItemDrop(object sender, DragEventArgs e)
        {
            Device dev = e.Data.GetData(typeof(Device)) as Device;
			MessageBox.Show("" + dev.Type);
        }

        public void OnExhibitionClick(object sender, EventArgs e)
        {
            IoC.Get<IPropertyGrid>().SelectedObject = SelectedExhibition;
        }

        public void OnAreaItemClick(object sender, EventArgs e, Area area)
        {
            selectedArea = area;
            IoC.Get<IPropertyGrid>().SelectedObject = area;
        }

        public void OnDeviceItemClick(object sender, EventArgs e, Device device)
        {
            selectedDevice = device;
            IoC.Get<IPropertyGrid>().SelectedObject = device;
        }

        public void OnOperationItemClick(object sender, EventArgs e, Operation operation)
        {
            IoC.Get<IPropertyGrid>().SelectedObject = operation;
        }

        #endregion

        
    }
}
