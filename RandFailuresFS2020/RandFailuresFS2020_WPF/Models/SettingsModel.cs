﻿using SimConModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RandFailuresFS2020_WPF.Models
{
    public class SettingsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private PresetModel _preset;
        private int _setAllPercent;

        public int SetAllPercent
        {
            get { return _setAllPercent; }
            set
            {
                _setAllPercent = value;
                SetAll();
                NotifyPropertyChanged();
            }
        }
        public PresetModel Preset
        {
            get { return _preset; }
            set
            {
                _preset = value;
                NotifyPropertyChanged();
            }
        }


        public SettingsModel()
        {
            Preset = SQLPresets.LoadPreset(SQLOptions.LoadOptionValueInt("PresetID"));
        }

        public void Reload()
        {
            Preset = SQLPresets.LoadPreset(SQLOptions.LoadOptionValueInt("PresetID"));
        }

        public void SavePreset()
        {
            SQLPresets.UpdatePreset(Preset);
        }

        private void SetAll()
        {
            SQLSimVar.UpdateAllPercentage(Preset.PresetID, SetAllPercent);
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
