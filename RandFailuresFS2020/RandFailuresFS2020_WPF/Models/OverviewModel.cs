﻿using SimConModels;
using System.Collections.Generic;
using System.Windows.Media;

namespace RandFailuresFS2020_WPF.Models
{
    public class OverviewModel : BaseModel
    {
        private string? _stateText;
        private Brush? _stateColor;
        private bool _resetEnabled;
        private bool _startEnabled;
        private bool _stopEnabled;
        private List<PresetModel>? _presetsList;
        private PresetModel? _selectedPreset;
        private int _selectedItemPreset;


        public List<PresetModel> PresetsList
        {
            get { return _presetsList!; }
            set
            {
                _presetsList = value;
                NotifyPropertyChanged();
            }
        }
        public PresetModel SelectedPreset
        {
            get { return _selectedPreset!; }
            set
            {
                _selectedPreset = value;
                NotifyPropertyChanged();
                if (value != null)
                    SQLOptions.UpdateOption("PresetID", value.PresetID.ToString());
            }
        }
        public int SelectedItemPreset
        {
            get { return _selectedItemPreset; }
            set
            {
                _selectedItemPreset = value;
                NotifyPropertyChanged();
            }
        }
        public string StateText
        {
            get { return _stateText!; }
            set
            {
                _stateText = value;
                NotifyPropertyChanged();
            }
        }
        public Brush StateColor
        {
            get { return _stateColor!; }
            set
            {
                _stateColor = value;
                NotifyPropertyChanged();
            }
        }
        public bool ResetEnabled
        {
            get { return _resetEnabled; }
            set
            {
                _resetEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool StartEnabled
        {
            get { return _startEnabled; }
            set
            {
                _startEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool StopEnabled
        {
            get { return _stopEnabled; }
            set
            {
                _stopEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public OverviewModel()
        {
            SimCon.GetSimCon().StateChanged += OverviewPresenter_StateChanged;
            StateText = "Sim not found";
            StateColor = Brushes.Red;
            ResetEnabled = false;
            StartEnabled = false;
            StopEnabled = false;
            Reload();
        }

        public void Reload()
        {
            PresetsList = SQLPresets.LoadPresets();
            SelectedItemPreset = PresetsList.FindIndex(a => a.PresetID.Equals(SQLOptions.LoadOptionValueInt("PresetID")));
        }

        private void OverviewPresenter_StateChanged(object? sender, string e)
        {
            StateText = e;
            switch (e)
            {
                case "Sim not found":
                    {
                        StateColor = Brushes.Red;
                        break;
                    }
                case "Sim connected":
                    {
                        StateColor = Brushes.Blue;
                        StartEnabled = true;
                        break;
                    }
                case "Failures started":
                    {
                        StateColor = Brushes.Green;
                        ResetEnabled = true;
                        StopEnabled = true;
                        StartEnabled = false;
                        break;
                    }
                case "Failures stopped":
                    {
                        StateColor = Brushes.Red;
                        ResetEnabled = true;
                        StopEnabled = false;
                        StartEnabled = true;
                        break;
                    }
            }
        }
    }
}
