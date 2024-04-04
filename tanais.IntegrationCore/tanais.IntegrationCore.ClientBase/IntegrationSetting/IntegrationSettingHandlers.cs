using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using tanais.IntegrationCore.IntegrationSetting;

namespace tanais.IntegrationCore
{
  partial class IntegrationSettingClientHandlers
  {

    public virtual void AuthMethodValueInput(Sungero.Presentation.EnumerationValueInputEventArgs e)
    {
      
    }

    public virtual void AddressValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      
    }

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      tanais.IntegrationCore.Functions.IntegrationSetting.SetStateProperties(_obj);
    }

    public virtual void VisiblePasswordValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      e.AddWarning(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordWarning);
    }

  }
}