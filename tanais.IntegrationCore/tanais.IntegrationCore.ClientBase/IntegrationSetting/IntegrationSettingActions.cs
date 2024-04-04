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
      var dialog = Dialogs.CreateInputDialog(tanais.IntegrationCore.IntegrationSettings.Resources.SetKeyDialog);
      var key = dialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.SetKeyDialogEnterKey, true);
      key.MaxLength(_obj.Info.Properties.AccessKey.Length);
      var confirmKey = dialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.SetKeyDialogConfirmKey, true);
      confirmKey.MaxLength(_obj.Info.Properties.AccessKey.Length);
      
      dialog.Buttons.AddOkCancel();
      dialog.Buttons.Default = DialogButtons.Ok;
      
      dialog.SetOnRefresh(
        x =>
        {
          x.AddError(tanais.IntegrationCore.IntegrationSettings.Resources.DontMatchKeys);
        });
      
      dialog.SetOnButtonClick(
        x =>
        {
          if (x.Button == DialogButtons.Ok && x.IsValid)
          {
            _obj.AccessKey = key.Value;
            _obj.VisibleAccessKey = new String('*', Constants.IntegrationSetting.NumberOfHidingCharacters);
            
            Dialogs.NotifyMessage(tanais.IntegrationCore.IntegrationSettings.Resources.SetKeyDialogComplete);
          }
        });
      
      dialog.Show();
    }

    public virtual bool CanSpecifyKey(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.AuthMethod == tanais.IntegrationCore.IntegrationSetting.AuthMethod.Key;
    }

    public virtual void SetPassword(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var dialog = Dialogs.CreateInputDialog(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordDialog);
      var password = dialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordDialogEnterPassword, true);
      password.MaxLength(_obj.Info.Properties.Password.Length);
      var confirmPassword = dialog.AddPasswordString(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordDialogConfirmPassword, true);
      confirmPassword.MaxLength(_obj.Info.Properties.Password.Length);
      
      dialog.Buttons.AddOkCancel();
      dialog.Buttons.Default = DialogButtons.Ok;
      
      dialog.SetOnRefresh(
        x =>
        {
          if (String.Compare(password.Value, confirmPassword.Value, StringComparison.Ordinal) != 0)
            x.AddError(tanais.IntegrationCore.IntegrationSettings.Resources.DontMatchPasswords);
        });
      
      dialog.SetOnButtonClick(
        x =>
        {
          if (x.Button == DialogButtons.Ok && x.IsValid)
          {
            _obj.Password = password.Value;
            _obj.VisiblePassword = new String('*', Constants.IntegrationSetting.NumberOfHidingCharacters);
            
            Dialogs.NotifyMessage(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordDialogComplete);
          }
        });
      
      dialog.Show();
    }

    public virtual bool CanSetPassword(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.AuthMethod == tanais.IntegrationCore.IntegrationSetting.AuthMethod.Password;
    }

  }

}