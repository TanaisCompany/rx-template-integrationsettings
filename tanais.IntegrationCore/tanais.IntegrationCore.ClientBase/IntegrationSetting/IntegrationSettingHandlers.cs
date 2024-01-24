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

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      tanais.IntegrationCore.Functions.IntegrationSetting.SetStateProperties(_obj);
    }

    public virtual void VisiblePasswordValueInput(Sungero.Presentation.StringValueInputEventArgs e)
    {
      e.AddWarning(tanais.IntegrationCore.IntegrationSettings.Resources.SetPasswordWarningMessage);
    }

  }
}