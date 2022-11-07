﻿using SimConModels;
using SimConModels.SimVar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RandFailuresFS2020_WPF.Models
{
    public class FailListModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _failuresText;

        public string FailuresText
        {
            get { return _failuresText; }
            set
            {
                _failuresText = value;
                NotifyPropertyChanged();
            }
        }

        public FailListModel()
        {
            FailuresText = "";
        }

        public void ShowFailures()
        {
            FailuresText = "";
            string altTime = "";

            if (SimVarLists.GetSimVarLists().GetFailuresList() != null)
                foreach (var sv in SimVarLists.GetSimVarLists().GetFailuresList())
                {
                    if (sv.WhenFail == WHEN_FAIL.ALT)
                    {
                        altTime = "at " + sv.FailureAlt.ToString() + " ft";
                    }
                    else if (sv.WhenFail == WHEN_FAIL.TIME)
                    {
                        altTime = "after " + sv.FailureTime.ToString() + " seconds";
                    }
                    else if (sv.WhenFail == WHEN_FAIL.SPEED)
                    {
                        altTime = "at " + sv.FailureSpeed.ToString() + " kts";
                    }
                    else
                    {
                        altTime = "";
                    }

                    FailuresText += "Name: " + sv.SimVarName + " when will fail: " + sv.WhenFail + " " + altTime + Environment.NewLine;
                }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
