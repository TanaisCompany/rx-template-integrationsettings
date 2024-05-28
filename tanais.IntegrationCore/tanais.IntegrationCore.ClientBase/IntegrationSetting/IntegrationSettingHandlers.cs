using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.IntegrationCore.IntegrationSetting;

namespace Tanais.IntegrationCore
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
      Tanais.IntegrationCore.Functions.IntegrationSetting.SetStateProperties(_obj);
    }

    public virtual void VisiblePasswordValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      e.AddWarning(Tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordWarning);
    }

  }
}