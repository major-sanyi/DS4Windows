﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS4Windows;
using DS4WinWPF.DS4Forms.ViewModels.Util;

namespace DS4WinWPF.DS4Forms.ViewModels.SpecialActions
{
    public class CalibrateGyroViewModel : NotifyDataErrorBase
    {
        private double holdInterval;
        public double HoldInterval { get => holdInterval; set => holdInterval = value; }

        public void LoadAction(SpecialAction action)
        {
            holdInterval = action.delayTime;
        }

        public void SaveAction(SpecialAction action, bool edit = false)
        {
            Global.SaveAction(action.name, action.controls, 9, $"{holdInterval.ToString("#.##", Global.configFileDecimalCulture)}", edit, holdInterval);
        }

        public override bool IsValid(SpecialAction action)
        {
            ClearOldErrors();

            bool valid = true;
            List<string> holdIntervalErrors = new List<string>();

            if (holdInterval < 0 || holdInterval > 60)
            {
                holdIntervalErrors.Add("Interval not valid");
                errors["HoldInterval"] = holdIntervalErrors;
                RaiseErrorsChanged("HoldInterval");
            }

            return valid;
        }

        public override void ClearOldErrors()
        {
            if (errors.Count > 0)
            {
                errors.Clear();
                RaiseErrorsChanged("HoldInterval");
            }
        }
    }
}
