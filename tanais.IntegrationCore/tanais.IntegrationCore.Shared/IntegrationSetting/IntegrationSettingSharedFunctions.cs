using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.IntegrationSetting;

namespace Tanais.IntegrationCore.Shared
{
  partial class IntegrationSettingFunctions
  {

    /// <summary>
    /// Установить видимость, обязательность свойств.
    /// </summary>
    public virtual void SetStateProperties()
    {
      var hasService = !string.IsNullOrWhiteSpace(_obj.Address);
      
      var isKey = hasService && _obj.AuthMethod == Tanais.IntegrationCore.IntegrationSetting.AuthMethod.Key;
      var isPassword = hasService && _obj.AuthMethod == Tanais.IntegrationCore.IntegrationSetting.AuthMethod.Password;
      
      // Способ авторизации.
      _obj.State.Properties.AuthMethod.IsVisible = hasService;
      _obj.State.Properties.AuthMethod.IsRequired = hasService;
      
      // Ключ доступа.
      _obj.State.Properties.VisibleAccessKey.IsVisible = isKey;
      _obj.State.Properties.VisibleAccessKey.IsRequired = isKey;
      
      // Имя пользователя, Пароль.
      _obj.State.Properties.Login.IsVisible = isPassword;
      _obj.State.Properties.Login.IsRequired = isPassword;
      _obj.State.Properties.VisiblePassword.IsVisible = isPassword;
      _obj.State.Properties.VisiblePassword.IsRequired = isPassword;
    }
    
    /// <summary>
    /// Заполнить имя записи справочника.
    /// </summary>
    public virtual void FillName()
    {
      var name = string.Empty;
      
      using (TenantInfo.Culture.SwitchTo())
      {
        name = Tanais.IntegrationCore.IntegrationSettings.Resources.NamePrefix;
        
        if (_obj.IntegratedSystem != null)
          name += Tanais.IntegrationCore.IntegrationSettings.Resources.NamePartWithIntegratedSystemFormat(_obj.IntegratedSystem.Name);
          
        if (_obj.BusinessUnit != null)
          name += Tanais.IntegrationCore.IntegrationSettings.Resources.NamePartForBusinessUnitFormat(_obj.BusinessUnit.Name);
      }
      
      _obj.Name = Sungero.Docflow.PublicFunctions.Module.TrimSpecialSymbols(name);
    }
  }
}