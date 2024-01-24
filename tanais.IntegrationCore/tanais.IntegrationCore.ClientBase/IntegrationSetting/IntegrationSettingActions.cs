using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegrationSetting;

namespace tanais.IntegrationCore.Client
{
  partial class IntegrationSettingActions
  {
    public virtual void SpecifyKey(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var inputDialog = Dialogs.CreateInputDialog(tanais.IntegrationCore.IntegrationSettings.Resources.SetKeyDialogTitle);
      var keyDialogField = inputDialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.EnterKeyFieldTitle, true);
      var confirmKeyDialogField = inputDialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.ConfirmKeyDialogFieldTitle, true);
      
      if (inputDialog.Show() == DialogButtons.Ok)
      {
        if (String.Compare(keyDialogField.Value, confirmKeyDialogField.Value, StringComparison.Ordinal) == 0)
        {
          _obj.AccessKey = keyDialogField.Value;
          _obj.VisibleAccessKey = new String('*', Constants.IntegrationSetting.NumberOfHidingCharacters);
        }
        else
          e.AddError(tanais.IntegrationCore.IntegrationSettings.Resources.NotEqualsKeyMessage);
      }
    }

    public virtual bool CanSpecifyKey(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

    public virtual void SetPassword(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var inputDialog = Dialogs.CreateInputDialog(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordDialogTitle);
      var passwordDialogField = inputDialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.EnterPasswordFieldTitle, true);
      var confirmPasswordDialogField = inputDialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.ConfirmPasswordFieldTitle, true);
      
      if (inputDialog.Show() == DialogButtons.Ok)
      {
        if (String.Compare(passwordDialogField.Value, confirmPasswordDialogField.Value, StringComparison.Ordinal) == 0)
        {
          _obj.Password = passwordDialogField.Value;
          _obj.VisiblePassword = new String('*', Constants.IntegrationSetting.NumberOfHidingCharacters);
        }
        else
          e.AddError(tanais.IntegrationCore.IntegrationSettings.Resources.NotEqualsPasswordMessage);
      }
    }

    public virtual bool CanSetPassword(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return true;
    }

  }

}